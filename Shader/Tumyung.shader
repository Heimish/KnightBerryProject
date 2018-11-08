Shader "JKH/Tumyung" {
	Properties {
		[HDR]_Color ("Color", Color) = (1,1,1,1)
		[HDR]_RimCol ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex3 ("굴절", 2D) = "white" {}
		_MainTex2 ("큐브굴절", 2D) = "white" {}
		_Cube("큐부", Cube) = ""{}
		_Cubal("큐브밝기", float) = 1
		[HDR]_CoreCol("코어 색",color) = (1,1,1,1)
		_Onoff("켜고끄기", Range(0,1)) = 1
		_Speed("커스틱속도", Range(0,1)) = 0
		_Speed2("커스틱속도", Range(0,1)) = 0
		_Speed3("커스틱속도", Range(0,1)) = 0
		_Speed4("커스틱속도", Range(0,1)) = 0
		



	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Sujung fullforwardshadows alpha:fade 
		#pragma target 3.0

		
		sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _MainTex3;
		samplerCUBE _Cube;
		float _Cubal;
		float _Onoff;
		float _Speed;
		float _Speed2;
		float _Speed3;
		float _Speed4;
		float4 _CoreCol;
		float4 _RimCol;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex2;
			float2 uv_MainTex3;
			float3 worldRefl;
			float3 viewDir;
			INTERNAL_DATA
		};

		fixed4 _Color;


		void surf (Input IN, inout SurfaceOutput o) {
			float4 t = tex2D(_MainTex3,IN.uv_MainTex3);
			fixed4 c = tex2D (_MainTex, float2(IN.uv_MainTex.x + t.r + _Time.y *_Speed , IN.uv_MainTex.y + t.r - _Time.y* _Speed2)) * _Color;
			fixed4 g = tex2D (_MainTex, float2(IN.uv_MainTex.x + t.r - _Time.y *_Speed3 , IN.uv_MainTex.y + t.r + _Time.y*_Speed4)) * _Color;
			float4 d = tex2D (_MainTex2,IN.uv_MainTex2);
			float3 rim = 1 - saturate(dot(IN.viewDir, o.Normal));
			float3 rimpow = pow(rim, 2);
			float4 cube = texCUBE(_Cube,IN.worldRefl + d.r);
			o.Emission = cube * _Cubal * rim * _RimCol;
			o.Albedo = (c.rgb +  g.rgb) / 2;
			o.Alpha = pow(1 - rimpow, 500) + c.r + rimpow;
		}

		float4 LightingSujung(SurfaceOutput s, float lightDir,float3 viewDir, float atten)
		{

			float4 CoreCol;
			CoreCol.rgb = _CoreCol;
			CoreCol.a = 1;
			

			float3 rimu;
			rimu =  1 - saturate(dot(viewDir, s.Normal));
			rimu = pow(1 - rimu, 15);

			float4 final;
			final.rgb = s.Albedo + rimu * CoreCol;
			final.a = s.Alpha * _Onoff;
			return final;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
