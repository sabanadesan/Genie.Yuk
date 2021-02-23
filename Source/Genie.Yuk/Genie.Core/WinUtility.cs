using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genie.Yuk
{
    public class WinUtility
    {
        public WinUtility()
        {

        }

        public void OnUiThread(Action action)
        {
            Task m_t = m_t = new Task(action);
            m_t.Start();
        }
    }
}
