Shader "Custom/VertexColorDiffuse_Instanced"
{
    Properties
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" { }
        _UV_Scale ("UV Scale", Float) = 0	//UV 缩放
        _Color ("Color", Color) = (1, 1, 1, 1)
		_Speed ("Speed", Float) = 1
        [Toggle(ENABLE_ALPHA)] _AlphaEnable ("Alpha Enable", Float) = 0
        _Alpha_Dis ("Alpha Dis", Float) = 0
    }
    
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
        
        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            
            #include "UnityCG.cginc"
            
            struct a2v
            {
                float4 vertex: POSITION;
                float2 uv: TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct v2f
            {
                float2 uv: TEXCOORD0;
                float2 uv2: TEXCOORD1;
                float4 vertex: SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Alpha_Dis;
            
            UNITY_INSTANCING_CBUFFER_START(MyProperties)
            UNITY_DEFINE_INSTANCED_PROP(float, _UV_Scale)
            UNITY_DEFINE_INSTANCED_PROP(float, _Speed)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
            UNITY_DEFINE_INSTANCED_PROP(float, _AlphaEnable)
            UNITY_INSTANCING_CBUFFER_END
            
            v2f vert(a2v v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = o.uv;
                o.uv.x *= UNITY_ACCESS_INSTANCED_PROP(_UV_Scale);
                o.uv.x -= _Time.g * UNITY_ACCESS_INSTANCED_PROP(_Speed);
                
                return o;
            }
            
            fixed4 frag(v2f i): SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                fixed4 mainTexCol = tex2D(_MainTex, i.uv);
                float scale = UNITY_ACCESS_INSTANCED_PROP(_UV_Scale);
                fixed4 color = UNITY_ACCESS_INSTANCED_PROP(_Color);
                float alphaEnable = UNITY_ACCESS_INSTANCED_PROP(_AlphaEnable);
                float a = (1 - (1 - 0) / (scale / _Alpha_Dis)) * step(0.5, alphaEnable);
                color.a = smoothstep(a, 1, i.uv2.x) + smoothstep(a, 1, 1 - i.uv2.x);
                fixed4 albedo = mainTexCol * color;
                
                // fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT * albedo.rgb;
                // float3 worldNormal = UnityObjectToWorldNormal(i.normal);
                // float3 lightDir = UnityWorldSpaceLightDir(i.vertex);
                // fixed3 diffuse = _LightColor0.rgb * albedo.rgb * saturate(dot(worldNormal, lightDir));
                
                return fixed4(albedo.rgb, mainTexCol.a * color.a);
            }
            ENDCG
            
        }
    }
    
    Fallback "Legacy Shaders/Transparent/VertexLit"
}
