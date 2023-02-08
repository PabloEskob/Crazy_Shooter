using UnityEngine;

namespace Source.Scripts.PostProcess
{
    public class PostProcess : MonoBehaviour
    {
        [SerializeField] private Shader _shader;

        private Material _material;

        private void Start() => 
            _material = new Material(_shader);

        private void OnRenderImage(RenderTexture src, RenderTexture dest) => 
            Graphics.Blit(src, dest, _material);
    }
}