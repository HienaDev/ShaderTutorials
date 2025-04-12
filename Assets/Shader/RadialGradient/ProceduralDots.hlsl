// HLSL Shader: Fermat Spiral with Animated Circles

// Shader inputs
Texture2D<float4> iChannel0 : register(t0); // Texture input (optional, could be a background)
SamplerState iSampler : register(s0); // Sampler for the texture
float4 iResolution : register(c0); // Screen resolution
float iTime : register(c1); // Time variable (used for animation)

// Function to compute the distance from the current pixel to the circle center
float mainImage(float2 uv : TEXCOORD) : SV_Target
{
    // Normalize the coordinates
    float2 resolution = iResolution.xy;
    float2 normalizedCoord = (2.0 * uv - resolution) / resolution.y;

    // Initialize variables
    float i = 0.0; // Loop counter for number of circles
    float d = 0.0; // Distance accumulator for circles' intensity

    // Loop through multiple circles
    while (i < 10.0)
    {
        // Calculate the position of the circle at angle 'i'
        float2 circlePos = float2(cos(0.63 * i), sin(0.63 * i)) * 0.7;

        // Compute the distance from the current pixel to the circle position
        float distance = length(normalizedCoord - circlePos);

        // Compute the radius of the circle, oscillating over time
        float radius = 0.1 * frac(iTime * -0.8 - i * 0.1);

        // Step function to check if the pixel is inside the circle
        d += step(distance - radius, 0.0);

        // Increment the loop counter
        i++;
    }

    // Output the final intensity as a color (white for the spiral)
    return float4(d, d, d, 1.0);
}

// Entry point for the shader
technique11 Render
{
    pass P0
    {
        SetShaderResource(iChannel0, t0);
        SetSampler(iSampler, s0);
        SetPixelShader(mainImage);
    }
}
