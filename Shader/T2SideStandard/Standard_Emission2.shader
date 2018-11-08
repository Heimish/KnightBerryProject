Shader "JKH/Standard_Emission" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MetallicMap("Metalmap", 2D) ="black" {}
		_Metallic ("Metallic", Range(0,1)) = 0
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_BumpMap("Normal",2D) = "Bump" {}
		_AO("AO" ,2D) = "white" {}
		_Emission ("Emission" ,2D) = "black" {}
		[HDR]_EmiColor("Emission color", color) = (0,0,0,0)
		_GgamBack("깜빡임 속도",float)=0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _AO;
		sampler2D _Emission;
		sampler2D _MetallicMap;


		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_MetallicMap;

		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float4 _EmiColor;
		float _GgamBack;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Emission = (tex2D(_Emission,IN.uv_MainTex)) * abs(sin(_Time.y*_GgamBack)) * _EmiColor;
			o.Normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));
			o.Occlusion = tex2D (_AO, IN.uv_MainTex);
			o.Albedo = c.rgb;
			float4 d = tex2D(_MetallicMap, IN.uv_MetallicMap) * _Metallic;
			o.Metallic = d.r;
			o.Smoothness = d.a * _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
