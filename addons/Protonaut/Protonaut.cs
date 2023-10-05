#if TOOLS
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Protonaut.addons.Protonaut
{
    [Tool]
    public partial class Protonaut : EditorPlugin
    {
        private List<PointViz> pointMeshes = new List<PointViz>();

        private VBoxContainer dock;
        private HBoxContainer MenuBar { get; set; }
        private MenuButton fileMenu;
        private MenuButton actionsMenu;

        private const int FILE_OPEN = 0;
        private const int FILE_CLOSE = 1;

        private void DbgPrint(string s)
        {
            bool printDebug = false;
            if(printDebug) 
            {
                GD.Print(s);
            }
        }


        public override void _EnterTree()
        {
            DbgPrint("Entered _EnterTree.");
            dock = new VBoxContainer();
            dock.Name = "Protonaut";
            AddControlToDock(DockSlot.LeftUl, dock);
            MenuBar = new HBoxContainer();
            dock.AddChild(MenuBar);

            // Create a MenuButton for the "File" menu.
            fileMenu = new MenuButton { Text = "File" };
            MenuBar.AddChild(fileMenu);

            // Add items to the "File" menu.
            fileMenu.GetPopup().AddItem("Open");
            fileMenu.GetPopup().AddItem("Close");

            // Connect signals for the "File" menu items.
            fileMenu.GetPopup().Connect("id_pressed", new Callable(this, "OnFileMenuPressed"));

            // Create a MenuButton for the "Edit" menu.
            actionsMenu = new MenuButton { Text = "Actions" };
            MenuBar.AddChild(actionsMenu);

            // Add items to the "Edit" menu.
            actionsMenu.GetPopup().AddItem("Create Points"); // ACTION_CREATE_POINTS
            actionsMenu.GetPopup().AddItem("Animate Points"); // ACTION_ANIMATE_POINTS

            // Connect signals for the "Edit" menu items.
            actionsMenu.GetPopup().Connect("id_pressed",
                        new Callable(this, "CreateManyPoints"));

        }

        public override void _ExitTree()
        {
            RemoveControlFromDocks(dock);
            dock.QueueFree();
        }

        public void OnFileMenuPressed(int id)
        {
            switch (id)
            {
                case FILE_OPEN: GD.Print("Message Only: Open selected"); break;
                case FILE_CLOSE:
                    {
                        GD.Print("Close selected");
                        RemoveControlFromDocks(dock);
                        dock.QueueFree();
                        break;
                    }
                default: break;
            }
        }

        private void CreateManyPoints(int numberOfPoints = 20)
        {
            DbgPrint("Entered CreateManyPointses.");
            int numPts = numberOfPoints;
            if(numPts == 0) { numPts = 50; }
            EditorInterface editorInterface = GetEditorInterface();
            Node editedSceneRoot = editorInterface.GetEditedSceneRoot();

            double radius = 100.0;
            double thickness = 5.0;

            double angleRad = 0.0;
            double radiusInstance = 0.0;
            double height = 0.0;

            float x = 0f; float y = 0f; float z = 0f;

            Random random = new Random();
            for (int i = 0; i < numPts; i++)
            {
                if(i % 100 == 0)
                    GD.Print($"i: {i}");
                angleRad = random.NextDouble() * 2 * Math.PI;
                radiusInstance = Math.Sqrt(random.NextDouble()) * radius;
                height = random.NextDouble() * 10.0;

                x = (float) (radiusInstance * Math.Cos(angleRad));
                z = (float) (radiusInstance * Math.Sin(angleRad));
                y = (float) height;

                try
                {
                    var newPt = PointViz.PointVizFactory(editedSceneRoot, x, y, z);
                    pointMeshes.Add(newPt);
                }
                catch (Exception e)
                {
                    //lbl_speed.Content = $"Overflow: {i} pts.";
                    GD.Print(e.ToString());
                    break;
                }
            }
            DbgPrint("Exited CreateManyPoints.");

        }
    }
}

#endif
