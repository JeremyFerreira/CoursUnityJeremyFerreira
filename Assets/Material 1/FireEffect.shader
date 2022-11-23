// This shader draws a texture on the mesh.
Shader "Unlit/FireEffect"
{
    // The _BaseMap variable is visible in the Material's Inspector, as a field
    // called Base Map.
    Properties
    { 
        _BaseMap("Base Map", 2D) = "white"{}
        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (0,0,0,1)
        _Color ("Color", Color) = (0,0,0,1)
        _Bias("Bias", float) = 1
        _Scale("Scale", float) = 1
        _Power("Power", float) = 1
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
                float3 normal       : NORMAL0;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                // The uv variable contains the UV coordinate on the texture for the
                // given vertex.
                float2 uv           : TEXCOORD0;
                float3 normal       : NORMAL0;
                half4 R             : TEXCOORD1;
            };

            // This macro declares _BaseMap as a Texture2D object.
            TEXTURE2D(_BaseMap);
            // This macro declares the sampler for the _BaseMap texture.
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                // The following line declares the _BaseMap_ST variable, so that you
                // can use the _BaseMap variable in the fragment shader. The _ST 
                // suffix is necessary for the tiling and offset function to work.
                float4 _BaseMap_ST;
                float4 _Color1;
                float4 _Color2;
                float4 _Color;
                float _Bias;
                float _Scale;
                float _Power;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                float3 vertPos = IN.positionOS;
                float3 posWorld = GetVertexPositionInputs(IN.positionOS.xyz).positionWS;
	            float3 normWorld = normalize(IN.normal);

                //float3 posWorld = mul(_Object2World, v.vertex).xyz;
	            //float3 normWorld = normalize(mul(float3x3(_Object2World), v.normal));

	            float3 I = normalize(posWorld - _WorldSpaceCameraPos.xyz);
	            OUT.R = _Bias + _Scale * pow(1.0 + dot(I, normWorld), _Power);
                
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // The SAMPLE_TEXTURE2D marco samples the texture with the given
                // sampler.
                half4 col = (_Color1 * (1 - SAMPLE_TEXTURE2D(_BaseMap,sampler_BaseMap, IN.uv+_SinTime)) + (_Color2 * SAMPLE_TEXTURE2D(_BaseMap,sampler_BaseMap, IN.uv+_SinTime)));
                return lerp(col,_Color, IN.R);
            }
            ENDHLSL
        }
    }
}
