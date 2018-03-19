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
using System.ComponentModel;

namespace NDC.NDynamics.Core
{


    /// <summary>
    /// Main Linear interpolation class, used to make ease-out animations and movements.
    /// </summary>
    public class Lerp
    {
        #region Members
        private readonly BackgroundWorker lerpWorker = new BackgroundWorker();

        private float value;
        private bool isPaused;

        /// <summary>
        /// The First value.
        /// </summary>
        public float first { get; set; }
        /// <summary>
        /// The Second value.
        /// </summary>
        public float second { get; set; }
        /// <summary>
        /// The Alpha value. (Must be between 0 and 1)
        /// </summary>
        public float alpha { get; set; }
        /// <summary>
        /// The closeness of the two numbers for the lerp to end.
        /// </summary>
        public float closeness { get; set; }
        /// <summary>
        /// The Delay between each interpolation.
        /// </summary>
        public int delay { get; set; }
        /// <summary>
        /// Is called every time the interpolation ticks.
        /// </summary>
        public event LerpEventHandler LTick;
        /// <summary>
        /// Is called on the very last interpolation.
        /// </summary>
        public event LerpEventHandler LDone;
        /// <summary>
        /// Is called every time the interpolation is started/unpaused.
        /// </summary>
        public event LerpEventHandler LStart;
        /// <summary>
        /// Is called every time the interpolation is paused.
        /// </summary>
        public event LerpEventHandler LPause;
        #endregion
        #region Public Methods
        /// <summary>
        /// Start to Linearly interpolate between the two given values based on the alpha value.
        /// </summary>
        public Lerp()
        {
            first = 0;
            second = 100;
            alpha = 0.5f;
            closeness = 1;
            Initialize();
        }
        /// <summary>
        /// Start to Linearly interpolate between the two given values based on the alpha value.
        /// </summary>
        /// <param name="first">The First value.</param>
        /// <param name="second">The Second value</param>
        /// <param name="alpha">The Alpha value</param>
        /// <param name="min">The closeness of the two numbers for the lerp to end.</param>
        /// <param name="delay">The Delay between each interpolation.</param>
        public Lerp(float first, float second, float alpha, float min, int delay = 1)
        {
            this.first = first;
            this.second = second;
            this.alpha = alpha;
            this.closeness = min;
            this.delay = delay;
            Initialize();
        }
        /// <summary>
        /// Start the interpolation.
        /// </summary>
        public void Start()
        {
            isPaused = false;
            try
            {
                lerpWorker.RunWorkerAsync();
                OnLerpStart();
            }
            catch
            {

            }
        }
        /// <summary>
        /// Pause the interpolation.
        /// </summary>
        public void Pause()
        {
            OnLerpPause();
            isPaused = true;
        }
        #endregion
        #region Private Methods
        private void Initialize()
        {
            lerpWorker.DoWork += lerpWorker_DoWork;
            lerpWorker.RunWorkerCompleted += lerpWorker_RunWorkerCompleted;
            if (delay < 1) { delay = 1; }
        }
        private void lerpWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            first = first + (second - first) * alpha;
            value = first;
            System.Threading.Thread.Sleep(delay);
        }
        private void lerpWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            OnLerpTick();
            if (NDC.NUtils.NMathUtil.isNear(first, second, closeness))
            {
                OnLerpDone();
            }
            else
            {
                if (!isPaused)
                {
                    try
                    {
                        lerpWorker.RunWorkerAsync();
                    }
                    catch
                    {

                    }
                }
            }
        }
        #endregion
        #region Event Carriers
        protected virtual void OnLerpTick()
        {
            LTick?.Invoke(this, new LerpArgs(value));
        }
        protected virtual void OnLerpDone()
        {
            LDone?.Invoke(this, new LerpArgs(value));
        }
        protected virtual void OnLerpStart()
        {
            LStart?.Invoke(this, new LerpArgs(value));
        }
        protected virtual void OnLerpPause()
        {
            LPause?.Invoke(this, new LerpArgs(value));
        }
        #endregion
    }
}
