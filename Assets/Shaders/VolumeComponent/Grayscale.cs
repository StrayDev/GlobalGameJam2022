using System;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

[Serializable, VolumeComponentMenu("Post-processing/Custom/Grayscale")]
public sealed class Grayscale : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    [Tooltip("Control intensity of effect")]
    public ClampedFloatParameter intensity = new ClampedFloatParameter(0.0f, 0.0f, 1.0f);

    private Material m_Material;

    public bool IsActive() => m_Material != null && intensity.value > 0.0f;
    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;
    public override void Setup()
    {
        if (Shader.Find("FullScreen/GrayscalePP") != null)
        {
            m_Material = new Material(Shader.Find("FullScreen/GrayscalePP"));
        }
    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destiniation)
    {
        if (m_Material == null) return;
        
        m_Material.SetFloat("_Intensity", intensity.value);
        m_Material.SetTexture("_InputTexture", source);
        
        HDUtils.DrawFullScreen(cmd, m_Material, destiniation);
    }

    public override void Cleanup() => CoreUtils.Destroy(m_Material);
}