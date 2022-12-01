Shader "Unlit/BlendTexture"
{
    // The _BaseMap variable is visible in the Material's Inspector, as a field
    // called Base Map.
    Properties
    { 
        _Noise("Noise", 2D) = "white"
        _BaseMap("Base Map", 2D) = "white"
        _BaseMap2("Base Map 2", 2D) = "white"{}
        _BlendValue("Blend Value", Range(0, 1)) = 0
        
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
                float2 uv2          : TEXCOORD2;
                float2 uvNoise      : TEXCOORD3;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
                float2 uv2          : TEXCOORD2;
                float2 uvNoise      : TEXCOORD3;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            TEXTURE2D(_BaseMap2);
            SAMPLER(sampler_BaseMap2);
            TEXTURE2D(_Noise);
            SAMPLER(sampler_Noise);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                float4 _BaseMap2_ST;
                float4 _Noise_ST;
                float _BlendValue;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                OUT.uv2 = TRANSFORM_TEX(IN.uv, _BaseMap2);
                OUT.uvNoise = TRANSFORM_TEX(IN.uv, _Noise);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv)*SAMPLE_TEXTURE2D(_Noise, sampler_Noise, IN.uvNoise) + SAMPLE_TEXTURE2D(_BaseMap2, sampler_BaseMap2, IN.uv2)*(1-SAMPLE_TEXTURE2D(_Noise, sampler_Noise, IN.uvNoise));
                return color;
            }
            ENDHLSL
        }
    }
}
