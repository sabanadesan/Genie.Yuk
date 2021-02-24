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
        protected Position position;

        public Component()
        {
            guid = Guid.NewGuid();
            position = new Position(0.0, 0.0, 0.0);

            ComponentManager.Register(guid, this);
            this.Start();
        }

        public abstract void Start();
        public abstract void Update();
    };
}
