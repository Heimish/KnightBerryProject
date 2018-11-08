Shader "Solutena/Crystal" {
	Properties {
		_NormalMap("NormalMap",2D) = "bump" {}
		_Color ("Color", Color) = (1,1,1,1)
		_Rim("림",Range(0,20)) = 0
		[HDR]_RimColor ("림컬러", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows noambient nolightmap

		#pragma target 3.0

		sampler2D _NormalMap;

		struct Input {
			float3 viewDir;
			float2 uv_NormalMap;
		};

		fixed4 _Color; 
		fixed4 _RimColor;
		float _Rim;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Normal = UnpackNormal(tex2D(_NormalMap,IN.uv_NormalMap) ) ;
			float rim;
			rim = dot(IN.viewDir, o.Normal);
			rim = pow(1-rim,_Rim);
			o.Emission = _Color + (rim*_RimColor);
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
