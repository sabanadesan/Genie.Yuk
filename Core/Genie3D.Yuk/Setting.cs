using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Utility.Ini;
using Utility.Ini.Model;

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
            _menuList = new LinkedList<Menu>();
        }
        
        public void Add(Menu item)
        {
            _menuList.AddLast(item);
        }

        public void Clear()
        {
            _menuList.Clear();
        }

        public void Load(string fileName, string path)
        {
            var FileParser = new FileIniDataParser();
            IniData data = FileParser.ReadAndParseIniFile(fileName, path, Encoding.ASCII);

            bool bresult;
            float fresult;
            Menu menu;

            SectionCollection sections = data.Sections;
            foreach (Section section in sections)
            {
                menu = new Menu(section.Name);

                foreach (Property property in section.Properties)
                {
                    if (Boolean.TryParse(property.Value, out bresult))
                    {
                        MenuEntryBool entry = new MenuEntryBool(property.Key, bresult);
                        menu.Add(entry);
                    }
                    else if (float.TryParse(property.Value, out fresult))
                    {
                        MenuEntryFloat entry = new MenuEntryFloat(property.Key, fresult);
                        menu.Add(entry);
                    }
                    else
                    {
                        MenuEntryString entry = new MenuEntryString(property.Key, property.Value);
                        menu.Add(entry);
                    }
                }

                Add(menu);
            }
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