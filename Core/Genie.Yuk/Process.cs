using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using System.Threading;

namespace Genie.Yuk
{
    public static class ProcessServer
    {
        private static Dictionary<String, Process> _registeredProcesses = new Dictionary<String, Process>();

        public static void Register(String key, Process p)
        {
            _registeredProcesses.Add(key, p);
        }

        public static Process Resolve(String key)
        {
            return _registeredProcesses[key];
        }

        public static Process CreateProcess(String key)
        {
            Process p = new Process();
            Register(key, p);
            return p;
        }

        public static void Run(Process p)
        {
            Thread t1 = new Thread(() =>
            {
               p.Start();
            });

            t1.Start();
        }
    }

    public class Process
    {
        public delegate void MyFunction();
        private MyFunction m;

        public Process()
        { 

        }

        public void AddHandler(MyFunction f)
        {
            m = f;
        }

        public void Run()
        {
            ProcessServer.Run(this);
        }

        public void Start()
        {
            m();
        }
    }

    public class Task
    {

    }
}
