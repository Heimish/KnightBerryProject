Shader "NHJ/Character_NoiseFade5" {
	Properties{
	   [HDR]_Color("기본색" , color) = (1,1,1,1)
	   _MainTex("기본 텍스쳐(RGB)", 2D) = "white" {}
	   _MetalTex("메탈",2D) = "black" {}
	   _Metallic("Metallic" ,float) = 0
	   _Smoothness("Smoothness", Range(0,1)) = 0
	   _EmitTex("셀프 일루미네이션 텍스쳐" , 2D) = "black" {}
	   _BumpTex("범프맵" , 2D) = "blue" {}
	   _NoiseTex("알파 텍스쳐 (R)", 2D) = "white"{}
	   _OC("오클루젼 맵",2D) = "white"{}
	   [HideInInspector]_Cutoff("알파테스트", float) = 0.5
	   _Hide("노이즈 한계치(1 ~ -1)_Hide" , float) = 1
	   [HDR]_AlphaColor("알파 경계선 칼라_AlphaColor" , color) = (1,1,1,1)
	   _Rimpow("림 두께", Range(1,30)) = 1
	   _RimbalGGi("림세기", float) = 0
	}
		SubShader{
		   Tags {"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"}

		   cull off

		   CGPROGRAM
		   #pragma surface surf Standard alphatest:_Cutoff keepalpha addshadow

		   #pragma target 3.0

		   float4 _Color;
		   sampler2D _MainTex;
		   sampler2D _NoiseTex;
		   sampler2D _EmitTex;
		   sampler2D _BumpTex;
		   sampler2D _MetalTex;
		   sampler2D _OC;
		   float _Metallic;
		   float _Hide;
		   float _Smoothness;
		   float4 _AlphaColor;
		   float _Rimpow;
		   float _RimbalGGi;


		   struct Input {
			  float2 uv_MainTex;
			  float2 uv_MetalTex;
			  float2 uv_EmitTex;
			  float2 uv_NoiseTex;
			  float2 uv_BumpTex;
			  float2 uv_Eye;
			  float3 viewDir;
			  float4 color: COLOR;
		   };

		   void surf(Input IN, inout SurfaceOutputStandard o) {
			  half4 c = tex2D(_MainTex, IN.uv_MainTex);
			  half4 d = tex2D(_NoiseTex, IN.uv_NoiseTex);
			  half4 emittex = tex2D(_EmitTex, IN.uv_EmitTex);
			  float alphalevel = d.r + _Hide;
			  o.Albedo = c.rgb * _Color.rgb * IN.color.rgb;
			  float4 met = tex2D(_MetalTex,IN.uv_MetalTex);
			  o.Metallic = met.r * _Metallic;
			  o.Smoothness = met.a * _Smoothness;
			  o.Occlusion = tex2D(_OC,IN.uv_MainTex);
			  o.Normal = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
			  float rim = pow(1 - saturate(dot(IN.viewDir,o.Normal)), _Rimpow);
			  o.Emission = rim * _RimbalGGi * c.rgb + emittex.rgb + (step(0.48, 1 - alphalevel) * _AlphaColor.rgb * 3);
			  o.Alpha = c.a * alphalevel * IN.color.a;
		   }
		   ENDCG
	   }
		   FallBack "Legacy Shaders/Transparent/Cutout/Diffuse"
}