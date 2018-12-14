Shader "Custom/LightParter" {
	Properties {
		//base altered color
		_Color ("AlteredColor", Color) = (1,1,1,1)
		//base effect color
		_EffectColor("EffectColor", Color) = (0,0,0,0)
		//main texture
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		//specular texture
		_SpecularTex("Specular", 2D) = "white" {}
		//normal texture
		_NormalTex("Normals", 2D) = "bump" {}
		//effect patern
		_EffectTexture("effectPatron", 2D) = "white" {}
		//gloss value
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//metallic value
		_Metallic ("Metallic", Range(0,1)) = 0.0
		//radius of effect
		_Radius("Radius", Range(0,100)) = 0.0
		//center of the effect sphere
		_WorldPosition("worldPosition", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#pragma surface surf Lambert vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SpecularTex;
		sampler2D _NormalTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SpecularTex;
			float2 uv_NormalTex;
			float4 vertColor;
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
		
		void vert(inout appdata_full v, out Input o){
			o.vertColor = v.color;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb ;
		
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
