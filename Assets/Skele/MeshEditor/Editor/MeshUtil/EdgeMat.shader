﻿Shader "Skele/EdgeMat" {
        
    SubShader { //DX9 shader
     Tags {"Queue"="Overlay+50" "IgnoreProjector"="True" "RenderType"="Transparent"}
     Pass {

         LOD 200
         Offset -1, -1
         ZWrite Off
                  
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
         #include "UnityCG.cginc"
   
         struct VertexInput {
             float4 v : POSITION;
             float4 color: COLOR;
         };
          
         struct VertexOutput {
             float4 pos : SV_POSITION;
             float4 col : COLOR;             
         };
         
          
         VertexOutput vert(VertexInput v) {          
             VertexOutput o;
             o.pos = mul(UNITY_MATRIX_MVP, v.v);
             o.pos.z -= 0.001f;
             o.col = v.color;
             return o;
         }
          
         float4 frag(VertexOutput o) : COLOR {
             return o.col;
         }
  
         ENDCG
         } 
     }
  
 }