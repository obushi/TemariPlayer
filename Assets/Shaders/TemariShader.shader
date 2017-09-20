﻿Shader "Toon/Temari" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
		_Fade ("Fade", Float) = 1.0
		_ColorPattern("Color Pattern", Int) = 0
	}

	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend Zero One//SrcAlpha OneMinusSrcAlpha
		LOD 200
		
CGPROGRAM
#pragma surface surf ToonRamp alpha:fade //alpha:premul

sampler2D _Ramp;
float _Fade;
int _ColorPattern;

// custom lighting function that uses a texture ramp based
// on angle between light direction and normal
#pragma lighting ToonRamp exclude_path:prepass
inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
{
	#ifndef USING_DIRECTIONAL_LIGHT
	lightDir = normalize(lightDir);
	#endif
	
	half d = dot (s.Normal, lightDir)*0.5 + 0.5;
	half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
	
	half4 c;
	c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
	c.a = _Fade;
	return c;
}


sampler2D _MainTex;
float4 _Color;

struct Input {
	float2 uv_MainTex : TEXCOORD0;
};

void surf (Input IN, inout SurfaceOutput o) {
	half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

	if (_ColorPattern == 0)
		o.Albedo = c.rgb;
	else if (_ColorPattern == 1)
		o.Albedo = c.gbr;
	else if (_ColorPattern == 2)
		o.Albedo = c.bgr;
	else if (_ColorPattern == 3)
		o.Albedo = c.rbg;
	else
		o.Albedo = c.grb;

	//o.Albedo = c.rrb;
	o.Alpha = c.a;
}
ENDCG

	} 

	Fallback "Diffuse"
}