using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Utility.Ini;


namespace Genie3D.Net
{
    public enum MenuType
    {
        Boolean,
        Float,
        String
    }

    class Setting
    {
        private LinkedList<Menu> _menuList;

        public Setting()
        {
            var FileParser = new FileIniDataParser();
            IniData data = FileParser.ReadAndParseIniFile("Config.ini", "Output", Encoding.ASCII);

            FileParser.WriteFile("Configuration.ini", "Output", data);

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

    class MenuEntryString : MenuEntry
    {
        private String _default;

        public MenuEntryString(String name, String def) : base(name, MenuType.String)
        {
            _default = def;
        }
    }
}