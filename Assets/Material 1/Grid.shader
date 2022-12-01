
Shader "Unlit/FireEffect"
{
    // The _BaseMap variable is visible in the Material's Inspector, as a field
    // called Base Map.
    Properties
    { 
        _BaseMap("Base Map", 2D) = "white"{}
        [HDR]_Color1 ("Color1", Color) = (1,1,1,1)
        [HDR]_Color2 ("Color2", Color) = (1,1,1,1)
        _GridSize ("GridSize", Range(2,64)) = 10
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
                float2 uv           : TEXCOORD0;
                float3 normal       : NORMAL0;
                half4 R             : TEXCOORD1;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                float4 _Color1;
                float4 _Color2;
                int _GridSize;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half2 st = IN.uv.xy;
                half3 color = (0,0,0);

                // Each result will return 1.0 (white) or 0.0 (black).
                float left = step(1.0/_GridSize,st.x);   // Similar to ( X greater than 0.1 )
                float bottom = step(1.0/_GridSize,st.y); // Similar to ( Y greater than 0.1 )
                float up = step(st.x,(1-1.0/_GridSize));   // Similar to ( X greater than 0.1 )
                float right = step(st.y,(1-1.0/_GridSize)); // Similar to ( Y greater than 0.1 )

                float a = step(st.x, 1.0/_GridSize);
                float b = step(st.y, 1.0/_GridSize);

                // The multiplication of left*bottom will be similar to the logical AND.
                //color = ((left * bottom * up * right) * _Color1 + (1-left * bottom * up * right)*_Color2);
                color = (a*b)*_Color1 +(1-(a*b))*_Color2;
               
                return half4(color,1);
                
            }
            ENDHLSL
        }
    }
}
