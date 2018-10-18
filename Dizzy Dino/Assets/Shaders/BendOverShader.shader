Shader "Unlit/BendOverShader"
{
	Properties
	{
        _Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
        _Direction ("Bend Direction", Float) = 1.0
        _Height ("Height", Float) = 2.0
        _Size ("Plane Size", Float) = 1.0
        _Offset ("Plane Offset", Float) = 0.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
  
		CGPROGRAM
        #pragma target 3.0
        #include "UnityPBSLighting.cginc"
        #pragma surface surf Lambert noforwardadd vertex:vert addshadow
       
	
		#include "UnityCG.cginc"
        
        
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            fixed3 color : COLOR;
        };

        half _Glossiness;
        half _Metallic;
        half4 _Color;
        
        float _Direction;
        float _Height;
        float _Size;
        float _Offset;     

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
		};
		
		void vert (inout appdata_full o)
		{
            float pi = 3.1415926535;
            float offset = (o.vertex.z + _Offset) / _Size;
            float displacement = (cos((offset + _Direction) * pi) + 1.0)/2.0;
            o.vertex.y -= displacement * _Height;
		}
		
            
        void surf(Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = IN.color.rgb * _Color;
        }

		ENDCG
		
	}
}
