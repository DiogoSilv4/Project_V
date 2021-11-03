Shader "Unlit/Drawable"
{
	Properties
	{
		_MainTex ("DrawTexture", 2D) = "black" {}
		_BaseTex ("BaseTex", 2D) = "black" {}
	}
	SubShader
	{
		//Tags {"RenderType" = "Opaque"}
		Tags {"RenderType" = "Transparent"}
		//ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		//Cull front
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert alpha
			#pragma fragment frag alpha
			
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv2 : TEXCOORD1;
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
				o.uv = v.uv2;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}



			

			sampler2D _MainTex;
			sampler2D _BaseTex;

			
			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture

				fixed4 white = fixed4(1,1,1,1);
				fixed4 blendColour = tex2D(_MainTex, i.uv);
				fixed4 textureColour = tex2D(_BaseTex, i.uv);

				float alpha = blendColour.a;

				if ( blendColour.r >= white.r &&
					 blendColour.g >= white.g &&
					 blendColour.b >= white.b )
				{
					return textureColour;
				}
				else
				{
					return blendColour;
				}
			}
			ENDCG
		}
	}
}
