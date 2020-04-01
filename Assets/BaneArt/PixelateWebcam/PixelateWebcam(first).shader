// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PixelateWebcam(First)"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.23
		_WebCam("WebCam", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_XPixelCount("X Pixel Count", Range( 1 , 256)) = 0
		_YPixelCount("Y Pixel Count", Range( 1 , 256)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _WebCam;
		uniform float _XPixelCount;
		uniform float _YPixelCount;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float _Cutoff = 0.23;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float pixelWidth2 =  1.0f / _XPixelCount;
			float pixelHeight2 = 1.0f / _YPixelCount;
			half2 pixelateduv2 = half2((int)(i.uv_texcoord.x / pixelWidth2) * pixelWidth2, (int)(i.uv_texcoord.y / pixelHeight2) * pixelHeight2);
			o.Albedo = tex2D( _WebCam, pixelateduv2 ).rgb;
			o.Alpha = 1;
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			clip( tex2D( _Mask, uv_Mask ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
2086;85;1049;728;1384.273;251.1986;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-972.5886,-177.6861;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-995.5886,-38.68605;Inherit;False;Property;_XPixelCount;X Pixel Count;3;0;Create;True;0;0;False;0;0;64;1;256;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-991.2726,36.80139;Inherit;False;Property;_YPixelCount;Y Pixel Count;4;0;Create;True;0;0;False;0;0;64;1;256;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCPixelate;2;-697.5884,-81.68607;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-482.5,-94;Inherit;True;Property;_WebCam;WebCam;1;0;Create;True;0;0;False;0;-1;None;;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-466.2726,129.8014;Inherit;True;Property;_Mask;Mask;2;0;Create;True;0;0;False;0;-1;None;61c0b9c0523734e0e91bc6043c72a490;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-135.5024,-88.59769;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;PixelateWebcam;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.23;True;True;0;False;TransparentCutout;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;3;0
WireConnection;2;1;4;0
WireConnection;2;2;5;0
WireConnection;1;1;2;0
WireConnection;0;0;1;0
WireConnection;0;10;6;0
ASEEND*/
//CHKSM=C4C756EDF7E136D8FE955D30DB6FE7E85919AC20