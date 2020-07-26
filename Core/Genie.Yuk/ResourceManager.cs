using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace Genie.Yuk
{

    public class ResourceNode
    {
        protected Guid _GUID;
        protected int _version;
        protected DateTime _date;

        public ResourceNode()
        {
            _GUID = Guid.NewGuid();
            _version = 1;
            _date = DateTime.Now;
        }

        public ResourceNode(Guid GUID, int version, DateTime date)
        {
            _GUID = GUID;
            _version = version;
            _date = date;
        }

        public Guid GUID
        {
            get
            {
                return _GUID;
            }
            set
            {
                _GUID = value;
            }
        }

        public int Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }
    }

    public class Resource
    {
        private LinkedList<ResourceNode> revision;

        public Resource()
        {
            revision = new LinkedList<ResourceNode>();
            revision.AddLast(new LinkedListNode<ResourceNode>(new ResourceNode()));
        }

        public Resource(Guid GUID, int version, DateTime date)
        {
            revision = new LinkedList<ResourceNode>();
            revision.AddLast(new LinkedListNode<ResourceNode>(new ResourceNode(GUID, version, date)));
        }

        public Guid GUID
        {
            get
            {
                return revision.First().GUID;
            }
        }
    }

    public class ShaderResourceNode : ResourceNode
    {

    }

    public class ShaderResource : Resource
    {

    }

    public static class ResourceManager
    {
        private static Dictionary<Guid, Resource> _registeredResources = new Dictionary<Guid, Resource>();

        public static void Register(Resource toRegister)
        {
            _registeredResources.Add(toRegister.GUID, toRegister);
        }

        public static Resource Resolve(Guid GUID)
        {
            return _registeredResources[GUID];
        }

        public static Boolean ContainsKey(Guid GUID)
        {
            return _registeredResources.ContainsKey(GUID);
        }

        public static void CreateResource()
        {

        }

        public static void DeleteResource()
        {

        }

        public static void MoveResource()
        {

        }
    }
}
