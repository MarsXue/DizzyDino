Shader "Unlit/WaterShader"
{
	Properties
	{
	    _MainTex ("Texture", 2D) = "white" {}
        _AltTex("Alternative texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Speed ("Speed", Range(0, 1)) = 1
        _Wavelength ("Wavelength", Range(0, 1)) = 1
        _Amplitude ("Amplitude", Range(0, 1)) = 1
        _Foam ("Foam width", Range(0, 3)) = 1
	}
	SubShader
	{
		Tags { 
            "RenderType" = "Opaque"
            "Queue" = "Transparent"
        }
		LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
                float4 screenPosition : TEXCOORD1; 
			};
            
            sampler2D _MainTex, _AltTex;
            float4 _MainTex_ST;
            
            float4 _Color;
            uniform sampler2D _CameraDepthTexture;
            float _Wavelength, _Amplitude, _Speed, _Foam;
			
			v2f vert (appdata v)
			{
				v2f o;
                
                // Alternative texture overlay
                float4 tex = tex2Dlod(_AltTex, float4(v.uv.xy, 0, 0));
                
                // Texture overlay
                float offset = v.vertex.x * v.vertex.z * _Wavelength * tex;
                float movement = _Time.z * _Speed + offset;
                v.vertex.y += sin(movement) * _Amplitude;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                o.screenPosition = ComputeScreenPos(o.vertex);
                
				UNITY_TRANSFER_FOG(o,o.vertex);
                
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                
                half depth = 
                    LinearEyeDepth(
                        SAMPLE_DEPTH_TEXTURE_PROJ(
                            _CameraDepthTexture, 
                            UNITY_PROJ_COORD(i.screenPosition)));
                 
                half4 foamColor = 1 - saturate(_Foam * depth - i.screenPosition.w);
                 
                col += foamColor * _Color;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
