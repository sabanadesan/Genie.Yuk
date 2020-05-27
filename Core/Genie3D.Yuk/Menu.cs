using System;
using System.Collections.Generic;
using System.Text;

namespace Genie3D.Net
{
    class Menu
    {
        LinkedList<MenuItem> MenuList;

        public Menu()
        {
            MenuList = new LinkedList<MenuItem>();
        }
        
    }

    class MenuItem
    {
        LinkedList<MenuEntry> subMenuList;

        public MenuItem()
        {
            subMenuList = new LinkedList<MenuEntry>();
        }
        
    }

    class MenuEntry
    {

    }
}
