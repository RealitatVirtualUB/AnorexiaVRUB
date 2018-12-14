// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ColorInterestShader"{
	Properties
	{
		_Pos("WorldPos", vector) = (0.0, 0.0, 0.0, 0.0)
		_Dist("Distance", float) = 5.0
		_MainTex("Texture", 2D) = "white" {}
		_AfectionColor("afectionColor", Color) = (0,0,0,0)

	}
		SubShader
	{
		
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				// We compute the world position to use it in the fragment function
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			float4 _Pos;
			sampler2D _MainTex;
			sampler2D _SecondayTex;
			float4 _AfectionColor;
			float _Dist;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv);
				// Depending the distance from the player, we use a different texture
				if (distance(_Pos.xyz, i.worldPos.xyz) > _Dist)
					return c;
				else {
					float4 finalColor = _AfectionColor;
					finalColor.rgb = lerp(finalColor, (0, 0, 0), distance(_Pos.xyz, i.worldPos.xyz)/_Dist);
					c += finalColor;
					//c.a += finalColor.a;
					return c;
				}
			}

			ENDCG
		}
	}
}
