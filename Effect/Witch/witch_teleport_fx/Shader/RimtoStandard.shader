Shader "Custom/RimtoNormal" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}		
		_Metal ("Metal", 2D) = "black"{}
		_Smooth("Smoothness",Range(0,1)) = 0.5
		_BumpMap("Normal",2D) = "Bump"{}
		_Occlusion("Occlusion",2D) = "white"{}
		_OcclusionPow("오클루젼 강도",Range(0,1)) = 1
		[Space(30)]
		_Rimthickness ("림 두께",Range(0,4)) = 3		
		[HDR] _RimColor("림 컬러",Color) = (1,1,1,1)
		[Space(50)]
		_RimOn("림 끄고 키기",Range(0,1)) = 0
		_Rimdel("림제거",Range(0,1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		LOD 200
		zwrite on
		blend SrcAlpha OneMinusSrcAlpha
		
		ColorMask 0
		CGPROGRAM
		#pragma surface surf nolight noambient noforwardadd nolightmap novertexlights noshadow
		struct Input{
			float4 color:COLOR;
		};
		void surf (Input IN, inout SurfaceOutput o ){
		}
		float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten){
			return float4(0,0,0,0);
		}
		ENDCG

		
		zwrite off
		CGPROGRAM
		
		#pragma surface surf Standard fullforwardshadows alpha:fade


		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _Metal;
		sampler2D _Occlusion;
		float4 _RimColor;
		float _RimOn;
		float _Rimdel;
		float _Smooth;
		float _Rimthickness;
		float _OcclusionPow;
		struct Input {
			float2 uv_MainTex;
			float2 uv_Metal;
			float2 uv_BumpMap;	
			float2 uv_Occlusion;
			float3 viewDir;
			
		};


		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) ;
			float4 me = tex2D (_Metal,IN.uv_Metal);			
			float4 oc = tex2D(_Occlusion,IN.uv_Occlusion);
			o.Normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));
			float rim = dot(o.Normal,IN.viewDir);	
			
			rim = pow(1-rim,_Rimthickness);			
			o.Albedo = lerp(c.rgb ,float3(0,0,0),_RimOn) ;			
			o.Metallic = me;
			o.Smoothness = me.a * _Smooth;								

			o.Emission =  rim * lerp(float4(0,0,0,0),_RimColor.rgb,_RimOn);
			o.Alpha = lerp(1,rim,_RimOn) * _Rimdel;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
