Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PaintPosition ("Position", Vector) = (0, 0, 0, 0)
        _PaintColor ("Color", Color) = (0, 0, 0, 0)
        _PaintRadius ("Radius", float) = 0
        _PaintHardness ("Hardness", float) = 0
        _PaintStrength ("Strength", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 world_pos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float3 _PaintPosition;
            float4 _PaintColor;
            float _PaintRadius;
            float _PaintHardness;
            float _PaintStrength;

            v2f vert (const appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.world_pos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float mask(
                const float3 frag_position,
                const float3 collision_position,
                const float radius,
                const float hardness)
            {
                const float distance_to_collision = distance(frag_position, collision_position);
                return 1 - smoothstep(radius * hardness, radius, distance_to_collision);
            }

            float4 frag (const v2f i) : SV_Target
            {
                const float4 col = tex2D(_MainTex, i.uv);
                const float edge = mask(i.world_pos, _PaintPosition, _PaintRadius, _PaintHardness) * _PaintStrength;
                return lerp(col, _PaintColor, edge);
            }
            ENDCG
        }
    }
}
