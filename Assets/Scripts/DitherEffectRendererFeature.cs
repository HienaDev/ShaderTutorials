using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Apple;
using UnityEngine.Rendering.RenderGraphModule.Util;

public class DitherEffectRendererFeature : ScriptableRendererFeature
{
    class DitherEffectPass : ScriptableRenderPass
    {

        const string passName = "DitherEffectPass";
        Material blitMaterial;

        public void Setup(Material mat)
        {
            blitMaterial = mat;
            requiresIntermediateTexture = true;
        }



        // RecordRenderGraph is where the RenderGraph handle can be accessed, through which render passes can be added to the graph.
        // FrameData is a context container through which URP resources can be accessed and managed.
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            var stack = VolumeManager.instance.stack;
            var customEffect = stack.GetComponent<SphereVolumeComponent>();

            if (!customEffect.IsActive())
                return;

            var resourceData = frameData.Get<UniversalResourceData>();

            if(resourceData.isActiveTargetBackBuffer)
            {
                Debug.LogWarning("DitherEffectPass: RenderGraph is not supported with backbuffer target");
                return;
            }

            var source = resourceData.activeColorTexture;

            var destionationDesc = renderGraph.GetTextureDesc(source);
            destionationDesc.name = $"CameraColor-{passName}";
            destionationDesc.clearBuffer = false;

            TextureHandle destination = renderGraph.CreateTexture(destionationDesc);

            RenderGraphUtils.BlitMaterialParameters para = new(source, destination, blitMaterial, 0);
            renderGraph.AddBlitPass(para, passName: passName);

            resourceData.cameraColor = destination;
        }


    }

    public RenderPassEvent injectionPoint = RenderPassEvent.AfterRenderingPostProcessing;
    public Material material;

    DitherEffectPass m_ScriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        m_ScriptablePass = new DitherEffectPass();

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = injectionPoint;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {

        if(material == null)
        {
            Debug.LogWarning("Missing material for DitherEffectRendererFeature.");
            return;
        }

        m_ScriptablePass.Setup(material);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}
