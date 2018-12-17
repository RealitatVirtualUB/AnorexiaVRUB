// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ColorInterestShader"{
	Properties
	{
		_Pos("WorldPos", vector) = (0.0, 0.0, 0.0, 0.0)
		_Dist("Distance", float) = 5.0
		_SpecMap("specularMap", 2D) = "white" {}
		_MainTex("Texture", 2D) = "white" {}
		_BumpMap("NormalMap", 2D) = "bump" {}
		_BumpDepth("bumpDepth", Range(-2, 2)) = 1
		_RimColor("rimColor", Color) = (1.0, 1.0,1.0,1.0)
		_RimPower("rimPower", Range(0.1,10.0)) = 3.0
		_AfectionColor("afectionColor", Color) = (0,0,0,0)

	}
		SubShader
	{
		Pass
		{
			Tags {"LightMode" = "ForwardBase"}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#pragma exclude_renderers flash
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc" // for _LightColor0

			struct VertexInput {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				float4 tangent : TANGENT;
			};

			struct VertexOutput {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
				float3 normalWorld : TEXCOORD2;
				float3 tangentWorld : TEXCOORD3;
				float3 binormalWorld : TEXCOORD4;
				float4 diff : COLOR0; // diffuse lighting color
			};

			//struct v2f {
			//	float4 pos : SV_POSITION;
			//	float2 uv : TEXCOORD0;
			//	float4 worldPos : TEXCOORD1;
			//	fixed4 diff : COLOR0; // diffuse lighting color
			//};

			//float4 _LightColor0;

			VertexOutput vert(VertexInput v)
			{
				VertexOutput o;
				o.normalWorld = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
				o.tangentWorld = normalize(mul(unity_ObjectToWorld, v.tangent).xyz);
				o.binormalWorld = normalize(cross(o.normalWorld, o.tangentWorld) * v.tangent.w);
				// We compute the world position to use it in the fragment function
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				//o.pos = UnityObjectToClipPos(v.vertex);
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				//o.diff = nl * _LightColor0;
				return o;
			}

			float4 _Pos;
			sampler2D _MainTex;
			sampler2D _BumpMap;
			sampler2D _SpecMap;
			float4 _AfectionColor;
			float4 _RimColor;
			float _RimPower;
			float _BumpDepth;
			float _Dist;

			fixed4 frag(VertexOutput i) : COLOR
			{
				//light variables
				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
				float3 lightDirection;
				float atten;

				if (_WorldSpaceLightPos0.w == 0.0) {
					atten = 1.0;
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				}
				else {
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.worldPos.xyz;
					float distance = length(fragmentToLightSource);
					atten = 1.0 / distance;
					lightDirection = normalize(fragmentToLightSource);
				}

				//Texture maps
				float4 mainTex = tex2D(_MainTex, i.uv);
				float4 normalTex = tex2D(_BumpMap, i.uv);
				float3 specularTex = tex2D(_SpecMap, i.uv).xyz;
				//unpacknormal function
				float3 localCoords = float3(2.0 * normalTex.ag - float2(1.0,1.0),0.0 );
				//localCoords.z = 1.0 - 0.5 * dot(localCoords, localCoords);
				localCoords.z = _BumpDepth;

				//normal transpose matrix
				float3x3 local2WorldTranspose = float3x3(
					i.tangentWorld,
					i.binormalWorld,
					i.normalWorld
					);

				//calculate normal direction
				float3 normalDirection = normalize(mul(localCoords, local2WorldTranspose));

				//lightning
				float3 diffuseReflection = atten * _LightColor0.xyz * saturate(dot(normalDirection, lightDirection));
				//float3 diffuseReflection = i.diff.rgb;

				//float3 specularReflection = diffuseReflection * specularTex * pow(saturate(dot(reflect(-lightDirection, normalDirection), viewDirection)), 10);
				float3 specularReflection = diffuseReflection * specularTex;
				//rim lightning
				float rim = 1 - saturate(dot(viewDirection, normalDirection));
				float3 rimLightning = saturate(dot(normalDirection, lightDirection) * _RimColor.xyz * _LightColor0.xyz * pow(rim, _RimPower));
				float3 lightFinal = UNITY_LIGHTMODEL_AMBIENT.xyz + diffuseReflection + specularReflection + rimLightning;

				fixed4 c = mainTex;
				c.rgb *=lightFinal;
				//c.a = 1.0;
				//c *= i.diff;
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
				//return c;
			}

			ENDCG
		}

		Pass
		{
			Tags {"LightMode" = "ForwardAdd"}
			Blend One One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#pragma exclude_renderers flash
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc" // for _LightColor0

			struct VertexInput {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				float4 tangent : TANGENT;
			};

			struct VertexOutput {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
				float3 normalWorld : TEXCOORD2;
				float3 tangentWorld : TEXCOORD3;
				float3 binormalWorld : TEXCOORD4;
				float4 diff : COLOR0; // diffuse lighting color
			};

			//struct v2f {
			//	float4 pos : SV_POSITION;
			//	float2 uv : TEXCOORD0;
			//	float4 worldPos : TEXCOORD1;
			//	fixed4 diff : COLOR0; // diffuse lighting color
			//};

			//float4 _LightColor0;

			VertexOutput vert(VertexInput v)
			{
				VertexOutput o;
				o.normalWorld = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
				o.tangentWorld = normalize(mul(unity_ObjectToWorld, v.tangent).xyz);
				o.binormalWorld = normalize(cross(o.normalWorld, o.tangentWorld) * v.tangent.w);
				// We compute the world position to use it in the fragment function
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				//o.pos = UnityObjectToClipPos(v.vertex);
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			float4 _Pos;
			sampler2D _MainTex;
			sampler2D _BumpMap;
			sampler2D _SpecMap;
			float4 _AfectionColor;
			float4 _RimColor;
			float _RimPower;
			float _BumpDepth;
			float _Dist;

			float4 frag(VertexOutput i) : COLOR
			{
				//light variables
				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
				float3 lightDirection;
				float atten;

				if (_WorldSpaceLightPos0.w == 0.0) {
					atten = 1.0;
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				}
				else {
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.worldPos.xyz;
					float distance = length(fragmentToLightSource);
					atten = 1.0 / distance;
					lightDirection = normalize(fragmentToLightSource);
				}

				//Texture maps
				float4 mainTex = tex2D(_MainTex, i.uv);
				float4 normalTex = tex2D(_BumpMap, i.uv);
				float3 specularTex = tex2D(_SpecMap, i.uv).xyz;
				//unpacknormal function
				float3 localCoords = float3(2.0 * normalTex.ag - float2(1.0,1.0),0.0);
				//localCoords.z = 1.0 - 0.5 * dot(localCoords, localCoords);
				localCoords.z = _BumpDepth;

				//normal transpose matrix
				float3x3 local2WorldTranspose = float3x3(
					i.tangentWorld,
					i.binormalWorld,
					i.normalWorld
					);

				//calculate normal direction
				float3 normalDirection = normalize(mul(localCoords, local2WorldTranspose));

				//lightning
				float3 diffuseReflection = atten * _LightColor0.xyz * saturate(dot(normalDirection, lightDirection));
				//float3 diffuseReflection = i.diff.rgb;
				//float3 specularReflection = diffuseReflection * specularTex * pow(saturate(dot(reflect(-lightDirection, normalDirection), viewDirection)), 10);
				float3 specularReflection = diffuseReflection * specularTex;
				//rim lightning
				float rim = 1 - saturate(dot(viewDirection, normalDirection));
				float3 rimLightning = saturate(dot(normalDirection, lightDirection) * _RimColor.xyz * _LightColor0.xyz * pow(rim, _RimPower));
				float3 lightFinal =diffuseReflection + specularReflection + rimLightning;

				fixed4 c = mainTex;
				c.rgb *= lightFinal;
			
				//c *= i.diff;
				// Depending the distance from the player, we use a different texture
				if (distance(_Pos.xyz, i.worldPos.xyz) > _Dist)
					return c;
				else {
					float4 finalColor = _AfectionColor;
					finalColor.rgb = lerp(finalColor, (0, 0, 0), distance(_Pos.xyz, i.worldPos.xyz) / _Dist);
					c += finalColor;
					//c.a += finalColor.a;
					return c;
				}
			}

			ENDCG
		}
	}
}
