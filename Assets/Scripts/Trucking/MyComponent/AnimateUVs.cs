using UnityEngine;

namespace Trucking.MyComponent
{

  public class AnimateUVs : MonoBehaviour
  {
 
       public Material myMat;
     
       public float scrollSpeed;
     
       private float offsetX;
     
       private Vector2 offsetVector;
          
       // Update is called once per frame
       void Update()
       {
     
        offsetX += (float) (scrollSpeed * Time.deltaTime / 10.0);
     
        offsetVector = new Vector2(offsetX, 0);
     
        myMat.SetTextureOffset("_MainTex", offsetVector);
     
       }

    }
}
