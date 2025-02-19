using System;
using Trucking.Common;
using UnityEngine;

namespace Trucking
{
	public class ColorManager : MonoSingleton<ColorManager> {

		[SerializeField]
		public ColorValue[] ColorList;

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
		
		}

		[Serializable]
		public class ColorValue
		{
			// public string name;
			public Color color;
			public Material roadMaterial;
			public Material truckMaterial;
		}
	}
}
