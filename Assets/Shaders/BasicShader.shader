Shader "Custom/BasicShader"
{
	Properties
	{
		_SplatMap ("Texture", 2D) = "white" {}
		_MainTex ("Texture", 2D) = "white" {}
		_SecondTex ("Texture 2", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{
			"RenderType"="Opaque"
		}
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uvSplat : TEXCOORD0;
				float2 uvMain : TEXCOORD1;
				float2 uvSecond : TEXCOORD2;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uvSplat : TEXCOORD0;
				float2 uvMain : TEXCOORD1;
				float2 uvSecond : TEXCOORD2;
			};

			sampler2D _SplatMap, _MainTex, _SecondTex;
			float4 _SplatMap_ST, _MainTex_ST, _SecondTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uvSplat = TRANSFORM_TEX(v.uvSplat, _SplatMap);
				o.uvMain = TRANSFORM_TEX(v.uvMain, _MainTex);
				o.uvSecond = TRANSFORM_TEX(v.uvSecond, _SecondTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 splatVal = tex2D(_SplatMap, i.uvSplat);
				fixed4 mainCol = tex2D(_MainTex, i.uvMain);
				fixed4 secondCol = tex2D(_SecondTex, i.uvSecond);
				fixed4 finalCol = lerp(mainCol, secondCol, splatVal.x);
				return finalCol;
			}
			ENDCG
		}
	}
}
