Shader "JKH/JKH_Refrection" {
	Properties {
	_MainTex("Refrection Tex" , 2D) = "white" {}
	_Refstrength("Refrection Strength" , Range(0,0.1)) = 0.05
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		zwrite off
		cull off
		
		Grabpass{}

		CGPROGRAM
		#pragma surface surf nolight noambient alpha:fade

		#pragma target 3.0

		sampler2D _GrabTexture;
		sampler2D _MainTex;
		float _Refstrength;

		struct Input {
			float4 color:COLOR;
			float4 screenPos;
			float2 uv_MainTex;
		};



		void surf (Input IN, inout SurfaceOutput o) {
		
		float4 ref = tex2D(_MainTex,IN.uv_MainTex)* IN.color.a;
		float3 screenUV = IN.screenPos.rgb/IN.screenPos.a;
		o.Emission = tex2D(_GrabTexture,(screenUV.xy + ref.x * _Refstrength));

		}

		float4 Lightingnolight(SurfaceOutput s, float3 lightDir , float atten)
		{
		return float4(0,0,0,1);
		}
		ENDCG
	}
	FallBack "Regacy Shaders/Transparent/Vertexlit"
}
