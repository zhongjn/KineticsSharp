using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KineticsSharp.Threading
{
    public class Legion : Legion<object>
    {
        public Legion(int workersCount, Action action) : base(workersCount, (obj) =>
        {
            action.Invoke();
        })
        {

        }
        public void Do()
        {
            base.Do(null);
        }
        private new void Do(object[] input) { }

    }
    public class Legion<TParam>
    {
        private Worker[] workers;
        public Legion(int workersCount, Action<TParam> action)
        {
            InitializeWorkers(workersCount, action);
        }
        private void InitializeWorkers(int workersCount, Action<TParam> action)
        {
            workers = new Worker[workersCount];
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = new Worker(action);
            }
        }
        public void Do(TParam[] input)
        {
            if (input == null)
            {
                input = new TParam[workers.Length];
            }
            if (input.Length != workers.Length)
            {
                throw new ArgumentException("incorrect size of input!");
            }


            for (int i = 0; i < workers.Length; i++)
            {
                workers[i].Start(input[i]);
            }
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i].Finish();
            }
        }


        private class Worker : IDisposable
        {
            private Thread thread;
            private bool stopped = false;
            private WorkInfo info;
            public Worker(Action<TParam> action)
            {

                thread = new Thread((obj) =>
                {
                    var info = (WorkInfo)obj;
                    while (!stopped)
                    {
                        info.Start.WaitOne();
                        action(info.Input);
                        info.Finish.Set();
                    }
                });
                info = new WorkInfo();
                thread.Start(info);
            }
            public void Start(TParam input)
            {
                info.Input = input;
                info.Start.Set();
            }
            public void Finish()
            {
                info.Finish.WaitOne();
            }


            #region IDisposable Support
            private bool disposedValue = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        stopped = true;
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                    // TODO: set large fields to null.

                    disposedValue = true;
                }
            }

            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            // ~Worker() {
            //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            //   Dispose(false);
            // }

            // This code added to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(true);
                // TODO: uncomment the following line if the finalizer is overridden above.
                // GC.SuppressFinalize(this);
            }
            #endregion
        }

        private class WorkInfo
        {
            public AutoResetEvent Start = new AutoResetEvent(false);
            public AutoResetEvent Finish = new AutoResetEvent(false);
            public TParam Input;
        }
    }


}
