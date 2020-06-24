using SharpVk.Interop.Khronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Utility.Ini;
using Utility.Ini.Model;

namespace Genie3D.Yuk
{
    public enum MenuType
    {
        Boolean,
        Number,
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

        public void Save(string fileName, string path)
        {
            IniData data = new IniData();

            Section section;
            Property property;

            foreach (Menu menu in _menuList)
            {
                section = new Section(menu.Name);

                foreach(MenuEntry entry in menu.List)
                {
                    if (entry is MenuEntryNumber)
                    {
                        MenuEntryNumber fentry = (MenuEntryNumber) entry;
                        property = new Property(fentry.Name, fentry.Value.ToString());
                        section.Properties.Add(property);
                    }
                    else if (entry is MenuEntryBool)
                    {
                        MenuEntryBool bentry = (MenuEntryBool) entry;
                        property = new Property(bentry.Name, bentry.Value.ToString());
                        section.Properties.Add(property);
                    }
                    else if (entry is MenuEntryString)
                    {
                        MenuEntryString sentry = (MenuEntryString) entry;
                        property = new Property(sentry.Name, sentry.Value);
                        section.Properties.Add(property);
                    }
                }

                data.Sections.Add(section);
            }

            var FileParser = new FileIniDataParser();
            FileParser.WriteFile(fileName, path, data);
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
                        MenuEntryNumber entry = new MenuEntryNumber(property.Key, fresult);
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

        public LinkedList<MenuEntry> List
        {
            get
            {
                return _entryList;
            }
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

    class MenuEntryNumber : MenuEntry
    {
        private double _default;
        private double _min;
        private double _max;
        private double _step;

        public MenuEntryNumber(String name, double def, double min = 0, double max = 1, double step = 0.1) : base(name, MenuType.Number)
        {
            _default = def;
            _min = min;
            _max = max;
            _step = step;
        }

        public double Value
        {
            get
            {
                return _default;
            }
            set
            {
                _default = value;
            }
        }

        public double Min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
            }
        }

        public double Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
            }
        }

        public double Step
        {
            get
            {
                return _step;
            }
            set
            {
                _step = value;
            }
        }

    }

    class MenuEntryBool : MenuEntry
    {
        private Boolean _default;
        
        public MenuEntryBool(String name, Boolean def) : base(name, MenuType.Boolean)
        {
            _default = def;
        }

        public Boolean Value
        {
            get
            {
                return _default;
            }
            set
            {
                _default = value;
            }
        }
    }

    class MenuEntryString : MenuEntry
    {
        private String _default;

        public MenuEntryString(String name, String def) : base(name, MenuType.String)
        {
            _default = def;
        }

        public String Value
        {
            get
            {
                return _default;
            }
            set
            {
                _default = value;
            }
        }
    }
}