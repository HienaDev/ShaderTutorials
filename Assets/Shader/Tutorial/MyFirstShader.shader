Shader "Introduction/MyFirstShader"
{
    Properties
    {
        _Color("Test Color", color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert // Runs on every vertex, this is the v2f vert called down there
            #pragma fragment frag // Runs on every single pixel on the screen, 
                                  //this is the fixed frag called down there

            #include "UnityCG.cginc"

            struct appdata // Object Data or Mesh, all of the information from the Object
                           // comes through here
            {
                float4 vertex : POSITION; // Position of each vertex, POSITION is a type of data

            };

            struct v2f // Vertex to fragment, pass data from vertex shader to fragment shader
            {
                float4 vertex : SV_POSITION; // SV_POSITION is same as POSITION but some GPUS
                                             //dont support POSITION but all support SV_POSITION
            };

            fixed4 _Color;

            v2f vert (appdata v) // Vertex shader, is type v2f and returns a v2f
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); //UnityObjectToClipPos is multiplying
                                                           // the vertex against the MVP matrix
                                                           // MPV: Model, View, Projection
                                                           // Model: turns local space into world space
                                                           // View: shifts the world so that the camera is in the right position
                                                           // Projection: camera isnt ortographic, so needs to some projection math
                                                           // on the object to looks right on screen
                return o;
            }

            fixed4 frag (v2f i) : SV_Target // Returns fixed4, takes in a v2f
            {
                fixed4 col = _Color;
                return col;
            }
            ENDCG
        }
    }
}
