Shader "JKHshader/grass" {
	Properties {
		_Color ("Grass Color", Color) = (1,1,1,1)
		_Color2 ("Albedo Color", Color) = (1,1,1,1)
		_Spec ("Grass Specular", Range(0,1)) = 0.778
		_MainTex ("Grass Texture (RGB)", 2D) = "white" {}
		_BumpMap ("Grass Normal Map", 2D) = "bump" {}
		[HideInInspector]_BumpNone ("", 2D) = "bump" {}
		_Spec2 ("Albedo Specular", range(0,1)) = 0.4
		_SecondTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap2 ("Albedo Normal Map", 2D) = "bump" {}
		[Toggle]_MultiDir ("Multi Directional Wind", Int) = 1
		_Wind ("Wind Power", Range(0,4)) = 1
		_WindDir ("Wind Direction (XY), Wind Speed (ZW)", Vector) = (1,0,1,1)
		_WindTex ("Wind Texture", 2D) = "white" {}
		_Occ ("Occlusion Intensity", Range(0,1)) = 1.0
		_Cutoff ("Cutoff", Range(0,1)) = 0.097
		_Alpha ("Cutoff/Occlusion (A)", 2D) = "white" {} 
		_Length ("Length", Range(0,1)) = 0.05
		_Div ("Length Division Factor", Range(0,10000)) = 50
		_Height ("Grass Height Map (A)", 2D) = "white" {}
		_Upssoom("유ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ",float) = 0
		_Subtex("Subtex", 2D) = "white" {}
		_SubSmooth("Subsmooth" , Range(0,1)) = 0
		_BumpMap3("SubNormal", 2D) = "bump" {}
		_Upssoom("리ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ",float) = 0
		_Subtex2("Subtex", 2D) = "white" {}
		_SubSmooth2("Subsmooth" , Range(0,1)) = 0
		_BumpMap4("SubNormal", 2D) = "bump" {}
		_Upssoom("바ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ",float) = 0
		_Subtex3("Subtex", 2D) = "white" {}
		_SubSmooth3("Subsmooth" , Range(0,1)) = 0
		_BumpMap5("SubNormal", 2D) = "bump" {}
		_Upssoom("보ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ",float) = 0
		_RMulti("R채널의 곱하기" ,float) = 1
		_GMulti("G채널의 곱하기" ,float) = 1
		_BMulti("B채널의 곱하기" ,float) = 1

	}
		
		CGINCLUDE
		#pragma target 4.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _WindTex;
		sampler2D _Alpha;
		float4 _Alpha_TexelSize;
		sampler2D _Height;
		float4 _Height_TexelSize;
		sampler2D _Subtex;
		sampler2D _BumpMap3;
		sampler2D _Subtex2;
		sampler2D _BumpMap4;
		sampler2D _Subtex3;
		sampler2D _BumpMap5;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_WindTex;
			float2 uv_Alpha;
			float2 uv_Height;
			float2 uv_SecondTex;
			float2 uv_BumpMap2;
			float2 uv_Subtex;
			float2 uv_BumpMap3;
			float2 uv_Subtex2;
			float2 uv_BumpMap4;
			float2 uv_Subtex3;
			float2 uv_BumpMap5;
			half ite;
	        float4 color:Color; 

		};

		fixed4 _Color;
		half _Spec;
		half _Length;
		half _Occ;
		half _Div;
		half _Wind;
		float4 _WindDir;
		int _MultiDir;
		float _SubSmooth;
		float _SubSmooth2;
		float _SubSmooth3;
		float _RMulti;
		float _GMulti;
		float _BMulti;
		
	

		void vertCustom (inout appdata_full v, half ITERATION) {
			v.vertex.xyz += lerp(0,(v.normal * _Length / _Div * ITERATION / 2),saturate(v.color.r * _RMulti));
		}


		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			float4 notex = tex2D(_Subtex, IN.uv_Subtex);
			float4 notex2 = tex2D(_Subtex2, IN.uv_Subtex2);
			float4 notex3 = tex2D(_Subtex3, IN.uv_Subtex3);
			int scale = IN.ite;
			IN.uv_WindTex += float2 (_WindDir.zw * _Time.x);
			float offsetx = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Alpha_TexelSize.x * _WindDir.x;
			float offsety = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Alpha_TexelSize.y * _WindDir.y;
			float2 offset = lerp(0,float2 (offsetx, offsety) * scale / 2,saturate(IN.color.r * _RMulti));
			IN.uv_Alpha = offset + IN.uv_Alpha;
			IN.uv_BumpMap = offset + IN.uv_BumpMap;
			o.Normal = lerp(UnpackNormal (tex2D(_BumpMap3, IN.uv_BumpMap3)), UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap)), saturate(IN.color.r * _RMulti));
			o.Normal = lerp(o.Normal, UnpackNormal(tex2D(_BumpMap4, IN.uv_BumpMap4)),saturate(IN.color.g * _GMulti));
			o.Normal = lerp(o.Normal, UnpackNormal(tex2D(_BumpMap5, IN.uv_BumpMap5)),saturate(IN.color.b * _BMulti));
			float3 Ef = c.rgb * lerp (1, tex2D (_Alpha, IN.uv_Alpha).a, _Occ);
			o.Albedo = lerp(notex.rgb, Ef.rgb, saturate(IN.color.r * _RMulti));
			o.Albedo = lerp(o.Albedo,notex2 , saturate(IN.color.g * _GMulti));
			o.Albedo = lerp(o.Albedo,notex3 , saturate(IN.color.b * _BMulti));

			o.Smoothness = lerp(_SubSmooth, _Spec * IN.ite / 10, saturate(IN.color.r * _RMulti));
			o.Smoothness = lerp(o.Smoothness, _SubSmooth2, saturate(IN.color.g * _GMulti));
			o.Smoothness = lerp(o.Smoothness, _SubSmooth3, saturate(IN.color.b * _BMulti));

			float h = tex2D (_Height, IN.uv_Height).a;
			
			o.Alpha = pow (tex2D (_Alpha, IN.uv_Alpha).a * h, IN.ite / 5);
		}

		void surfTop (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			float4 notex = tex2D(_Subtex, IN.uv_Subtex);
			float4 notex2 = tex2D(_Subtex2, IN.uv_Subtex2);
			float4 notex3 = tex2D(_Subtex3, IN.uv_Subtex3);
			int scale = IN.ite;
			IN.uv_WindTex += float2 (_WindDir.zw * _Time.x);
			float offsetx = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Alpha_TexelSize.x * _WindDir.x;
			float offsety = lerp (tex2D (_WindTex, IN.uv_WindTex), (tex2D (_WindTex, IN.uv_WindTex) - 0.5) / 0.5, _MultiDir) * _Wind * _Alpha_TexelSize.y * _WindDir.y;
			float2 offset = lerp(0,float2 (offsetx, offsety) * scale / 2,saturate(IN.color.r * _RMulti));
			IN.uv_Alpha = offset + IN.uv_Alpha;
			IN.uv_BumpMap = offset + IN.uv_BumpMap;
			o.Normal = lerp(UnpackNormal (tex2D(_BumpMap3, IN.uv_BumpMap3)),UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap)),saturate(IN.color.r * _RMulti));
			o.Normal = lerp(o.Normal, UnpackNormal(tex2D(_BumpMap4, IN.uv_BumpMap4)),saturate(IN.color.g * _GMulti));
			o.Normal = lerp(o.Normal, UnpackNormal(tex2D(_BumpMap5, IN.uv_BumpMap5)),saturate(IN.color.b * _BMulti));
			float3 Df = c.rgb * lerp (1, tex2D (_Alpha, IN.uv_Alpha).a, _Occ);
			o.Albedo = lerp(notex.rgb, Df.rgb, saturate(IN.color.r * _RMulti));
			o.Albedo = lerp(o.Albedo,notex2 , saturate(IN.color.g * _GMulti));
			o.Albedo = lerp(o.Albedo,notex3 , saturate(IN.color.b * _BMulti));
			o.Smoothness = lerp(_SubSmooth,_Spec * IN.ite / 10, saturate(IN.color.r * _RMulti));
			o.Smoothness = lerp(o.Smoothness, _SubSmooth2, saturate(IN.color.g * _GMulti));
			o.Smoothness = lerp(o.Smoothness, _SubSmooth3, saturate(IN.color.b * _BMulti));
			float h = tex2D (_Height, IN.uv_Height).a;

			o.Alpha = pow (tex2D (_Alpha, IN.uv_Alpha).a * h, IN.ite / 5);
		}
		ENDCG

	SubShader {
		Tags { "RenderType"="AlphaTest" "Queue"="AlphaTest" }
		LOD 200

		CGPROGRAM
		#pragma surface surfBase Standard

		sampler2D _SecondTex;
		sampler2D _BumpMap2;

		half _Spec2;
		fixed4 _Color2;

		void surfBase (Input IN, inout SurfaceOutputStandard o) {
			
			o.Normal = lerp(UnpackNormal (tex2D(_BumpMap3, IN.uv_BumpMap3)), UnpackNormal (tex2D (_BumpMap2, IN.uv_BumpMap2)),saturate(IN.color.r * _RMulti));
			o.Normal = lerp(o.Normal, UnpackNormal(tex2D(_BumpMap4, IN.uv_BumpMap4)),saturate(IN.color.g * _GMulti));
			o.Normal = lerp(o.Normal, UnpackNormal(tex2D(_BumpMap5, IN.uv_BumpMap5)),saturate(IN.color.b * _BMulti));
			half4 c = tex2D (_SecondTex, IN.uv_SecondTex) * _Color2;
			float4 notex = tex2D (_Subtex,IN.uv_Subtex);
			float4 notex2 = tex2D(_Subtex2, IN.uv_Subtex2);
			float4 notex3 = tex2D(_Subtex3, IN.uv_Subtex3);
			float3 Rf = c.rgb * lerp (1, tex2D (_Alpha, IN.uv_Alpha).a, _Occ * tex2D (_Height, IN.uv_Height));
			o.Albedo = lerp(notex, Rf.rgb, saturate(IN.color.r * _RMulti));
			o.Albedo = lerp(o.Albedo,notex2 , saturate(IN.color.g * _GMulti));
			o.Albedo = lerp(o.Albedo,notex3 , saturate(IN.color.b * _BMulti));
			o.Alpha = c.a;
			o.Smoothness = lerp(_SubSmooth,_Spec2, saturate(IN.color.r * _RMulti));
			o.Smoothness = lerp(o.Smoothness, _SubSmooth2, saturate(IN.color.g * _GMulti));
			o.Smoothness = lerp(o.Smoothness, _SubSmooth3, saturate(IN.color.b * _BMulti));
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 1.5
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 2
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 3
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 4
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 5
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 6
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 7
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 8
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 9
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 10
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		/*CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 11
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 12
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 13
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 14
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 15
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 16
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 17
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 18
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 19
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff vertex:vert
		#define ITERATION 20
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT (Input, o);
			half ite = ITERATION;
			o.ite = ite;
			vertCustom (v, ITERATION);
		}
		ENDCG*/
		

	}
	FallBack "Diffuse"
}
