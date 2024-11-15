Shader "Custom/DissolveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Dissolve ("Dissolve Amount", Range(0,1)) = 0.5
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        sampler2D _DissolveTex;
        float _Dissolve;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_DissolveTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            fixed4 dissolve = tex2D(_DissolveTex, IN.uv_DissolveTex);

            // Contrôle la transparence par rapport à la texture de dissolution
            if (dissolve.r < _Dissolve)
            {
                o.Alpha = 0; // Fait disparaître
            }
            else
            {
                o.Albedo = c.rgb; // Couleur de l'objet
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}