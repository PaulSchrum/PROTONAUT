using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protonaut.addons.Protonaut
{
    internal abstract class PrimitiveBase
    {
        protected Vector3 AnchorPt;
        protected MeshInstance3D MeshInstance;
        protected Material Material;
        protected Material BackMaterial;

        private Transform3D ObjectTransform;

        public PrimitiveBase(Vector3 point3D, Material material = null, Material backMaterial = null)
        {
            AnchorPt = point3D;
            Material = material;
            BackMaterial = backMaterial;

            MeshInstance = new MeshInstance3D();
            MeshInstance.Mesh = MakeMesh(point3D);
            MeshInstance.MaterialOverride = material;
            ObjectTransform = MeshInstance.Transform;
        }

        protected abstract Mesh MakeMesh(Vector3 pt3);

        public void MoveTo(Vector3 newPosition)
        {
            Vector3 translation = newPosition - AnchorPt;

            // Update the existing transform with new translation
            Transform3D newTransform = new Transform3D(ObjectTransform.Basis, 
                ObjectTransform.Origin + translation);
            MeshInstance.Transform = newTransform;
            ObjectTransform = newTransform;
        }

        public void MoveBy(Vector3 deltaPositions)
        {
            MoveTo(AnchorPt + deltaPositions);
        }

        public void MoveBy(Vector3 velocityVectorUps, float timeDeltaSeconds)
        {
            MoveBy(velocityVectorUps * timeDeltaSeconds);
        }

    }
}
