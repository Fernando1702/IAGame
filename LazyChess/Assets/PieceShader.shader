Shader "Unlit/PieceShader"
{
	Properties
	{
		_Color ("Texture", Color) = (1,1,1,1)
		_OutlineColor("Outline Color", Color)  =(0,0,0,0)
		_Outline("Outline", float) = 0.1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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

			fixed4 _Color;	
			
			v2f vert (appdata v)
			{
				v2f o;
				
				o.vertex = UnityObjectToClipPos(v.vertex + float3(0, 0.1, 0) * sin(v.vertex.x * 20 + _Time.y)*  sin(v.vertex.x * 20 + _Time.y));

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _Color;

				return col;
			}
			ENDCG
		}
	
	
		Pass
		{

			cull front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;

				float4 vertex : SV_POSITION;
			};

			fixed4 _OutlineColor;
			float _Outline;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex + v.normal * _Outline + float3(0, 0.1, 0) * sin(v.vertex.x * 20 + _Time.y)*  sin(v.vertex.x * 20 + _Time.y));

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _OutlineColor;

				return col;
			}
			ENDCG
		}
	}
}
