using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Genie3D.Net
{
    public enum MenuType
    {
        Boolean,
        Float
    }

    class Setting
    {
        private LinkedList<Menu> _menuList;

        public Setting()
        {
            _menuList = new LinkedList<Menu>();
        }
        
        public void Add(Menu item)
        {
            _menuList.AddLast(item);
        }
    }

    class Menu
    {
        private LinkedList<MenuEntry> _entryList;
        private String _name;

        public Menu(String name)
        {
            _entryList = new LinkedList<MenuEntry>();
            _name = name;
        }

        public void Add(MenuEntry entry)
        {
            _entryList.AddLast(entry);
        }

        public MenuEntry Get(int index)
        {
            return _entryList.ElementAt<MenuEntry>(index);
        }
    }

    abstract class MenuEntry
    {
        private MenuType _type;
        private String _name;

        public MenuEntry(String name, MenuType type)
        {
            _type = type;
            _name = name;
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }

    class MenuEntryFloat : MenuEntry
    {
        private float _default;

        public MenuEntryFloat(String name, float def) : base(name, MenuType.Float)
        {
            _default = def;
        }
    }

    class MenuEntryBool : MenuEntry
    {
        private Boolean _default;
        
        public MenuEntryBool(String name, Boolean def) : base(name, MenuType.Boolean)
        {
            _default = def;
        }
    }
}