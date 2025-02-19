using System.Collections.Generic;
using Coffee.UIExtensions;
using I2.Loc;
using Mobcast.Coffee.Toggles;
using TMPro;
using Trucking.Common;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_Option : Popup_Base<Popup_Option>
    {
        public Button btnSound;
        public Button btnSound_Off;
        public Button btnMusic;
        public Button btnMusic_Off;
        public CompositeToggle toggleSound;
        public CompositeToggle toggleMusic;

        public Button btnFacebook_login;
        public Button btnFacebook_logoff;
        public TextMeshProUGUI txtFacebook;

        public Button btnGoogle_login;
        public Button btnGoogle_logoff;
        public TextMeshProUGUI txtGoogle;
        public Button btnReset;
        public Button btnWithdraw;

        public CompositeToggle toggleAll;
        public CompositeToggle toggleEachTruckArrive;
        public CompositeToggle toggleAllTruckArrive;
        public CompositeToggle toggleEachRefuelingComplete;
        public CompositeToggle toggleAllRefuelingComplete;
        public CompositeToggle toggleEventStart;
        public CompositeToggle toggleEventDelivery;
        public CompositeToggle togglePartProduction;
        public CompositeToggle toggleProductionUpgrade;

        public TextMeshProUGUI txtVersion;
        public Button btnTerms;
        public Button btnEmail;

        public Button btnFps60;
        public Button btnFps30;

        public TMP_Dropdown dropdownLanguage;


        private void Start()
        {
            dropdownLanguage.value = UserDataManager.Instance.data.optionLanguage.Value;
            toggleSound.booleanValue = UserDataManager.Instance.data.settingSound.Value;
            toggleMusic.booleanValue = UserDataManager.Instance.data.settingMusic.Value;

            toggleAllTruckArrive.booleanValue = UserDataManager.Instance.data.notiAllTruckArrive.Value;
            toggleEachTruckArrive.booleanValue = UserDataManager.Instance.data.notiEachTruckArrive.Value;
            toggleAllRefuelingComplete.booleanValue = UserDataManager.Instance.data.notiAllRefuelingComplete.Value;
            toggleEachRefuelingComplete.booleanValue = UserDataManager.Instance.data.notiEachRefuelingComplete.Value;
            toggleEventStart.booleanValue = UserDataManager.Instance.data.notiEventStart.Value;
            toggleEventDelivery.booleanValue = UserDataManager.Instance.data.notiEventDelivery.Value;
            togglePartProduction.booleanValue = UserDataManager.Instance.data.notiPartProduction.Value;
            toggleProductionUpgrade.booleanValue = UserDataManager.Instance.data.notiProductionUpgrade.Value;

            btnTerms.OnClickAsObservable().Subscribe(_ =>
            {
                Application.OpenURL("https://watersplash.cookappsgames.com/watersplash/policy/privacy.html");
            }).AddTo(this);

            btnEmail.OnClickAsObservable().Subscribe(_ =>
            {
                //email Id to send the mail to
                string email = "cookappsplay@gmail.com";
                //subject of the mail
                string subject = EscapeURL("FEEDBACK/SUGGESTION");
                //body of the mail which consists of Device Model and its Operating System
                string body = EscapeURL("Please Enter your message here\n\n\n\n" +
                                        "________" +
                                        "\n\nPlease Do Not Modify This\n\n" +
                                        "Model: " + SystemInfo.deviceModel + "\n\n" +
                                        "OS: " + SystemInfo.operatingSystem + "\n\n" +
                                        "________");
                //Open the Default Mail App
                Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
            }).AddTo(this);


            btnSound.OnClickAsObservable().Subscribe(_ => { toggleSound.booleanValue = !toggleSound.booleanValue; })
                .AddTo(this);

            btnSound_Off.OnClickAsObservable().Subscribe(_ => { toggleSound.booleanValue = !toggleSound.booleanValue; })
                .AddTo(this);

            btnMusic.OnClickAsObservable().Subscribe(_ => { toggleMusic.booleanValue = !toggleMusic.booleanValue; })
                .AddTo(this);

            btnMusic_Off.OnClickAsObservable().Subscribe(_ => { toggleMusic.booleanValue = !toggleMusic.booleanValue; })
                .AddTo(this);

            btnWithdraw.OnClickAsObservable().Subscribe(_ =>
                {
                    Popup_Common.Instance.Show(Utilities.GetStringByData(19908),
                            Utilities.GetStringByData(19909)
                            + "\n\n" + Utilities.GetStringByData(19910)
                            + "\n\n" + Utilities.GetStringByData(19911))
                        .SetBigSize(900, 600)
                        .SetLeft(Utilities.GetStringByData(19912), Popup_Common.ButtonColor.Red,
                            () => LoadingScene.LoadScene("Title"))
                        .SetRight(Utilities.GetStringByData(19913), Popup_Common.ButtonColor.Green);
                    PlayerPrefs.SetInt("GDPR", 0);
                })
                .AddTo(this);


            toggleSound.ObserveEveryValueChanged(x => x.booleanValue).Subscribe(sound =>
            {
                if (UserDataManager.Instance.data.settingSound.Value != sound)
                {
                    UserDataManager.Instance.data.settingSound.Value = sound;
                    AudioManager.Instance.SoundMuted(!sound);
                    AudioManager.Instance.PlaySound("sfx_button_main");
                }
            }).AddTo(this);

            toggleMusic.ObserveEveryValueChanged(x => x.booleanValue).Subscribe(music =>
            {
                if (UserDataManager.Instance.data.settingMusic.Value != music)
                {
                    UserDataManager.Instance.data.settingMusic.Value = music;
                    AudioManager.Instance.MusicMuted(!music);
                    AudioManager.Instance.PlaySound("sfx_button_main");
                }
            }).AddTo(this);

            // noti
            toggleAll.GetComponent<Button>().OnClickAsObservable().Subscribe(_ =>
            {
                toggleAllTruckArrive.booleanValue = toggleAll.booleanValue;
                toggleEachTruckArrive.booleanValue = toggleAll.booleanValue;
                toggleAllRefuelingComplete.booleanValue = toggleAll.booleanValue;
                toggleEachRefuelingComplete.booleanValue = toggleAll.booleanValue;
                toggleEventStart.booleanValue = toggleAll.booleanValue;
                toggleEventDelivery.booleanValue = toggleAll.booleanValue;
            }).AddTo(this);


            toggleAllTruckArrive.ObserveEveryValueChanged(x => x.booleanValue).Subscribe(noti =>
            {
                if (UserDataManager.Instance.data.notiAllTruckArrive.Value != noti)
                {
                    UserDataManager.Instance.data.notiAllTruckArrive.Value = noti;
                    AudioManager.Instance.PlaySound("sfx_button_main");
                }
            }).AddTo(this);

            toggleEachTruckArrive.ObserveEveryValueChanged(x => x.booleanValue).Subscribe(noti =>
            {
                if (UserDataManager.Instance.data.notiEachTruckArrive.Value != noti)
                {
                    UserDataManager.Instance.data.notiEachTruckArrive.Value = noti;
                    AudioManager.Instance.PlaySound("sfx_button_main");
                }
            }).AddTo(this);

            toggleAllRefuelingComplete.ObserveEveryValueChanged(x => x.booleanValue).Subscribe(noti =>
            {
                if (UserDataManager.Instance.data.notiAllRefuelingComplete.Value != noti)
                {
                    UserDataManager.Instance.data.notiAllRefuelingComplete.Value = noti;
                    AudioManager.Instance.PlaySound("sfx_button_main");
                }
            }).AddTo(this);


            toggleEachRefuelingComplete.ObserveEveryValueChanged(x => x.booleanValue).Subscribe(noti =>
            {
                if (UserDataManager.Instance.data.notiEachRefuelingComplete.Value != noti)
                {
                    UserDataManager.Instance.data.notiEachRefuelingComplete.Value = noti;
                    AudioManager.Instance.PlaySound("sfx_button_main");
                }
            }).AddTo(this);


            toggleEventStart.ObserveEveryValueChanged(x => x.booleanValue).Subscribe(noti =>
            {
                if (UserDataManager.Instance.data.notiEventStart.Value != noti)
                {
                    UserDataManager.Instance.data.notiEventStart.Value = noti;
                    AudioManager.Instance.PlaySound("sfx_button_main");
                }
            }).AddTo(this);

            toggleEventDelivery.ObserveEveryValueChanged(x => x.booleanValue).Subscribe(noti =>
            {
                if (UserDataManager.Instance.data.notiEventDelivery.Value != noti)
                {
                    UserDataManager.Instance.data.notiEventDelivery.Value = noti;
                    AudioManager.Instance.PlaySound("sfx_button_main");
                }
            }).AddTo(this);

            togglePartProduction.ObserveEveryValueChanged(x => x.booleanValue).Subscribe(noti =>
            {
                if (UserDataManager.Instance.data.notiPartProduction.Value != noti)
                {
                    UserDataManager.Instance.data.notiPartProduction.Value = noti;
                    AudioManager.Instance.PlaySound("sfx_button_main");
                }
            }).AddTo(this);


            toggleProductionUpgrade.ObserveEveryValueChanged(x => x.booleanValue).Subscribe(noti =>
            {
                if (UserDataManager.Instance.data.notiProductionUpgrade.Value != noti)
                {
                    UserDataManager.Instance.data.notiProductionUpgrade.Value = noti;
                    AudioManager.Instance.PlaySound("sfx_button_main");
                }
            }).AddTo(this);


            // //noti

            Observable.CombineLatest(UserDataManager.Instance.data.notiAllTruckArrive,
                UserDataManager.Instance.data.notiEachTruckArrive,
                UserDataManager.Instance.data.notiAllRefuelingComplete,
                UserDataManager.Instance.data.notiEachRefuelingComplete,
                UserDataManager.Instance.data.notiEventStart,
                UserDataManager.Instance.data.notiEventDelivery,
                UserDataManager.Instance.data.notiPartProduction,
                UserDataManager.Instance.data.notiProductionUpgrade
            ).Subscribe(_ =>
            {
                toggleAll.booleanValue = toggleAllTruckArrive.booleanValue
                                         && toggleEachTruckArrive.booleanValue
                                         && toggleAllRefuelingComplete.booleanValue
                                         && toggleEachRefuelingComplete.booleanValue
                                         && toggleEventStart.booleanValue
                                         && toggleEventDelivery.booleanValue
                                         && togglePartProduction.booleanValue
                                         && toggleProductionUpgrade.booleanValue;
            });

            SetLanguage(UserDataManager.Instance.data.optionLanguage.Value);

            dropdownLanguage.OnSelectAsObservable()
                .Subscribe(_ => { AudioManager.Instance.PlaySound("sfx_button_main"); }).AddTo(this);

            dropdownLanguage.ObserveEveryValueChanged(x => x.value).Subscribe(index =>
            {
                Debug.Log("index : " + index.ToString());
                SetLanguage(index);
                UserDataManager.Instance.data.optionLanguage.Value = index;
            }).AddTo(this);

            btnFps60.OnClickAsObservable().Subscribe(_ =>
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
                SetMaxFps(0);
            }).AddTo(this);

            btnFps30.OnClickAsObservable().Subscribe(_ =>
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
                SetMaxFps(1);
            }).AddTo(this);

            dropdownLanguage.transform.parent.gameObject.SetActive(false);
//            btnWithdraw.gameObject.SetActive(false);
        }

        public void SetMaxFps(int value)
        {
            int[] fps = {50, 30};
            value = Mathf.Clamp(value, 0, 1);
            Application.targetFrameRate = fps[value];
            UserDataManager.Instance.data.maxFps.Value = value;
            btnFps60.GetComponent<UIEffect>().enabled = value != 0;
            btnFps30.GetComponent<UIEffect>().enabled = value != 1;
        }

        string EscapeURL(string url)
        {
            return WWW.EscapeURL(url).Replace("+", "%20");
        }

        void SetLanguage(int index)
        {
            if (index == 0)
            {
                LocalizationManager.CurrentLanguage = "English";
            }
            else
            {
                LocalizationManager.CurrentLanguage = "Korean";
            }
//            LocalizationManager.CurrentLanguage =
//                LocalizationManager.GetAllLanguages()[UserDataManager.Instance.data.optionLanguage.Value];

//            LocalizationManager.CurrentLanguage = Utilities.GetStringByData(20801 + UserDataManager.Instance.data.optionLanguage.Value);
            List<TMP_Dropdown.OptionData> languageOptions = new List<TMP_Dropdown.OptionData>();

            for (int i = 0; i < 10; i++)
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(Utilities.GetStringByData(20801 + i));
                languageOptions.Add(optionData);
            }


            dropdownLanguage.options = languageOptions;
            dropdownLanguage.captionText.text = languageOptions[index].text;
            txtVersion.text = "Ver " + Trucking.Common.Trucking.GetVersionString();
        }

        public override void Close()
        {
            base.Close();
            UserDataManager.Instance.SaveData();

            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled, string error) =>
                {
                    Debug.Log("advertisingId " + advertisingId + " " + trackingEnabled + " " + error);
                }
            );
        }
    }
}