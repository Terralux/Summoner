// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-2460-OUT,custl-5085-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:8068,x:32553,y:33109,varname:node_8068,prsc:2;n:type:ShaderForge.SFN_LightColor,id:3406,x:32553,y:32953,varname:node_3406,prsc:2;n:type:ShaderForge.SFN_LightVector,id:6869,x:31673,y:33031,varname:node_6869,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:9684,x:31382,y:32777,prsc:2,pt:True;n:type:ShaderForge.SFN_Dot,id:7782,x:31994,y:32921,cmnt:Lambert,varname:node_7782,prsc:2,dt:0|A-9684-OUT,B-6869-OUT;n:type:ShaderForge.SFN_Tex2d,id:851,x:31962,y:32299,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1fb22b39ffa4d154b9a57af90e2d9a3f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5085,x:32979,y:32952,cmnt:Attenuate and Color,varname:node_5085,prsc:2|A-1517-OUT,B-5673-OUT;n:type:ShaderForge.SFN_AmbientLight,id:7528,x:32734,y:32544,varname:node_7528,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2460,x:32933,y:32486,cmnt:Ambient Light,varname:node_2460,prsc:2|A-3652-OUT,B-7528-RGB;n:type:ShaderForge.SFN_Multiply,id:5673,x:32750,y:33036,cmnt:Attenuate and Color,varname:node_5673,prsc:2|A-3406-RGB,B-8068-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7617,x:31994,y:33093,ptovrint:False,ptlb:Full Light,ptin:_FullLight,varname:_node_1304_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_ValueProperty,id:9308,x:31994,y:33176,ptovrint:False,ptlb:Shadow,ptin:_Shadow,varname:_node_1304_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.3;n:type:ShaderForge.SFN_If,id:1073,x:32266,y:32956,varname:node_1073,prsc:2|A-7782-OUT,B-7617-OUT,GT-7617-OUT,EQ-7617-OUT,LT-9308-OUT;n:type:ShaderForge.SFN_Add,id:7250,x:32577,y:32744,varname:node_7250,prsc:2|A-3493-OUT,B-1073-OUT;n:type:ShaderForge.SFN_Multiply,id:1517,x:32776,y:32714,varname:node_1517,prsc:2|A-851-RGB,B-7250-OUT;n:type:ShaderForge.SFN_Slider,id:5608,x:31614,y:32470,ptovrint:False,ptlb:RimLightWidth,ptin:_RimLightWidth,varname:_node_2335_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7169878,max:1;n:type:ShaderForge.SFN_Multiply,id:3493,x:32363,y:32679,varname:node_3493,prsc:2|A-8873-OUT,B-8383-RGB;n:type:ShaderForge.SFN_Color,id:8383,x:32119,y:32771,ptovrint:False,ptlb:Outline Color,ptin:_OutlineColor,varname:node_8383,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_ViewVector,id:9322,x:31347,y:32522,varname:node_9322,prsc:2;n:type:ShaderForge.SFN_Dot,id:2060,x:31637,y:32551,cmnt:Lambert,varname:node_2060,prsc:2,dt:0|A-9322-OUT,B-9684-OUT;n:type:ShaderForge.SFN_OneMinus,id:6023,x:31860,y:32551,varname:node_6023,prsc:2|IN-2060-OUT;n:type:ShaderForge.SFN_If,id:8873,x:32071,y:32551,varname:node_8873,prsc:2|A-6023-OUT,B-5608-OUT,GT-8954-OUT,EQ-8954-OUT,LT-4284-OUT;n:type:ShaderForge.SFN_Vector1,id:4284,x:31693,y:32775,varname:node_4284,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:8954,x:31693,y:32708,varname:node_8954,prsc:2,v1:1;n:type:ShaderForge.SFN_If,id:3652,x:32615,y:32461,varname:node_3652,prsc:2|A-3493-OUT,B-2685-OUT,GT-3493-OUT,EQ-851-RGB,LT-851-RGB;n:type:ShaderForge.SFN_Vector1,id:2685,x:32357,y:32351,varname:node_2685,prsc:2,v1:0;proporder:851-7617-9308-5608-8383;pass:END;sub:END;*/

Shader "Shader Forge/Trailblazer Test Shader" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _FullLight ("Full Light", Float ) = 0.5
        _Shadow ("Shadow", Float ) = 0.3
        _RimLightWidth ("RimLightWidth", Range(0, 1)) = 0.7169878
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
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
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _FullLight;
            uniform float _Shadow;
            uniform float _RimLightWidth;
            uniform float4 _OutlineColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float node_8873_if_leA = step((1.0 - dot(viewDirection,normalDirection)),_RimLightWidth);
                float node_8873_if_leB = step(_RimLightWidth,(1.0 - dot(viewDirection,normalDirection)));
                float node_8954 = 1.0;
                float3 node_3493 = (lerp((node_8873_if_leA*0.0)+(node_8873_if_leB*node_8954),node_8954,node_8873_if_leA*node_8873_if_leB)*_OutlineColor.rgb);
                float node_3652_if_leA = step(node_3493,0.0);
                float node_3652_if_leB = step(0.0,node_3493);
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 emissive = (lerp((node_3652_if_leA*_Diffuse_var.rgb)+(node_3652_if_leB*node_3493),_Diffuse_var.rgb,node_3652_if_leA*node_3652_if_leB)*UNITY_LIGHTMODEL_AMBIENT.rgb);
                float node_1073_if_leA = step(dot(normalDirection,lightDirection),_FullLight);
                float node_1073_if_leB = step(_FullLight,dot(normalDirection,lightDirection));
                float3 finalColor = emissive + ((_Diffuse_var.rgb*(node_3493+lerp((node_1073_if_leA*_Shadow)+(node_1073_if_leB*_FullLight),_FullLight,node_1073_if_leA*node_1073_if_leB)))*(_LightColor0.rgb*attenuation));
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
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _FullLight;
            uniform float _Shadow;
            uniform float _RimLightWidth;
            uniform float4 _OutlineColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float node_8873_if_leA = step((1.0 - dot(viewDirection,normalDirection)),_RimLightWidth);
                float node_8873_if_leB = step(_RimLightWidth,(1.0 - dot(viewDirection,normalDirection)));
                float node_8954 = 1.0;
                float3 node_3493 = (lerp((node_8873_if_leA*0.0)+(node_8873_if_leB*node_8954),node_8954,node_8873_if_leA*node_8873_if_leB)*_OutlineColor.rgb);
                float node_1073_if_leA = step(dot(normalDirection,lightDirection),_FullLight);
                float node_1073_if_leB = step(_FullLight,dot(normalDirection,lightDirection));
                float3 finalColor = ((_Diffuse_var.rgb*(node_3493+lerp((node_1073_if_leA*_Shadow)+(node_1073_if_leB*_FullLight),_FullLight,node_1073_if_leA*node_1073_if_leB)))*(_LightColor0.rgb*attenuation));
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
