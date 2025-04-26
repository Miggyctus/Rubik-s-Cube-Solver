Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
float3 palette(float t)
{
    float3 a = float3(0.5, 0.5, 0.5);
    float3 b = float3(0.5, 0.5, 0.5);
    float3 c = float3(1.0, 1.0, 1.0);
    float3 d = float3(0.263, 0.416, 0.557);

    return a + b * cos(6.28318 * (c * t + d));
}

fixed4 frag(v2f i) : SV_Target
{
    float2 fragCoord = i.uv * _ScreenParams.xy;
    float2 uv = (fragCoord * 2.0 - _ScreenParams.xy) / _ScreenParams.y;
    float2 uv0 = uv;
    float3 finalColor = float3(0.0, 0.0, 0.0);

    float time = _Time.y;

    for (float j = 0.0; j < 4.0; j += 1.0)
    {
        uv = frac(uv * 1.5) - 0.5;

        float d = length(uv) * exp(-length(uv0));

        float3 col = palette(length(uv0) + j * 0.4 + time * 0.4);

        d = sin(d * 8.0 + time) / 8.0;
        d = abs(d);
        d = pow(0.01 / d, 1.2);

        finalColor += col * d;
    }

    return fixed4(finalColor, 1.0);
}

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
