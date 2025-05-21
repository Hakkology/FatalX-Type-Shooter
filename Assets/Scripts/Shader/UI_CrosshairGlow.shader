Shader "UI/CrosshairGlow"
{
    Properties
    {
        _Color           ("Color",           Color) = (1,1,1,1)
        _Thickness       ("Line Thickness",  Float) = 0.02
        _Glow            ("Glow Softness",   Float) = 0.03
        _Speed           ("Rotate Speed",    Float) = 1.0
        _Scale           ("Overall Scale",   Float) = 1.0
        _CircleRadius    ("Circle Radius",   Float) = 0.5
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;
            float _Thickness;
            float _Glow;
            float _Speed;
            float _Scale;
            float _CircleRadius;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color  : COLOR;
                float2 uv      : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color  : COLOR;
                float2 uv     : TEXCOORD0;
            };

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.color  = IN.color * _Color;
                // UV'yi -1..+1 aralığına:
                OUT.uv = (IN.uv * 2.0 - 1.0) * _Scale;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // Rotation
                float t = _Time.y * _Speed;
                float c = cos(t), s = sin(t);
                float2 uv = IN.uv;
                float2 ruv = float2(
                    uv.x * c - uv.y * s,
                    uv.x * s + uv.y * c
                );

                // Line mask (horizontal + vertical)
                float maskX = smoothstep(_Thickness, _Thickness * 0.5, abs(ruv.x));
                float maskY = smoothstep(_Thickness, _Thickness * 0.5, abs(ruv.y));
                float lineMask = saturate(maskX + maskY);

                // Line glow
                float glowX = smoothstep(_Thickness + _Glow, _Thickness, abs(ruv.x));
                float glowY = smoothstep(_Thickness + _Glow, _Thickness, abs(ruv.y));
                float glowMask = saturate(glowX + glowY) * 0.5;

                // Circle mask
                float radial = length(ruv);
                float ringDist = abs(radial - _CircleRadius);
                float maskC = smoothstep(_Thickness, _Thickness * 0.5, ringDist);
                float glowC = smoothstep(_Thickness + _Glow, _Thickness, ringDist) * 0.5;
                float circleMask = saturate(maskC + glowC);

                // Final alpha combines lines + circle
                float alpha = saturate(lineMask + glowMask + circleMask) * IN.color.a;
                return fixed4(IN.color.rgb, alpha);
            }
            ENDCG
        }
    }
}
