﻿using SharpVk.Spirv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utility.Yuk;

namespace Genie3D.Net
{
    internal class GameManager
    {
        private static GameManager _instance;
        private Boolean _is_screen;
        private static GraphicsBackend _backend;
        private static System.Object _swapChainPanel;
        private static int? _width;
        private static int? _height;
        private static String _path;

        private GameManager() {
            Startup();
        }

        private void Startup()
        {

            Service.Register<Setting>(new Setting());
            Setting setting = Service.Resolve<Setting>();

            setting.Load("Config.ini", _path);

            CreateDefaultSettings(setting);

            setting.Save("Configuration.ini", _path);

            if (IsOnScreen)
            {
                GameGraphics cls = null; 

                if (_backend == GraphicsBackend.Vulkan)
                {
                    Service.Register<GameGraphics>(new GameGraphics());
                }
                else if (_backend == GraphicsBackend.DirectX12)
                {
                    Utility.Yuk.Exception.CheckIsIntializedOrThrow(_swapChainPanel, _width, _height);

                    Service.Register<GameGraphics>(new GameGraphics(_swapChainPanel, (int)_width, (int)_height));
                }

                cls = Service.Resolve<GameGraphics>();

                Utility.Yuk.Exception.CheckIsIntializedOrThrow(cls);

                cls.Run();
            }
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

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public Boolean IsOnScreen
        {
            get
            {
                _is_screen = true;
                return _is_screen;
            }
        }

        public static GraphicsBackend Backend
        {
            get
            {
                return _backend;
            }
            set 
            {
                _backend = value; 
            }
        }

        public static int? Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }
        public static int? Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public static System.Object SwapChainPanel
        {
            get
            {
                return _swapChainPanel;
            }
            set
            {
                _swapChainPanel = value;
            }
        }

        public static string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
    }
}
