using SharpVk.Spirv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private GameManager() {
            Startup();
        }

        private void Startup()
        {

            Service.Register<Setting>(new Setting());
            Setting setting = Service.Resolve<Setting>();

            CreateDefaultSettings(setting);

            if (IsOnScreen)
            {
                if(_backend == GraphicsBackend.Vulkan)
                {
                    Service.Register<GameGraphics>(new GameGraphics());
                    GameGraphics cls = Service.Resolve<GameGraphics>();
                }
                else if (_backend == GraphicsBackend.DirectX12)
                {
                    if (_swapChainPanel == null)
                        throw new Exception("Please initialize variable: " + "'_swapChainPanel'");

                    if (_width == null)
                        throw new Exception("Please initialize variable: " + "'_width'");

                    if (_height == null)
                        throw new Exception("Please initialize variable: " + "'_height'");

                    Service.Register<GameGraphics>(new GameGraphics(_swapChainPanel, (int)_width, (int)_height));
                    GameGraphics cls = Service.Resolve<GameGraphics>();
                }
            }
        }

        private void CreateDefaultSettings(Setting setting)
        {
            Menu menu = new Menu("Graphics");

            setting.Add(menu);

            MenuEntryBool entry = new MenuEntryBool("Fullscreen", false);
            menu.Add(entry);

            MenuEntryFloat entry1 = new MenuEntryFloat("Height", 100);
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
    }
}
