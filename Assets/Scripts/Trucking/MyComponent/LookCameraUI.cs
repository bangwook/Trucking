using UnityEngine;

namespace Trucking.MyComponent
{
    public class LookCameraUI : MonoBehaviour
    {
        private void LateUpdate()
        {
            if (Camera.main != null)
            {
                transform.rotation = Camera.main.transform.rotation;    
            }
        }
    }   
}