using System.Collections.Generic;
using System.Linq;
using Coffee.UIExtensions;
using I2.Loc;
using SoftMasking;
using Trucking.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Namespace
{
    public static class TruckerTool
    {
        [MenuItem("Trucking/Delete Save", false, -1)]
        private static void DeleteSave()
        {
            PlayerPrefs.DeleteKey(UserDataManager.SaveKey);            
        }
        
        [MenuItem("Trucking/Set English", false, -1)]
        private static void SetEnglish()
        {
            LocalizationManager.CurrentLanguage = "English";
        }

        [MenuItem("Trucking/Find Component", false, -1)]
        private static void FindComponent()
        {
            List<Mask> arrComponent = ((Mask[])Resources.FindObjectsOfTypeAll(typeof(Mask))).ToList();

            foreach (var component in arrComponent) 
            {
                Utilities.LogParent(component.transform, component.name);
            }
        }
        
        [MenuItem("Trucking/Find Component<Soft Mask>", false, -1)]
        private static void FindComponent2()
        {
            List<SoftMask> arrComponent = ((SoftMask[])Resources.FindObjectsOfTypeAll(typeof(SoftMask))).ToList();

            foreach (var component in arrComponent) 
            {
                Utilities.LogParent(component.transform, component.name);
            }
        }



        
//        [MenuItem("Trucking/Find TextMeshPro", false, -1)]
//        private static void FindTextMeshPro()
//        {
//            TMPro.TextMeshProUGUI[] uiTextsUGUI = (TMPro.TextMeshProUGUI[])Resources.FindObjectsOfTypeAll(typeof(TMPro.TextMeshProUGUI));
//            if (uiTextsUGUI != null && uiTextsUGUI.Length > 0)
//            {
//                foreach (var textMeshProUgui in uiTextsUGUI)
//                {
//                    if (textMeshProUgui.GetComponent<Localize>() == null)
//                    {
//                        Debug.Log($"FindTextMeshPro : {textMeshProUgui.name}, {textMeshProUgui.transform.parent.name}");
//
//                        Localize localize = textMeshProUgui.gameObject.AddComponent<Localize>();
//                        localize.mLocalizeTarget = new LocalizeTarget_TextMeshPro_UGUI();
//                        
////                        localize.SetTerm();
//                        
//                        LogParent(textMeshProUgui.transform);
//                        break;
//                    }
//                }
//            }
//        }

        private static void LogParent(Transform trs)
        {
            if (trs.parent != null)
            {
                Debug.Log(trs.parent.name);
                LogParent(trs.parent);
            }
        }
        
        
    }
}