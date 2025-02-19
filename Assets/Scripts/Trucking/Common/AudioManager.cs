using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using UniRx;

namespace Trucking.Common
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public HashSet<Sound> sounds =
            new HashSet<Sound>();

        public Sound music;
        public bool soundMuted;
        public bool musicMuted;

        protected override void AwakeSingleton()
        {
            Persist = true;
        }

        /// Creates a new sound, registers it, gives it the properties specified, and starts playing it
        public Sound PlaySound(string soundName, bool loop = false, bool interrupts = false,
            Action<Sound> callback = null)
        {
            if (soundMuted)
            {
                return null;
            }

            Sound sound = null;

            if (loop)
            {
                sound = HasRegister(soundName);
            }

            if (sound == null)
            {
                sound = NewSound(soundName, loop, interrupts, callback);
            }

            if (sound != null && sound.source != null)
            {
                sound.playing = !soundMuted;
            }

            return sound;
        }

        public Sound PlaySoundIntaval(string soundName, float intaval, int count, bool loop = false,
            bool interrupts = false,
            Action<Sound> callback = null)
        {
            Sound sound = null;

            Observable.Interval(TimeSpan.FromSeconds(intaval)).StartWith(0).Take(count)
                .Subscribe(_ =>
                {
                    sound = NewSound(soundName, loop, interrupts, callback);

                    if (sound != null)
                    {
                        sound.playing = !soundMuted;
                    }
                }).AddTo(this);

            return sound;
        }


        /// Creates a new sound, registers it, and gives it the properties specified
        public Sound NewSound(string soundName, bool loop = false, bool interrupts = false,
            Action<Sound> callback = null)
        {
            Sound sound = new Sound(soundName);
            RegisterSound(sound);

            if (sound != null)
            {
                sound.loop = loop;
                sound.interrupts = interrupts;
                sound.callback = callback;
            }

            return sound;
        }

        public void Stop(string soundName)
        {
            Sound sound = HasRegister(soundName);

            if (sound != null && sound.source != null)
            {
                sound.source.Stop();
            }
        }

        public Sound PlayMusic(string soundName, bool loop = false)
        {
            if (music != null && music.name == soundName)
            {
                return music;
            }

            if (music != null && music.playing)
            {
                music.Finish();
            }

            music = NewSound(soundName, loop);
            music.playing = !musicMuted;

            return music;
        }

        /// Registers a sound with the AudioManager and gives it an AudioSource if necessary
        /// You should probably avoid calling this function directly and just use 
        /// NewSound and PlayNewSound instead
        public void RegisterSound(Sound sound)
        {
            sounds.Add(sound);
            sound.audioManager = this;
            if (sound.source == null)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = sound.clip;
                sound.source = source;
            }
        }

        public Sound HasRegister(string soundName)
        {
            return sounds.ToList().Find(x => x.name == soundName);
        }

        public bool IsPlaying(string soundName)
        {
            Sound sound = HasRegister(soundName);

            if (sound != null)
            {
                return sound.playing;
            }

            return false;
        }

        private void Update()
        {
            foreach (var sound in sounds)
            {
                sound.Update();
            }
        }

        public void SoundMuted(bool isMute)
        {
            soundMuted = isMute;

            foreach (var sound in sounds)
            {
                if (sound != music)
                {
                    sound.SoundMuted(isMute);
                }
            }
        }

        public void MusicMuted(bool isMute)
        {
            musicMuted = isMute;

            music?.MusicMuted(isMute);
        }

        public void MusicPause()
        {
            if (music != null && music.source != null)
            {
                music.source.volume = 0.0f;
            }
        }

        public void MusicUnpause()
        {
            if (music != null && music.source != null)
            {
                music.source.volume = 1.0f;
            }
        }
    }

    public class Sound
    {
        public AudioManager audioManager;
        public string name;
        public AudioClip clip;
        public AudioSource source;
        public Action<Sound> callback;
        public bool loop;
        public bool interrupts;

        private HashSet<Sound> interruptedSounds =
            new HashSet<Sound>();

        /// returns a float from 0.0 to 1.0 representing how much
        /// of the sound has been played so far
        public float progress
        {
            get
            {
                if (source == null || clip == null)
                    return 0f;
                return (float) source.timeSamples / (float) clip.samples;
            }
        }

        /// returns true if the sound has finished playing
        /// will always be false for looping sounds
        public bool finished
        {
            get { return !loop && progress >= 1f; }
        }

        /// returns true if the sound is currently playing,
        /// false if it is paused or finished
        /// can be set to true or false to play/pause the sound
        /// will register the sound before playing
        public bool playing
        {
            get { return source != null && source.isPlaying; }
            set
            {
                if (value)
                {
                    audioManager.RegisterSound(this);
                }

                PlayOrPause(value, interrupts);
            }
        }

        /// Try to avoid calling this directly
        /// Use AudioManager.NewSound instead
        public Sound(string newName)
        {
            name = newName;
            clip = (AudioClip) Resources.Load("Sound/" + name, typeof(AudioClip));
            if (clip == null)
                throw new Exception("Couldn't find AudioClip with name '" + name +
                                    "'. Are you sure the file is in a folder named 'Resources'?");
        }

        public void Update()
        {
            if (source != null)
                source.loop = loop;
            if (finished)
                Finish();
        }

        /// Try to avoid calling this directly
        /// Use the Sound.playing property instead
        public void PlayOrPause(bool play, bool pauseOthers)
        {
            if (pauseOthers)
            {
                if (play)
                {
                    interruptedSounds = new HashSet<Sound>(audioManager.sounds.Where(snd => snd.playing &&
                                                                                            snd != this));
                }

                interruptedSounds.ToList().ForEach(sound => sound.PlayOrPause(!play, false));
            }

            if (play && !source.isPlaying)
            {
                source.Play();
            }
            else if (source.isPlaying)
            {
                source.Pause();
            }
        }

        /// performs necessary actions when a sound finishes
        public void Finish()
        {
            PlayOrPause(false, true);
            if (callback != null)
                callback(this);
            MonoBehaviour.Destroy(source);
            source = null;
        }

        /// Reset the sound to its beginning
        public void Reset()
        {
            source.time = 0f;
        }

        public void SoundMuted(bool isMute)
        {
            if (source != null)
            {
                if (isMute)
                {
                    Finish();
                }
            }
        }

        public void MusicMuted(bool isMute)
        {
            if (source != null)
            {
                source.mute = isMute;

                if (!isMute)
                {
                    source.Play();
                }
            }
        }
    }
}