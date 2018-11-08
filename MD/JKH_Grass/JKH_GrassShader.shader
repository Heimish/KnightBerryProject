Shader "Custom/JKH_GrassShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[Normal] _BumpMap("노말맵", 2D) = "Bump" {}
		_Metal("메탈릭(a = 스무스니스)", 2D) = "White"{}
		_MetBemet("메탈릭", Range(0,1)) = 0
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		[HideInInspector]_cutout("알파테스트" ,float) = 0.5
		_grassmask("마스크텍스쳐", 2D) = "white" {}
		_Radial("중심과의 거리", float) = 1
		_Wavedistance("이동거리", Vector) = (0.01,0.03,0.01,0)
		_Wavesacle("웨이브크기",float) = 1
		_Wavespeed("웨이브속도", Vector) = (1,1,1,0)
		
	}
	SubShader {
		Tags { "RenderType"="TransparentCutout" "Queue" = "AlphaTest" }

		cull off

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows vertex:vert alphatest:_cutout addshadow
		#pragma target 5.0

		  
	

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _grassmask;
		sampler2D _Metal;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_Metal;
			float3 viewDir;
		};

		float _Radial;
		float3 _Wavedistance;
		float _Wavesacle;
		float3 _Wavespeed;
		float _MetBemet;


			void vert (inout appdata_full v) 
		{
		float3 mask = tex2Dlod(_grassmask,v.texcoord);

		float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
		float3 vertexPos = mul(unity_WorldToObject, float4(worldPos, 1.0f));
		float3 Ao = saturate(distance(vertexPos,float3(0.2,0,2)) - _Radial);
		float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
        float Rim = 1 - saturate(dot(v.normal, viewDir));
   		
		v.vertex.x += cos(worldPos.y * _Wavesacle + _Time.y * _Wavespeed.x) * _Wavedistance.x * Ao * Rim * mask.b;
		v.vertex.y += cos(worldPos.y * _Wavesacle - _Time.y * _Wavespeed.y) * _Wavedistance.y * Ao * Rim * mask.b;
		v.vertex.z += cos(worldPos.xz * _Wavesacle + _Time.y * _Wavespeed.z) * _Wavedistance.z * Ao * Rim * mask.b;

		v.vertex.z += ((mask.r *  sin(mask.r + _Time.y)) * 0.15) * sin((mask.g * 2 -1)  + _Time.y * 1) * cos((worldPos * 2 -1) + _Time.y * 2);
		v.vertex.x += ((mask.r *  cos(mask.r + _Time.y)) * 0.2) * cos((mask.g * 2 -1) + _Time.y * 3) * sin((worldPos * 2 -1) + _Time.y * 1);
		
		}


		half _Glossiness;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Metallic = tex2D(_Metal,IN.uv_Metal).r * _MetBemet;
			o.Smoothness = tex2D(_Metal,IN.uv_Metal).a * _Glossiness;
			o.Normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Legacy Shaders/Transparent/Cutout/Diffuse"
}
