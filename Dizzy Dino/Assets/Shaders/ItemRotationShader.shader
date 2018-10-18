Shader "Unlit/ItemRotationShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _XYaxis ("Rotate on XY axis", Float) = 1.0
    }
	SubShader
	{
		Tags {
            "RenderType" = "Opaque"
        }      
        LOD 200
        
		CGPROGRAM
        #pragma target 3.0
        #include "UnityPBSLighting.cginc"
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow
        
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        half4 _Color;
        
        float _XYaxis;
        
        void vert (inout appdata_full v)
        {
            float speed = 25;
            float delta = speed * _Time;
            if (_XYaxis == 1.0) {
                float2x2 mat = float2x2(cos(delta), -sin(delta), sin(delta), cos(delta));
                v.vertex.xy = mul(v.vertex.xy, mat);
                v.normal.xy = mul(v.normal.xy, mat);
                v.tangent.xy = mul(v.tangent.xy, mat);
            } else {
                delta = -delta;
                float2x2 mat = float2x2(cos(delta), -sin(delta), sin(delta), cos(delta));
                v.vertex.xz = mul(v.vertex.xz, mat);
                v.normal.xz = mul(v.normal.xz, mat);
                v.tangent.xz = mul(v.tangent.xz, mat);
            }
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }

        
		ENDCG
	}
    Fallback "Diffuse"

}
