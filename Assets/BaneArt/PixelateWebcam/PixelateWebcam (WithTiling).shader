// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PixelateWebcam(tiled)"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_XPixelCount("X Pixel Count", Range( 1 , 256)) = 0
		_YPixelCount("Y Pixel Count", Range( 1 , 256)) = 0
		_Mask("Mask", 2D) = "white" {}
		_MaskXPixelCount("Mask X Pixel Count", Range( 1 , 256)) = 0
		_MaskYPixelCount("Mask Y Pixel Count", Range( 1 , 256)) = 0
		[Toggle]_ManualMaskPixelisation("Manual Mask Pixelisation", Float) = 1
		_MainTex("WebCam", 2D) = "white" {}
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

		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float _XPixelCount;
		uniform float _YPixelCount;
		uniform sampler2D _Mask;
		uniform float _ManualMaskPixelisation;
		uniform float4 _Mask_ST;
		uniform float _MaskXPixelCount;
		uniform float _MaskYPixelCount;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv0_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float pixelWidth2 =  1.0f / _XPixelCount;
			float pixelHeight2 = 1.0f / _YPixelCount;
			half2 pixelateduv2 = half2((int)(uv0_MainTex.x / pixelWidth2) * pixelWidth2, (int)(uv0_MainTex.y / pixelHeight2) * pixelHeight2);
			o.Albedo = tex2D( _MainTex, pixelateduv2 ).rgb;
			o.Alpha = 1;
			float2 uv0_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float pixelWidth10 =  1.0f / _MaskXPixelCount;
			float pixelHeight10 = 1.0f / _MaskYPixelCount;
			half2 pixelateduv10 = half2((int)(uv0_Mask.x / pixelWidth10) * pixelWidth10, (int)(uv0_Mask.y / pixelHeight10) * pixelHeight10);
			clip( tex2D( _Mask, (( _ManualMaskPixelisation )?( pixelateduv10 ):( uv0_Mask )) ).a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
1298;188;1049;728;1815.606;5.976868;1;True;False
Node;AmplifyShaderEditor.TexturePropertyNode;12;-1524.878,215.9124;Inherit;True;Property;_Mask;Mask;3;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1271.128,286.203;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-1294.128,425.2033;Inherit;False;Property;_MaskXPixelCount;Mask X Pixel Count;4;0;Create;True;0;0;False;0;0;32;1;256;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1298.072,500.6907;Inherit;False;Property;_MaskYPixelCount;Mask Y Pixel Count;5;0;Create;True;0;0;False;0;0;32;1;256;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;13;-1215.861,-97.27001;Inherit;True;Property;_MainTex;WebCam;8;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-964.0813,-20.90733;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-1006.527,92.57059;Inherit;False;Property;_XPixelCount;X Pixel Count;1;0;Create;True;0;0;False;0;0;32;1;256;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1002.211,168.058;Inherit;False;Property;_YPixelCount;Y Pixel Count;2;0;Create;True;0;0;False;0;0;32;1;256;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCPixelate;10;-984.3273,369.2226;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCPixelate;2;-681.7891,-18.48841;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ToggleSwitchNode;11;-773.8201,282.7823;Inherit;False;Property;_ManualMaskPixelisation;Manual Mask Pixelisation;6;0;Create;True;0;0;False;0;1;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;6;-469.6657,211.3452;Inherit;True;Property;_MaskValue;MaskValue;4;0;Create;True;0;0;False;0;-1;None;cf89fb6505f93104e90129de5f6432b3;True;0;False;white;Auto;False;Instance;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-482.5,-94;Inherit;True;Property;_MainTexValue;WebCamValue;7;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Instance;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-135.5024,-88.59769;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;PixelateWebcam;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;2;12;0
WireConnection;3;2;13;0
WireConnection;10;0;7;0
WireConnection;10;1;8;0
WireConnection;10;2;9;0
WireConnection;2;0;3;0
WireConnection;2;1;4;0
WireConnection;2;2;5;0
WireConnection;11;0;7;0
WireConnection;11;1;10;0
WireConnection;6;0;12;0
WireConnection;6;1;11;0
WireConnection;1;0;13;0
WireConnection;1;1;2;0
WireConnection;0;0;1;0
WireConnection;0;10;6;4
ASEEND*/
//CHKSM=0880B5B0127E469B84871D21E01B730F1679B91F