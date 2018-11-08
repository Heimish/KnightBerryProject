Shader "JKH/Afterimage" {
	Properties {
		[HDR]_Color ("Color", Color) = (1,1,1,1)
		[PowerSlider(3.0)] _Rimpower ("두께 조절",Range(0,20)) = 1
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		LOD 200
		zwrite on
		blend SrcAlpha One
		
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

		CGPROGRAM
		#pragma surface surf JKH alpha:fade noambient


		struct Input {
			float3 viewDir;
			float4 color : COLOR;
		};

		
		fixed4 _Color;
		float _Rimpower;

		void surf (Input IN, inout SurfaceOutput o) {

			o.Emission = IN.color.rgb * _Color;
			float rim =  abs(1 -abs(dot( o.Normal , IN.viewDir)));
			float blink = saturate(pow(rim,_Rimpower) *_Color.a) ;

			o.Alpha =  blink * IN.color.a;
		}
		
		float4 LightingJKH( SurfaceOutput s , float3 lightDir, float atten){
		return float4(0,0,0,s.Alpha);
		}
		
		ENDCG
	} 
	FallBack "Diffuse"
}
