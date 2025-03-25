using UnityEngine;
using UnityEngine.UI;

public class BlurEffectLayer : MonoBehaviour
    {
        public bool m_isRuntimeBlur = false;
        public Camera m_camera;
        public RawImage m_rawImage;
        public Shader CurShader;

        [Range(0, 6)]
        public int DownSampleNum = 1;
        [Range(0.0f, 20.0f)]
        public float BlurSpreadSize = 0.0f;
        [Range(0, 8)]
        public int BlurIterations = 4;

        private string ShaderName = "Learning Unity Shader/Lecture 15/RapidBlurEffect";
        private RenderTexture renderBuffer;
        private Material CurMaterial;
        private Color m_colorRawImage;
        Material material
        {
            get
            {
                if (CurMaterial == null)
                {
                    CurMaterial = new Material(CurShader);
                    CurMaterial.hideFlags = HideFlags.HideAndDontSave;
                }
                return CurMaterial;
            }
        }

        void Start()
        {
            if (m_camera == null)
            {
                enabled = false;
                Debug.LogError("m_camera == null");
                return;
            }

            if (m_rawImage == null)
            {
                m_camera.enabled = false;
                enabled = false;
                Debug.LogError("m_rawImage == null");
                return;
            }

            //判断当前设备是否支持屏幕特效
            if (!SystemInfo.supportsImageEffects)
            {
                m_camera.enabled = false;
                enabled = false;
                Debug.LogError("SystemInfo.supportsImageEffects: false");
                return;
            }

            if (null == CurShader)
            {
                CurShader = Shader.Find(ShaderName);
            }

            m_colorRawImage = m_rawImage.color;
            m_colorRawImage.a = 1;
        }

        void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
        {
            if (CurShader != null)
            {
                float widthMod = 1.0f / (1.0f * (1 << DownSampleNum));
                material.SetFloat("_DownSampleValue", BlurSpreadSize * widthMod);
                sourceTexture.filterMode = FilterMode.Bilinear;
                int renderWidth = sourceTexture.width >> DownSampleNum;
                int renderHeight = sourceTexture.height >> DownSampleNum;


                renderBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, RenderTextureFormat.RGB111110Float); //sourceTexture.format
                renderBuffer.filterMode = FilterMode.Bilinear;
                Graphics.Blit(sourceTexture, renderBuffer, material, 0);

                for (int i = 0; i < BlurIterations; i++)
                {
                    float iterationOffs = (i * 1.0f);
                    material.SetFloat("_DownSampleValue", BlurSpreadSize * widthMod + iterationOffs);

                    RenderTexture tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, RenderTextureFormat.RGB111110Float); //sourceTexture.format    
                    Graphics.Blit(renderBuffer, tempBuffer, material, 1);
                    RenderTexture.ReleaseTemporary(renderBuffer);
                    renderBuffer = tempBuffer;

                    tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, RenderTextureFormat.RGB111110Float); //sourceTexture.format    
                    Graphics.Blit(renderBuffer, tempBuffer, material, 2);
                    RenderTexture.ReleaseTemporary(renderBuffer);
                    renderBuffer = tempBuffer;
                }

                Graphics.Blit(renderBuffer, destTexture);

                if (m_isRuntimeBlur)
                {
                    if (null != renderBuffer)
                    {
                        RenderTexture.ReleaseTemporary(renderBuffer);
                    }
                }
                else
                {
                    if (m_rawImage)
                    {
                        m_rawImage.texture = renderBuffer;
                        m_rawImage.color = m_colorRawImage;
                    }

                    // if (m_camera)
                    // {
                    //     m_camera.enabled = false;
                    // }

                    enabled = false;
                }
            }
            else
            {
                Graphics.Blit(sourceTexture, destTexture);
            }
        }

        private void OnEnable()
        {
            if (null != m_rawImage)
            {
                m_rawImage.color = new Color(m_rawImage.color.r, m_rawImage.color.g, m_rawImage.color.b, 0);
            }
        }

        void OnDestroy()
        {
            if (CurMaterial)
            {
                DestroyImmediate(CurMaterial);
            }

            if (!m_isRuntimeBlur)
            {
                if (renderBuffer != null || m_rawImage.texture != null)
                {
                    RenderTexture.ReleaseTemporary(renderBuffer);
                    renderBuffer = null;
                    m_rawImage.texture = null;
                }
            }
        }
    }