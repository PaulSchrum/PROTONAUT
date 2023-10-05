#if TOOLS
using Godot;
using System;
using System.Linq;

namespace Protonaut.addons.Protonaut
{
    public partial class PointViz : Godot.MeshInstance3D
    {
        private Vector3 position;
        private ArrayMesh mesh;
        private Material material = new StandardMaterial3D();
        protected Material BackMaterial = new StandardMaterial3D();
        private Transform3D objectTransform;
        private const float POINT_SIZE = 1.25f;
        private const float SQRT_PT_SIZE = POINT_SIZE * 0.70711f;

        protected PointViz() : base()
        { }

        public static PointViz PointVizFactory(Node scene, 
            float x, float y, float z)
        {
            PointViz retVar = new PointViz();
            //var retVar = new PointViz(x, y, z);
            retVar.position = new Vector3(x, y, z);

            InitializeMesh(retVar);


            scene.AddChild(retVar);

            // diagnostic
            var c = scene.GetChildren().Select(c => c.Name).ToList();

            return retVar;
        }

        private static void InitializeMesh(PointViz ptv)
        {
            float baseY = ptv.position.Y - POINT_SIZE;
            var mesh = new ArrayMesh();

            // Define vertices and indices to form a pyramid
            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(ptv.position.X, ptv.position.Y, ptv.position.Z);
            vertices[1] = new Vector3(ptv.position.X,
                baseY, ptv.position.Z + POINT_SIZE);
            vertices[2] = new Vector3(ptv.position.X+SQRT_PT_SIZE, 
                baseY, ptv.position.Z - SQRT_PT_SIZE);
            vertices[3] = new Vector3(ptv.position.X - SQRT_PT_SIZE,
                baseY, ptv.position.Z - SQRT_PT_SIZE);

            SurfaceTool surfaceTool = new SurfaceTool();
            surfaceTool.Begin(Mesh.PrimitiveType.Triangles);

            foreach (Vector3 vertex in vertices)
            {
                surfaceTool.AddVertex(vertex);
            }

            // Define the indices to connect the vertices into four triangular faces
            // Base: vertices 1, 2, 3
            GD.Print("0");
            surfaceTool.AddIndex(1);
            surfaceTool.AddIndex(2);
            surfaceTool.AddIndex(3);

            // Sides: vertices 0, 1, 2, 3
            surfaceTool.AddIndex(0);
            surfaceTool.AddIndex(2);
            surfaceTool.AddIndex(1);

            surfaceTool.AddIndex(0);
            surfaceTool.AddIndex(3);
            surfaceTool.AddIndex(2);

            surfaceTool.AddIndex(0);
            surfaceTool.AddIndex(1);
            surfaceTool.AddIndex(3);

            // Advice from Chat-GPT:
            // You might want to specify normals, uvs etc here using SurfaceTool methods.

            mesh.SurfaceSetName(0, "A Point");
            surfaceTool.Commit(mesh, 0);

            // Set default material
            ((StandardMaterial3D)ptv.material).AlbedoColor = new Color(1, 0, 0); // Red

            // Apply mesh and material
            ptv.Mesh = mesh;
            ptv.Mesh.SurfaceSetMaterial(0, ptv.material);

            // Set initial transform
            ptv.objectTransform = new Transform3D();
            ptv.objectTransform.Basis = new Basis();
            ptv.objectTransform.Origin = ptv.position;
        }

        public void MoveTo(Vector3 newPosition)
        {
            objectTransform.Origin = newPosition;
            this.Transform = objectTransform;
        }
    }

}
#endif