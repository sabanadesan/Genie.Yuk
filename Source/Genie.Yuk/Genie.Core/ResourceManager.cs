using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace Genie.Yuk
{

    public class ResourceNode
    {
        protected Guid _internal_GUID;
        protected int _version;
        protected DateTime _date;
        protected String _content;

        public ResourceNode()
        {
            _internal_GUID = Guid.NewGuid();
            _version = 1;
            _date = DateTime.Now;
        }

        public ResourceNode(Guid internal_GUID, int version, DateTime date, String content)
        {
            _internal_GUID = internal_GUID;
            _version = version;
            _date = date;
            _content = content;
        }

        public Guid Internal_GUID
        {
            get
            {
                return _internal_GUID;
            }
            set
            {
                _internal_GUID = value;
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

        public String Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }
    }

    public class ResourceList
    {
        private LinkedList<ResourceNode> revision;
        protected Guid _GUID;

        public ResourceList()
        {
            _GUID = Guid.NewGuid();
            revision = new LinkedList<ResourceNode>();
            revision.AddLast(new LinkedListNode<ResourceNode>(new ResourceNode()));
        }

        public ResourceList(Guid GUID, Guid internal_GUID, int version, DateTime date, String content)
        {
            _GUID = GUID;
            revision = new LinkedList<ResourceNode>();
            revision.AddLast(new LinkedListNode<ResourceNode>(new ResourceNode(internal_GUID, version, date, content)));
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
    }

    public class ShaderResourceNode : ResourceNode
    {

    }

    public class ShaderResource : ResourceList
    {

    }

    public static class ResourceManager
    {
        private static Dictionary<Guid, ResourceList> _registeredResources = new Dictionary<Guid, ResourceList>();

        public static void Register(ResourceList toRegister)
        {
            _registeredResources.Add(toRegister.GUID, toRegister);
        }

        public static ResourceList Resolve(Guid GUID)
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
