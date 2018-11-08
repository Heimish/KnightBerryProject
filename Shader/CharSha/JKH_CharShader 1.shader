Shader "JKH/JKH_CharShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[Toggle]_Mtl("Metalic",Range(0,1)) = 0
		_SpecPow("Specular Power", Range(10,150)) = 100
		_BumpMap("노말맵", 2D) = "Bump" {}
		_Rimpow ("림 두께", Range(1,30)) = 1
		_Fakecolor("가짜스페큘러 색깔", color) = (1,1,1,1)
		_FakeSpecpow("가짜스페큘러 두께", Range(10,200)) = 100
		_Lower("로우 라이트세기", Range(0,1)) = 0
		_RimbalGGi("림세기", float) = 1
		_viwecol("뷰빛색" , color) = (1,1,1,1)

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		cull off
		CGPROGRAM
		#pragma surface surf JKH fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float4 _Color;
		float _SpecPow;
		float _Mtl;
		float _Rimpow;
		float _FakeSpecpow;
		float4 _Fakecolor;
		float _Lower;
		float _RimbalGGi;
		float4 _viwecol;




		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;
			float3 worldNormal;
			INTERNAL_DATA
		};


		void surf (Input IN, inout SurfaceOutput o) {
			float4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));
			float3 worldNormal = WorldNormalVector(IN,o.Normal);
			float4 uplight = saturate(dot(worldNormal, float3(0,-1,0)));
			o.Albedo = c.rgb;
			o.Emission = uplight * _Lower;
			o.Alpha = 1;
			o.Gloss = c.a;
		}

		float4 LightingJKH (SurfaceOutput o, float3 lightDir, float3 viewDir, float atten){

		//램버트 연산
		float3 Diffusecolor;
		float NdotL =dot(o.Normal, lightDir) *0.5 + 0.5;
		Diffusecolor = NdotL * o.Albedo.rgb * _LightColor0.rgb * atten;

		//스페큘러 연산
		float3 SpecColor;
		float3 H = normalize(lightDir + viewDir);
		float spec = saturate(dot(H, o.Normal));
		spec = pow(spec, _SpecPow);
		if(_Mtl > 0)
		{
		SpecColor = spec * o.Gloss * o.Albedo.rgb *atten;
		}

		else
		{
		SpecColor = spec  * o.Gloss * _LightColor0 * atten;
		}
		
		//어퍼 라이트 연산
		float3 Uplight;
		Uplight = o.Emission * atten * _LightColor0;

		//림라이트 연산

		float3 Rimcolor;
		float Rim = saturate(dot(viewDir, o.Normal));
		float invrim = 1 - Rim;
		Rimcolor = pow(invrim, _Rimpow) * o.Albedo * NdotL  * _LightColor0 ;


		//가짜 스페큘러 연산
		float3 FakeSpec;
		FakeSpec = pow(Rim, _FakeSpecpow) * o.Gloss * _Fakecolor * _LightColor0;





		//최종 연산
		float4 Final;
		Final.rgb = Diffusecolor.rgb + SpecColor.rgb + Rimcolor.rgb + FakeSpec.rgb + Uplight.rgb;
		Final.a = o.Alpha;



			return Final;
		}


		ENDCG
	}
	FallBack "Diffuse"
}
