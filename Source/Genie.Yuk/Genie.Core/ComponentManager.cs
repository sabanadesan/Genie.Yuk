using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Genie.Yuk
{
    public static class ComponentManager
    {
        private static Dictionary<Guid, Component> _registeredTypes = new Dictionary<Guid, Component>();

        public static void Register(Guid key, Component toRegister)
        {
            _registeredTypes.Add(key, toRegister);
        }

        public static Component Resolve(Guid key)
        {
            return _registeredTypes[key];
        }

        public static void Update()
        {
            foreach (KeyValuePair<Guid, Component> kvp in _registeredTypes)
            {
                Guid key = kvp.Key;
                Component component = kvp.Value;

                component.Update();
            }
        }
    }

    public abstract class Component
    {
        private Guid guid;

        public Component()
        {
            guid = Guid.NewGuid();
            ComponentManager.Register(guid, this);
        }

        public abstract void Update();
    };

    public class InputComponent : Component
    {
        public override void Update()
        {

        }

        ~InputComponent()  // finalizer
        {
            // cleanup statements...
        }
    }
}
