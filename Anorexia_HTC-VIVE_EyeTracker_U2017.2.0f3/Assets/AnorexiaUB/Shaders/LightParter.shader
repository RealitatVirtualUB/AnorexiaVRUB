Shader "Custom/LightParter" {
	Properties {
		_Color ("AlteredColor", Color) = (1,1,1,1)
		_EffectColor("EffectColor", Color) = (0,0,0,0)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SpecularTex("Specular", 2D) = "white" {}
		_NormalTex("Normals", 2D) = "bump" {}
		_EffectTexture("effectPatron", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Radius("Radius", Range(0,100)) = 0.0
		_WorldPosition("worldPosition", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SpecularTex;
		sampler2D _NormalTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SpecularTex;
			float2 uv_NormalTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _EffectColor;
		float4 worldPosition;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
		
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
