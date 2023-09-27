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
        private List<PrimitiveBase> primitiveObjects = new List<PrimitiveBase>();

        private VBoxContainer dock;
        private HBoxContainer MenuBar { get; set; }
        private MenuButton fileMenu;
        private MenuButton actionsMenu;

        private const int FILE_OPEN = 0;
        private const int FILE_CLOSE = 1;

        public override void _EnterTree()
        {
            GD.Print("Entered _EnterTree.");
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

        private void CreateManyPoints(int numberOfPoints = 5000)
        { // start here: Create 5 points close to the origin.
            GD.Print("Entered CreateManyPointses.");
            double radius = 100.0;
            double thickness = 5.0;

            double angleRad = 0.0;
            double radiusInstance = 0.0;
            double height = 0.0;

            double x = 0.0; double y = 0.0; double z = 0.0;

            Random random = new Random();
            for (int i = 0; i < numberOfPoints; i++)
            {
                if(i % 100 == 0)
                    GD.Print($"i: {i}");
                angleRad = random.NextDouble() * 2 * Math.PI;
                radiusInstance = Math.Sqrt(random.NextDouble()) * radius;
                height = random.NextDouble() * 10.0;

                x = radiusInstance * Math.Cos(angleRad);
                z = radiusInstance * Math.Sin(angleRad);
                y = height;

                try
                {
                    //this.primitiveObjects.AddPrimitive(this.Scene,
                    //    new PointVisual(new Point3D(x, y, z),
                    //    new DiffuseMaterial(Brushes.Yellow),
                    //    new DiffuseMaterial(Brushes.Red)));

                    //lbl_speed.Content = $"{i++}";

                }
                catch (Exception e)
                {
                    //lbl_speed.Content = $"Overflow: {i} pts.";
                    GD.Print(e.ToString());
                    break;
                }
            }
            GD.Print("Exited CreateManyPoints.");

        }
    }
}

#endif
