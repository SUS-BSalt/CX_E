Shader "Custom/GaussianBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0, 0.05)) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv[5] : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurSize;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float2 offsets[5] = { float2(-_BlurSize, -_BlurSize), float2(0, -_BlurSize), float2(_BlurSize, -_BlurSize), float2(-_BlurSize, 0), float2(_BlurSize, 0) };
                for (int i = 0; i < 5; i++)
                    o.uv[i] = TRANSFORM_TEX(v.uv, _MainTex) + offsets[i];
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 sum = fixed4(0,0,0,0);
                sum += tex2D(_MainTex, i.uv[0]) * 0.05;
                sum += tex2D(_MainTex, i.uv[1]) * 0.2;
                sum += tex2D(_MainTex, i.uv[2]) * 0.05;
                sum += tex2D(_MainTex, i.uv[3]) * 0.2;
                sum += tex2D(_MainTex, i.uv[4]) * 0.2;
                sum += tex2D(_MainTex, TRANSFORM_TEX(i.vertex.xy, _MainTex)) * 0.25;
                return sum;
            }
            ENDCG
        }
    }
}