#if TOOLS
using Godot;
using System;

namespace Protonaut.addons.Protonaut
{
    public partial class PointViz : MeshInstance3D
    {
        private Vector3 position;
        private ArrayMesh mesh;
        private Material material = new StandardMaterial3D();
        protected Material BackMaterial = new StandardMaterial3D();
        private Transform3D objectTransform;

        public PointViz(float x, float y, float z) : this(new Vector3(x, y, z))
        {
        }

        public PointViz(Vector3 position)
        {
            this.position = position;
            InitializeMesh();
            this.Transform = objectTransform;
        }

        private void InitializeMesh()
        {
            mesh = new ArrayMesh();
            SurfaceTool surfaceTool = new SurfaceTool();

            // Define vertices and indices to form a pyramid (adjust as needed)
            // Omitted for brevity...

            surfaceTool.Commit(mesh, 0);

            // Set default material
            ((StandardMaterial3D)material).AlbedoColor = new Color(1, 0, 0); // Red

            // Apply mesh and material
            this.Mesh = mesh;
            this.Mesh.SurfaceSetMaterial(0, material);

            // Set initial transform
            objectTransform = new Transform3D();
            objectTransform.Basis = new Basis();
            objectTransform.Origin = position;
        }

        public void MoveTo(Vector3 newPosition)
        {
            objectTransform.Origin = newPosition;
            this.Transform = objectTransform;
        }
    }

}
#endif