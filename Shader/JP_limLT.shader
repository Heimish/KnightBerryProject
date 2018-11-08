Shader "JP/LT/LimLT" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_Metallic ("Metallic", Range(0,1)) = 0.0
		_Bump("Normal",2D) = "bump"{}
		_rim("림두께",Range(1,20))= 3
		_rimspeed("림속도",float)= 30
		_Close("근접",Range(0,1)) =0 
		_rimColor("림칼라",Color) = (1,0,0,1)
	}
	SubShader {
		Tags { "RenderType"="opaque"}
		LOD 200
		
		Blend One One

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Bump;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			float2 uv_Bump;
		};

		//half _Glossiness;
		//half _Metallic;
		fixed4 _Color;
		float _rim;
		float4 _rimColor;
		float _Close;
		float _rimspeed;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float rim;
			
			
			o.Normal = UnpackNormal(tex2D(_Bump,IN.uv_Bump) ) ;
			rim = dot(IN.viewDir, o.Normal);
			rim = pow(1-rim,_rim);
			//rim = rim * (sin(_Time*_rimspeed) *0.5+0.5);
			//rim = rim * (sin(_Time*_rimspeed));


			

			//o.Emission = rim * _rimColor.rgb * _Close;

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Emission = lerp(c.rgb,_rimColor.rgb,rim);
			//o.Emission = lerp(c.rgb,_rimColor,rim);

			o.Albedo = lerp(c.rgb,_rimColor,rim);
			//o.Albedo = c.rgb * rim;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
