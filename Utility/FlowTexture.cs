using UnityEngine;
using UnityEngine.UI;

namespace ProjectD
{
    [RequireComponent(typeof(Image))]
    public class FlowTexture : MonoBehaviour
    {
        [SerializeField] private Vector2 flowSpeed;

        private Material material;
        private Vector2 currentOffset;
        private int offsetProperty = Shader.PropertyToID("_MainTex");

        private void Awake()
        {
            if (TryGetComponent(out Image targetImage) && targetImage.material != null)
            {
                material = targetImage.material;
            }
            else
            {
                Debug.LogError("No Image or No Material");
            }
        }

        private void Update()
        {
            Flow();
        }

        private void Flow()
        {
            if (material != null)
            {
                currentOffset += flowSpeed * Time.deltaTime;
                material.SetTextureOffset(offsetProperty, currentOffset);
            }
        }
    }
}
