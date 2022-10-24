using UnityEngine;
using Utils;

namespace DefaultNamespace
{
    public class OverlapChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private BoxCollider objectCollider;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material notAvailableMaterial;
        [SerializeField] private MeshRenderer meshRenderer;
        
        public bool CanSpawnInLocation(Vector3 position)
        {
            Collider[] hitColliders = Physics.OverlapBox(transform.position, 
                MathUtil.AddToVector3(objectCollider.size, -0.1f)/2, Quaternion.identity, ~layerMask);

            return hitColliders.Length <= 1;
        }

        public void ChangeMaterialAvailable(bool isAvailable)
        {
            meshRenderer.material = isAvailable? defaultMaterial:notAvailableMaterial;
        }

    }
}