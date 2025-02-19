using System;
using System.Collections.Generic;
using I2.Loc;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Trucking.Common
{
    public class Utilities : MonoSingleton<Utilities>
    {
        public static int LimitToRange(int value, int min, int max)
        {
            return Math.Min(max, Math.Max(value, min));
        }


        public static void RemoveAllChildren(Transform source)
        {
            int childs = source.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                Transform child = source.GetChild(i);
                child.gameObject.SetActive(false);
                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }

        public static void RemoveObject(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(null);
            Destroy(obj);
        }

        public static void ChangeLayers(GameObject go, string name)
        {
            ChangeLayers(go, LayerMask.NameToLayer(name));
        }

        public static void ChangeLayers(GameObject go, int layer)
        {
            go.layer = layer;
            foreach (Transform child in go.transform)
            {
                ChangeLayers(child.gameObject, layer);
            }
        }

        public static Color GetColorByHtmlString(string html)
        {
            if (html[0] != '#')
            {
                html = "#" + html;
            }

            Color color;
            ColorUtility.TryParseHtmlString(html, out color);

            return color;
        }

        public static List<T> FindComponent<T>(Transform t, List<T> list = null) where T : Behaviour
        {
            if (list == null)
            {
                list = new List<T>();
            }

            T[] meList = t.GetComponents<T>();

            if (meList != null)
            {
                list.AddRange(meList);
            }

            foreach (Transform child in t)
            {
                List<T> childList = FindComponent<T>(child);

                if (childList != null)
                {
                    list.AddRange(childList);
                }
            }

            return list;
        }


        public static Vector3 FindCenterPoint(List<Vector3> targets)
        {
            Vector3 centroid;
            Vector3 minPoint = targets[0];
            Vector3 maxPoint = targets[0];

            for (int i = 1; i < targets.Count; i++)
            {
                Vector3 pos = targets[i];
                if (pos.x < minPoint.x)
                    minPoint.x = pos.x;
                if (pos.x > maxPoint.x)
                    maxPoint.x = pos.x;
                if (pos.y < minPoint.y)
                    minPoint.y = pos.y;
                if (pos.y > maxPoint.y)
                    maxPoint.y = pos.y;
                if (pos.z < minPoint.z)
                    minPoint.z = pos.z;
                if (pos.z > maxPoint.z)
                    maxPoint.z = pos.z;
            }

            centroid = minPoint + 0.5f * (maxPoint - minPoint);

            return centroid;
        }

        public static int GetActiveChildCount(Transform transform)
        {
            int count = 0;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    count++;
                }
            }

            return count;
        }

        public static float GetAnimatoionLength(Animator animator, string name)
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;

            if (ac != null)
            {
                for (int i = 0; i < ac.animationClips.Length; i++)
                    if (ac.animationClips[i].name == name)
                        return ac.animationClips[i].length;
            }

            return 0;
        }

        public static string GetThousandCommaText(long data)
        {
            return string.Format("{0:#,##0}", data);
        }

        public static string GetTimeString(TimeSpan time)
        {
            string text = "";
            int count = 0;

            if (time.TotalSeconds == 0)
            {
                text += "--m --s";
                return text;
            }

            if (time.Days > 0)
            {
                text += $"{time.Days:D1}d ";
                count++;
            }

            if (time.Hours > 0)
            {
                text += $"{time.Hours:D1}h ";
                count++;
            }

            if (time.Minutes > 0 && count < 2)
            {
                text += $"{time.Minutes:D1}m ";
                count++;
            }

            if (time.Seconds > 0 && count < 2)
            {
                text += $"{time.Seconds:D1}s ";
            }
            else if (time.Seconds == 0 && count == 0)
            {
                text += $"{time.Seconds:D1}s ";
            }

            return text;
        }

        public static string GetTimeStringShort(TimeSpan time)
        {
            string text;

            if (time.Days > 0)
            {
                text = $"{time.Days:D1}d {time.Hours:D1}h";
            }
            else if (time.Hours > 0)
            {
                text = $"{time.Hours:D1}h {time.Minutes:D1}m";
            }
            else if (time.Minutes > 0)
            {
                text = $"{time.Minutes:D1}m {time.Seconds:D1}s";
            }
            else
            {
                text = $"{time.Seconds:D1}s";
            }

            return text;
        }

        public static string GetNumberKKK(long value)
        {
            if (Math.Abs(value) >= 100000)
            {
                long k = value / 1000;

                return GetThousandCommaText(k) + "K";
            }

            return GetThousandCommaText(value);
        }

        public static string GetTimeDate(int min)
        {
            TimeSpan time = TimeSpan.FromMinutes(min);

            if (time.Days > 0)
            {
                return time.Days + GetStringByData(41113);
            }

            if (time.Hours > 0)
            {
                return time.Hours + GetStringByData(41112);
            }

            if (time.Minutes > 0)
            {
                return time.Minutes + GetStringByData(41111);
            }

            return "";
        }

        public static void SetCanvasFirst(Transform trs)
        {
            Canvas canvas = FindObjectOfType<Canvas>();

            if (canvas != null)
            {
                trs.SetParent(canvas.transform, false);
                trs.SetAsLastSibling();
            }
        }

        static public int RandomRange(int min, int max)
        {
            if (min == max) return min;
            return Random.Range(min, max + 1);
        }

        public static void LogParent(Transform trs, string str)
        {
            if (trs.parent != null)
            {
                str = trs.parent.name + "/" + str;
                LogParent(trs.parent, str);
            }
            else
            {
                Debug.Log(str);
            }
        }

        public static string GetStringByData(int id, TextMeshProUGUI textMeshProUgui = null)
        {
            return LocalizationManager.GetTranslation(id.ToString());
        }

        public static IReadOnlyReactiveProperty<string> ObsGetStringByData(int id)
        {
            return Observable.Create<string>(
                obs =>
                {
                    obs.OnNext(LocalizationManager.GetTranslation(id.ToString()));
                    return Disposable.Empty;
                }
            ).DistinctUntilChanged().ToReadOnlyReactiveProperty();
        }


        public static void SetLocalize(TextMeshProUGUI textMeshProUgui)
        {
            if (textMeshProUgui != null)
            {
                if (textMeshProUgui.GetComponent<Localize>() == null)
                {
                    string str = textMeshProUgui.name + " : " + textMeshProUgui.fontMaterial.name;
                    LogParent(textMeshProUgui.transform, str);

                    int startIndex = textMeshProUgui.fontMaterial.name.IndexOf("SDF");
                    string fontString = textMeshProUgui.fontMaterial.name.Substring(startIndex + 4)
                        .Replace(" (Instance)", "");

                    Debug.Log(fontString);
                    Localize localize = textMeshProUgui.gameObject.AddComponent<Localize>();
                    localize.SetTerm("", "font/font_" + fontString);
                }
            }
        }

        public static long LongClamp(long value, long min, long max)
        {
            if (value > max)
            {
                return max;
            }

            if (value < 0)
            {
                return 0;
            }

            return value;
        }


        public static void MoveTaskToBack()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaObject activity =
                    new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>(
                        "currentActivity");
                activity.Call<bool>("moveTaskToBack", true);
            }
        }

        public static Vector3 worldToUISpace(Camera camera, Canvas parentCanvas, Vector3 worldPos)
        {
            //first you need the RectTransform component of your canvas
            RectTransform CanvasRect = parentCanvas.GetComponent<RectTransform>();

            //then you calculate the position of the UI element
            //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

            Vector2 ViewportPosition = camera.WorldToViewportPoint(worldPos);
            Vector2 WorldObject_ScreenPosition = new Vector2(
                ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the ui element
            return WorldObject_ScreenPosition;
        }

        public static string GetDecimalPoint(float value, string p = "N1")
        {
            string result = string.Empty;

            if (value == (int) value)
            {
                result = value.ToString();
            }
            else
            {
                result = value.ToString(p);
            }

            return result;
        }

        public static Vector3 CopyVector3(Vector3 ori, float x = 0, float y = 0, float z = 0)
        {
            return new Vector3(ori.x + x, ori.y + y, ori.z + z);
        }

        public static Vector3 CopyVector3FromRectTransform(RectTransform rt, float x = 0, float y = 0, float z = 0)
        {
            float ratio = Screen.width / rt.root.GetComponent<CanvasScaler>().referenceResolution.x;

            return CopyVector3(rt.position, x * ratio, y * ratio, z * ratio);
        }
    }
}