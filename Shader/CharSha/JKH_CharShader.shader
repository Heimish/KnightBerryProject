Shader "Custom/JKH_CharShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[Toggle]_Mtl("Metalic",Range(0,1)) = 0
		_SpecPow("Specular Power", float) = 100
		_BumpMap("노말맵", 2D) = "Bump" {}
		_Rimpow ("림 두께", float) = 1
		_Fakecolor("가짜스페큘러 색깔", color) = (1,1,1,1)
		_FakeSpecpow("가짜스페큘러 두께", float) = 100
		_RampTex("RampTex", 2D) = "white" {}
		_MainTex2("에미션", 2D) = "white" {}
		_EmiColor("에미션컬러", color) = (0,0,0,1)

	}
	SubShader {
		Tags { "RenderType"="Opaque"}


		CGPROGRAM
		#pragma surface surf JKH fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _RampTex;
		sampler2D _MainTex2;
		float4 _Color;
		float _SpecPow;
		float _Mtl;
		float _Rimpow;
		float _FakeSpecpow;
		float4 _Fakecolor;
		float4 _EmiColor;


		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;
			float2 uv_MainTex2;
		};


		void surf (Input IN, inout SurfaceOutput o) {
			float4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Emission = tex2D(_MainTex2,IN.uv_MainTex2) * _EmiColor;
			o.Normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));
			o.Albedo = c.rgb;
			o.Alpha = 1;
			o.Gloss = c.a;
		}

		float4 LightingJKH (SurfaceOutput o, float3 lightDir, float3 viewDir, float atten){

		//램버트 연산
		float3 Diffusecolor;
		float NdotL =dot(o.Normal, lightDir) *0.5 + 0.5;
		Diffusecolor = NdotL * o.Albedo.rgb * _LightColor0.rgb * atten;

		//ramp 연산
		float3 Rampcol;
		float4 ramp = tex2D(_RampTex,float2(NdotL,0.5));
		Rampcol = ramp.rgb * o.Albedo;

		//스페큘러 연산
		float3 SpecColor;
		float3 H = normalize(lightDir + viewDir);
		float spec = saturate(dot(H, o.Normal));
		spec = pow(spec, _SpecPow);
		if(_Mtl > 0)
		{
		SpecColor = spec * o.Gloss * o.Albedo.rgb;
		}

		else
		{
		SpecColor = spec  * o.Gloss * _LightColor0;
		}
		
		//림라이트 연산
		float3 Rimcolor;
		float Rim = abs(dot(viewDir, o.Normal));
		float invrim = 1 - Rim;
		Rimcolor = saturate(pow(invrim, _Rimpow)) * o.Albedo.rgb * NdotL;


		//가짜 스페큘러 연산
		float3 FakeSpec;
		FakeSpec = saturate(pow(Rim, _FakeSpecpow)) * _Fakecolor;


		//에미션 연산
		
		float3 Emicol;
		Emicol = saturate(o.Emission);



		//최종 연산
		float4 Final;
		Final.rgb = Diffusecolor.rgb + SpecColor.rgb + Rimcolor.rgb + FakeSpec.rgb + Rampcol.rgb + Emicol.rgb;
		Final.a = o.Alpha;



			return Final;
		}


		ENDCG
	}
	FallBack "Diffuse"
}
