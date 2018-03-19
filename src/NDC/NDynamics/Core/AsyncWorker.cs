/**************************************************************************\
Copyright (c) 2017 Nirex.0@Gmail.Com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
\**************************************************************************/


using NDC.NDynamics.Arguments;
using NDC.NDynamics.Handlers;
using NDC.NUtils;
using System.ComponentModel;

namespace NDC.NDynamics.Core
{
    /// <summary>
    /// Class to call a function in a loop Based on a specific interval in an async state.
    /// </summary>
    public class AsyncWorker
    {
        #region Members
        /// <summary>
        /// Is called every time the worker calls the callee.
        /// </summary>
        public event AsyncWorkerEventHandler WorkerInterval;
        private BackgroundWorker asyncWorker = new BackgroundWorker();
        private int interval = 0;

        public long intervalCount = 0;
        private bool shouldRun = false;
        private bool isInit = true;
        private UAction actToCall;
        #endregion
        #region Public Methods
        /// <summary>
        /// Set up the async worker.
        /// </summary>
        /// <param name="Inteval">The number of miliseconds the async worker's thread sleeps before making a call the callee.</param>
        public AsyncWorker(int Inteval = 10)
        {
            interval = Inteval;
            asyncWorker.DoWork += AsyncWorker_DoWork;
            asyncWorker.RunWorkerCompleted += AsyncWorker_RunWorkerCompleted;
        }
        /// <summary>
        /// Start the async worker to call the callee.
        /// </summary>
        /// <param name="callee">function to call, callee must be of type void and take no argument.</param>
        /// <returns>Whether or not the operation has gone successful</returns>
        public bool RunAsyncWorker(UAction callee)
        {
            try
            {
                if (isInit)
                {
                    isInit = false;
                    shouldRun = true;
                    asyncWorker.RunWorkerAsync();
                }
                shouldRun = true;
                actToCall = callee;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Stop the async worker.
        /// </summary>
        /// <returns>Whether or not the operation has gone successful</returns>
        public bool StopAsyncWorker()
        {
            try
            {
                shouldRun = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsRunning()
        {
            return shouldRun;
        }
        #endregion
        #region Private Methods
        private void AsyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(interval);
        }
        private void AsyncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (shouldRun)
            {
                intervalCount++;
                OnWorkerInterval();
                actToCall?.Invoke();
                asyncWorker.RunWorkerAsync();
            }
        }
        #endregion
        #region Event Carriers
        protected virtual void OnWorkerInterval()
        {
            WorkerInterval?.Invoke(this, new AsyncWorkerArgs(intervalCount));
        }
        #endregion
    }
}


