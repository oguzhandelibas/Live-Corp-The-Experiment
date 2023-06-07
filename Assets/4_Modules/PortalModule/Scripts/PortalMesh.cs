using UnityEngine;

namespace PortalModule
{
    [RequireComponent(typeof(Renderer))]
    public class PortalMesh : MonoBehaviour
    {
        [SerializeField] private string viewTextureName = "_MainTex";
        [SerializeField] private PortalView view;
        [SerializeField] private int materialId;

        private Renderer attachedRenderer;

        private void Start()
        {
            attachedRenderer = GetComponent<Renderer>();
            view.OnRenderTextureChanged += OnRenderTextureChanged;
        }

        private void OnRenderTextureChanged()
        {
            var materials = attachedRenderer.materials;
            materials[materialId].SetTexture(viewTextureName, view.RenderTexture);
            attachedRenderer.materials = materials;
        }
    }
}