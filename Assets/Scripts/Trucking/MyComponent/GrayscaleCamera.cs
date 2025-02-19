using UnityEngine;

namespace Trucking.MyComponent
{
	public class GrayscaleCamera : MonoBehaviour
	{
		public float intensity;
		public Material material;

		// Use this for initialization
		void Awake()
		{

			material = new Material(Shader.Find("CookappsPlay/Grayscale"));

		}

		// Update is called once per frame
		void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (intensity == 0)
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				material.SetFloat("_bwBlend", intensity);
				Graphics.Blit(source, destination, material);
			}
		}
	}
}