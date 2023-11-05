using StandardAssets.Characters.Physics;
using UnityEngine;

namespace MultiplayerARPG
{
    public class RigidBodyEntityMovementFactory : IEntityMovementFactory
    {
        public string Name => "Rigid Body Entity Movement";

        public DimensionType DimensionType => DimensionType.Dimension3D;

        public RigidBodyEntityMovementFactory()
        {

        }

        public bool ValidateSourceObject(GameObject obj)
        {
            return true;
        }

        public IEntityMovementComponent Setup(GameObject obj, ref Bounds bounds)
        {
            bounds = default;
            MeshRenderer[] meshes = obj.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshes.Length; ++i)
            {
                if (i > 0)
                    bounds.Encapsulate(meshes[i].bounds);
                else
                    bounds = meshes[i].bounds;
            }

            SkinnedMeshRenderer[] skinnedMeshes = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < skinnedMeshes.Length; ++i)
            {
                if (i > 0)
                    bounds.Encapsulate(skinnedMeshes[i].bounds);
                else
                    bounds = skinnedMeshes[i].bounds;
            }

            float scale = Mathf.Max(obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
            bounds.size = bounds.size / scale;
            bounds.center = bounds.center / scale;

            obj.AddComponent<Rigidbody>();
            CapsuleCollider capsuleCollider = obj.AddComponent<CapsuleCollider>();
            capsuleCollider.height = bounds.size.y;
            capsuleCollider.radius = Mathf.Min(bounds.extents.x, bounds.extents.z);
            capsuleCollider.center = Vector3.zero + (Vector3.up * capsuleCollider.height * 0.5f);

            OpenCharacterController openCharacterController = obj.AddComponent<OpenCharacterController>();
            openCharacterController.SetRadiusHeightAndCenter(capsuleCollider.radius, capsuleCollider.height, capsuleCollider.center, false, false);

            return obj.AddComponent<RigidBodyEntityMovement>();
        }
    }
}
