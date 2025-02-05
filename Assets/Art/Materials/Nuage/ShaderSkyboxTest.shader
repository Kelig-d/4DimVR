Shader "Unlit/ShaderSkyboxTest"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (0.0, 0.5, 1.0, 1.0) // Bleu clair
        _BottomColor ("Bottom Color", Color) = (0.0, 0.0, 0.5, 1.0) // Bleu foncé
        _LineColor ("Line Color", Color) = (1.0, 0.5, 0.0, 1.0) // Orange
        _LineThickness ("Line Thickness", Float) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _TopColor;
            float4 _BottomColor;
            float4 _LineColor;
            float _LineThickness;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Map world position to vertical gradient (Y axis)
                float gradient = saturate(i.worldPos.y * 0.5 + 0.5);

                // Add the orange line at the middle
                float line = smoothstep(0.48, 0.52, gradient) * smoothstep(0.52, 0.48, gradient);

                // Combine colors
                float4 color = lerp(_BottomColor, _TopColor, gradient);
                color = lerp(color, _LineColor, line * _LineThickness);

                return color;
            }
            ENDCG
        }
    }
}

