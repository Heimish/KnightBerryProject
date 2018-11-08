Shader "Custom/Map/S_Lerf" {
	Properties{
		_Color( "Main Color", Color ) = ( 1,1,1,1 )
		_EmissionColor( "_EmissionColor", Color ) = ( 1,1,1,1 )
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
		_MainTex ("Base (RGB) TransGloss (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5

		[Header( OutVertex )]
		_OutTex( "OutTex", 2D ) = "white" {}
		_OutRange( "OutRange", float ) = 0.1
		_OutSpeed( "OutSpeed", float ) = 0.5
		_OutTex2( "OutTex", 2D ) = "white" {}
		_OutRange2( "OutRange", float ) = 0.1
		_OutSpeed2( "OutSpeed", float ) = 0.5
		_OutDir( "OutDir", vector ) = ( 1, 1, 0.5, 1 )
	}

	SubShader {
		Tags {"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
		LOD 400

		Cull Off

		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert alphatest:_Cutoff
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		fixed4 _Color;
		fixed4 _EmissionColor;
		half _Shininess;
		sampler2D _OutTex;
		float _OutRange;
		float _OutSpeed;
		sampler2D _OutTex2;
		float _OutRange2;
		float _OutSpeed2;
		float4 _OutDir;

		void vert( inout appdata_full v )
		{
			float tex = tex2Dlod( _OutTex, float4( v.texcoord.x - _Time.y * _OutSpeed, v.texcoord.y - _Time.y * _OutSpeed, 0, 0 ) ).r;
			float tex2 = tex2Dlod( _OutTex2, float4( v.texcoord.x - _Time.y * _OutSpeed2, v.texcoord.y - _Time.y * _OutSpeed2, 0, 0 ) ).r;
			float3 addvertex = v.color.r * v.normal.xyz * lerp( tex * _OutRange, tex2 * _OutRange2, 0.5f );
			v.vertex.xyz += (addvertex * _OutDir.xyz) * _OutDir.w;
		}

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};
		void surf( Input IN, inout SurfaceOutput o )
		{
			fixed4 c = tex2D( _MainTex, IN.uv_MainTex );
			o.Albedo = c.rgb * _Color.rgb;
			o.Emission = c.rgb * _EmissionColor.rgb;
			o.Gloss = c.a;
			o.Alpha = c.a * _Color.a;
			o.Specular = _Shininess;
		}
		ENDCG
	}
	
	FallBack "Legacy Shaders/Transparent/Cutout/VertexLit"
}

/*
Pass
{
Name "ShadowCaster"
Tags{ "LightMode" = "ShadowCaster" }
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

sampler2D _MainTex;
fixed _Cutoff;

struct v2f {
V2F_SHADOW_CASTER;
float2 uv : TEXCOORD1;
}
v2f vert( appdata_full v )
{
v2f o;
o.pos = mul( UNITY_MATRIX_MVP, v.vertex );
o.uv = v.texcoord;
TRANSFER_SHADOW_CASTER( o )
return o;
}
float4 frag( v2f IN ) : COLOR
{
fixed4 c = tex2D( _MainTex, IN.uv );
clip( c.a - _Cutoff );
SHADOW_CASTER_FRAGMENT( IN )
}
ENDCG
}
*/