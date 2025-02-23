﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "CookappsPlay/Grayscale"
{
 Properties
 {
  _MainTex ("Texture", 2D) = "white" {}
  _bwBlend ("Black & White" , Range(0,1)) = 1
 }
 SubShader
 {
  // No culling or depth
  Cull Off ZWrite Off ZTest Always

  Pass
  {
   CGPROGRAM
   #pragma vertex vert
   #pragma fragment frag
   
   #include "UnityCG.cginc"

   struct appdata
   {
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
   };

   struct v2f
   {
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
   };

   v2f vert (appdata v)
   {
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
   }
   
   sampler2D _MainTex;
   uniform float _bwBlend;

   fixed4 frag (v2f i) : SV_Target
   {
    fixed4 col = tex2D(_MainTex, i.uv);
    half3 gray = col.r * 0.3 + col.g*0.59 + col.b * 0.11;
    col.rgb = lerp(col.rgb, gray,_bwBlend );
    return col;
   }
   ENDCG
  }
 }
}



