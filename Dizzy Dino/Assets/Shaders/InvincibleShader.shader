﻿Shader "Unlit/InvincibleShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
            // indicate that our pass is the "base" pass in forward
            // rendering pipeline. It gets ambient and main directional
            // light data set up; light direction in _WorldSpaceLightPos0
            // and color in _LightColor0
            Tags {"LightMode" = "ForwardBase"}
            
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
                fixed3 localPosition : TEXCOORD1;
                fixed4 diff : COLOR0; // diffuse lighting color
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
            inline fixed4 RGBtoHSL(fixed4 rgb) {
                fixed4 hsl = fixed4(0.0, 0.0, 0.0, rgb.w);
                
                fixed vMin = min(min(rgb.x, rgb.y), rgb.z);
                fixed vMax = max(max(rgb.x, rgb.y), rgb.z);
                fixed vDelta = vMax - vMin;
                
                hsl.z = (vMax + vMin) / 2.0;
                
                if (vDelta == 0.0) {
                    hsl.x = hsl.y = 0.0;
                }
                else {
                    if (hsl.z < 0.5) hsl.y = vDelta / (vMax + vMin);
                    else hsl.y = vDelta / (2.0 - vMax - vMin);
                    
                    float rDelta = (((vMax - rgb.x) / 6.0) + (vDelta / 2.0)) / vDelta;
                    float gDelta = (((vMax - rgb.y) / 6.0) + (vDelta / 2.0)) / vDelta;
                    float bDelta = (((vMax - rgb.z) / 6.0) + (vDelta / 2.0)) / vDelta;
                    
                    if (rgb.x == vMax) hsl.x = bDelta - gDelta;
                    else if (rgb.y == vMax) hsl.x = (1.0 / 3.0) + rDelta - bDelta;
                    else if (rgb.z == vMax) hsl.x = (2.0 / 3.0) + gDelta - rDelta;
                    
                    if (hsl.x < 0.0) hsl.x += 1.0;
                    if (hsl.x > 1.0) hsl.x -= 1.0;
                }
                
                return hsl;
            }

            inline fixed hueToRGB(float v1, float v2, float vH) {
                if (vH < 0.0) vH+= 1.0;
                if (vH > 1.0) vH -= 1.0;
                if ((6.0 * vH) < 1.0) return (v1 + (v2 - v1) * 6.0 * vH);
                if ((2.0 * vH) < 1.0) return (v2);
                if ((3.0 * vH) < 2.0) return (v1 + (v2 - v1) * ((2.0 / 3.0) - vH) * 6.0);
                return v1;
            }

            inline fixed4 HSLtoRGB(fixed4 hsl) {
                fixed4 rgb = fixed4(0.0, 0.0, 0.0, hsl.w);
                
                if (hsl.y == 0) {
                    rgb.xyz = hsl.zzz;
                }
                else {
                    float v1;
                    float v2;
                    
                    if (hsl.z < 0.5) v2 = hsl.z * (1 + hsl.y);
                    else v2 = (hsl.z + hsl.y) - (hsl.y * hsl.z);
                    
                    v1 = 2.0 * hsl.z - v2;
                    
                    rgb.x = hueToRGB(v1, v2, hsl.x + (1.0 / 3.0));
                    rgb.y = hueToRGB(v1, v2, hsl.x);
                    rgb.z = hueToRGB(v1, v2, hsl.x - (1.0 / 3.0));
                }
                
                return rgb;
            }
            
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.localPosition = v.vertex.xyz;
                
                // get vertex normal in world space
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                // dot product between normal and light direction for
                // standard diffuse (Lambert) lighting
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                // factor in the light color
                o.diff = nl * _LightColor0;
                
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                half spread = 40;
                half speed = 25;
                half timeOffset = 0.0;
                fixed saturation = 0.8;
                fixed luminosity = 0.5;
                fixed intensity = 0.2;
            
                fixed3 lPos = i.localPosition / spread;
                half time = _Time.y * speed / spread;
                half tOffset = time + timeOffset;
                
                fixed hue = (-lPos.z) / 2.0;
                hue += time;
                while (hue < 0) hue += 1;
                while (hue > 1) hue -= 1;
                
                fixed4 hsl = fixed4(hue, saturation, luminosity, 1.0);
                
                // sample texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // multiply by lighting
                col *= i.diff;
                
                return col + HSLtoRGB(hsl) * intensity;
            }
            
            
			ENDCG
		}
	}
}