using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LolServer
{
    public delegate void ExecutorDelegate();
    public class ExecutorPool
    {
        private static ExecutorPool instance;

        Mutex tex = new Mutex();

        public static ExecutorPool Instance
        {
            get
            {
                if (instance == null) 
                    instance = new ExecutorPool(); 
                return instance;
            }
        }

        public void execute(ExecutorDelegate d)
        {
            lock(this)
            {
                tex.WaitOne();
                d();
                tex.ReleaseMutex();
            }
        }
    }
}
