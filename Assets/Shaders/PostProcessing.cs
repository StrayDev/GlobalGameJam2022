using System;
using UnityEngine;

namespace Shaders
{
    [ExecuteInEditMode]
    [ImageEffectAllowedInSceneView]
    public class PostProcessing : MonoBehaviour
    {
        [SerializeField] public Shader shader;
        [SerializeField] public Texture2D textureGradient;
        private Material m_Material;

        private void Start()
        {
            if (Camera.main == null) return;
            
            Camera.main.depthTextureMode = DepthTextureMode.Depth;

            if (m_Material == null)
            {
                m_Material = new Material(shader);
            }
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (m_Material == null)
            {
                m_Material = new Material(shader);
            }
            
            // Send to shader
            m_Material.SetTexture("_gradientTex", textureGradient);
            
            // Render to screen
            Graphics.Blit(src, dest, m_Material);
        }
    }
}