#if TOOLS
using Godot;
using System;

[Tool]
public partial class Protonaut : EditorPlugin
{
    private MenuBar MenuBar { get; set; }
    private MenuButton fileMenu;
    private MenuButton editMenu;
    private VBoxContainer dock;

    public override void _EnterTree()
    {
        GD.Print("Entered.");
        // Create a custom dock.
        dock = new VBoxContainer();
        AddControlToDock(DockSlot.LeftUl, dock);

        // Create a MenuButton for the "File" menu.
        fileMenu = new MenuButton { Text = "File" };
        dock.AddChild(fileMenu);

        // Add items to the "File" menu.
        fileMenu.GetPopup().AddItem("Open");
        fileMenu.GetPopup().AddItem("Save");

        // Connect signals for the "File" menu items.
        fileMenu.GetPopup().Connect("id_pressed", new Callable(this, "OnFileMenuPressed"));

        // Create a MenuButton for the "Edit" menu.
        editMenu = new MenuButton { Text = "Edit" };
        dock.AddChild(editMenu);

        // Add items to the "Edit" menu.
        editMenu.GetPopup().AddItem("Cut");
        editMenu.GetPopup().AddItem("Copy");

        // Connect signals for the "Edit" menu items.
        editMenu.GetPopup().Connect("id_pressed", new Callable(this, "OnEditMenuPressed"));
    }

    //new Callable(this, "OnFileMenuPressed"));
    //new Callable(this, "OnEditMenuPressed"));

    public override void _ExitTree()
    {
        RemoveControlFromDocks(dock);
        dock.QueueFree();
    }

    public static void OnFileMenuPressed(int id)
    {
        switch (id)
        {
            case 0: GD.Print("Open selected"); break;
            case 1: GD.Print("Save selected"); break;
        }
    }

    private void OnEditMenuPressed(int id)
    {
        switch (id)
        {
            case 0: GD.Print("Cut selected"); break;
            case 1: GD.Print("Copy selected"); break;
        }
    }
}

#endif
