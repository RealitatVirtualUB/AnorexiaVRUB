// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OclussionDistanceShader" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Fader("Fader Slider", Float) = 0
		_MinOpacity("Minimum Opacity", Range(0,1)) = 0.1
	}
		SubShader{
			Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
			LOD 200
			ZWrite Off
			ZTest Less
			Cull Back

			CGPROGRAM
			#pragma surface surf Lambert alpha


			sampler2D _MainTex;
			float _Fader;
			float _MinOpacity;

			struct Input {
				float2 uv_MainTex;
				float3 worldPos;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				half4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;
				float dist = distance(IN.worldPos, _WorldSpaceCameraPos);
				if (dist >= _Fader) o.Alpha = c.a;
				else o.Alpha = 0;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
