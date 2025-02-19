using Trucking.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UIBlockTouch : MonoSingleton<UIBlockTouch>
    {
        public Image blackPanel;

        public void ShowClear()
        {
            gameObject.SetActive(true);
            blackPanel.color = Color.clear;
        }

        public void ShowBlack(float alpha = 0.5f)
        {
            gameObject.SetActive(true);
            blackPanel.color = new Color(0, 0, 0, alpha);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}