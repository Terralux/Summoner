// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-1835-OUT;n:type:ShaderForge.SFN_ChannelBlend,id:1835,x:32506,y:32677,varname:node_1835,prsc:2,chbt:0|M-3918-OUT,R-612-RGB,G-9742-RGB,B-6866-RGB;n:type:ShaderForge.SFN_Tex2d,id:612,x:32216,y:32692,ptovrint:False,ptlb:node_612,ptin:_node_612,varname:node_612,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e810661a9654ab94e807f0fecdfbce52,ntxv:0,isnm:False|UVIN-773-OUT;n:type:ShaderForge.SFN_NormalVector,id:4575,x:31958,y:32523,prsc:2,pt:False;n:type:ShaderForge.SFN_Append,id:8503,x:31954,y:33082,varname:node_8503,prsc:2|A-4015-X,B-4015-Y;n:type:ShaderForge.SFN_FragmentPosition,id:4015,x:31697,y:32815,varname:node_4015,prsc:2;n:type:ShaderForge.SFN_Append,id:1585,x:31954,y:32923,varname:node_1585,prsc:2|A-4015-Z,B-4015-X;n:type:ShaderForge.SFN_Tex2d,id:9742,x:32216,y:32889,ptovrint:False,ptlb:node_612_copy,ptin:_node_612_copy,varname:_node_612_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ae8cd00a95824724088f8e6ea825fe47,ntxv:0,isnm:False|UVIN-1585-OUT;n:type:ShaderForge.SFN_Append,id:773,x:31954,y:32770,varname:node_773,prsc:2|A-4015-Y,B-4015-Z;n:type:ShaderForge.SFN_Tex2d,id:6866,x:32216,y:33067,ptovrint:False,ptlb:node_612_copy_copy,ptin:_node_612_copy_copy,varname:_node_612_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ba0dd41def2e15e439c109b603b4ea61,ntxv:0,isnm:False|UVIN-8503-OUT;n:type:ShaderForge.SFN_Multiply,id:3918,x:32167,y:32533,varname:node_3918,prsc:2|A-4575-OUT,B-4575-OUT;proporder:612-9742-6866;pass:END;sub:END;*/

Shader "Shader Forge/Triplanar" {
    Properties {
        _node_612 ("node_612", 2D) = "white" {}
        _node_612_copy ("node_612_copy", 2D) = "white" {}
        _node_612_copy_copy ("node_612_copy_copy", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _node_612; uniform float4 _node_612_ST;
            uniform sampler2D _node_612_copy; uniform float4 _node_612_copy_ST;
            uniform sampler2D _node_612_copy_copy; uniform float4 _node_612_copy_copy_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 node_3918 = (i.normalDir*i.normalDir);
                float2 node_773 = float2(i.posWorld.g,i.posWorld.b);
                float4 _node_612_var = tex2D(_node_612,TRANSFORM_TEX(node_773, _node_612));
                float2 node_1585 = float2(i.posWorld.b,i.posWorld.r);
                float4 _node_612_copy_var = tex2D(_node_612_copy,TRANSFORM_TEX(node_1585, _node_612_copy));
                float2 node_8503 = float2(i.posWorld.r,i.posWorld.g);
                float4 _node_612_copy_copy_var = tex2D(_node_612_copy_copy,TRANSFORM_TEX(node_8503, _node_612_copy_copy));
                float3 diffuseColor = (node_3918.r*_node_612_var.rgb + node_3918.g*_node_612_copy_var.rgb + node_3918.b*_node_612_copy_copy_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _node_612; uniform float4 _node_612_ST;
            uniform sampler2D _node_612_copy; uniform float4 _node_612_copy_ST;
            uniform sampler2D _node_612_copy_copy; uniform float4 _node_612_copy_copy_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_3918 = (i.normalDir*i.normalDir);
                float2 node_773 = float2(i.posWorld.g,i.posWorld.b);
                float4 _node_612_var = tex2D(_node_612,TRANSFORM_TEX(node_773, _node_612));
                float2 node_1585 = float2(i.posWorld.b,i.posWorld.r);
                float4 _node_612_copy_var = tex2D(_node_612_copy,TRANSFORM_TEX(node_1585, _node_612_copy));
                float2 node_8503 = float2(i.posWorld.r,i.posWorld.g);
                float4 _node_612_copy_copy_var = tex2D(_node_612_copy_copy,TRANSFORM_TEX(node_8503, _node_612_copy_copy));
                float3 diffuseColor = (node_3918.r*_node_612_var.rgb + node_3918.g*_node_612_copy_var.rgb + node_3918.b*_node_612_copy_copy_var.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
