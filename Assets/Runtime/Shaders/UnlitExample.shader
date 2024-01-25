Shader "Unlit/UnlitExample"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;

            struct appdata
            {
                float3 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct fragdata
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fragdata vert(appdata v)
            {
                fragdata f;
                f.vertex = float4(v.vertex.x, v.vertex.y, v.vertex.z, 0);
                f.vertex.y += sin(_Time.y * 5);
                f.vertex = UnityObjectToClipPos(f.vertex);
                f.uv = v.uv;
                return f;
            }

            float4 frag(fragdata f) : SV_TARGET
            {
                float4 color = tex2D(_MainTex, f.uv);
                return color;
            }
            ENDCG
        }
    }
}
