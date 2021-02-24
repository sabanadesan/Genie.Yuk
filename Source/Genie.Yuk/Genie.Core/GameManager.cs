using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Utility.Yuk;
using System.Threading;

namespace Genie.Yuk
{
    public class GameManager
    {
        private String _path;
        private Setting _setting;

        public GameManager(String Path) {
            _path = Path;
            _setting = new Setting();
        }

        public void Startup()
        {
            _setting.Load("Config.ini", _path);

            CreateDefaultSettings(_setting);

            _setting.Save("Configuration.ini", _path);
        }

        private void CreateDefaultSettings(Setting setting)
        {
            Menu menu = new Menu("Graphics");

            setting.Add(menu);

            MenuEntryBool entry = new MenuEntryBool("Fullscreen", false);
            menu.Add(entry);

            MenuEntryNumber entry1 = new MenuEntryNumber("Height", 10.00);
            menu.Add(entry1);
        }
    }
}
