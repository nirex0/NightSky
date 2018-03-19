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
    /// Main Smooth Step class, used to make ease-in ease-out animations and movements
    /// </summary>
    public class SmoothStep
    {
        #region Members
        private readonly BackgroundWorker SmoothStepWorker = new BackgroundWorker();


        /// <summary>
        /// Is called every time the smoothstep ticks.
        /// </summary>
        public event SmoothStepEventHandler SmoothStepTick;
        /// <summary>
        /// Is called on the very last step.
        /// </summary>
        public event SmoothStepEventHandler SmoothStepDone;
        /// <summary>
        /// Is called every time the smoothstep is started/unpaused.
        /// </summary>
        public event SmoothStepEventHandler SmoothStepStart;
        /// <summary>
        /// Is called every time the smoothstep is paused.
        /// </summary>
        public event SmoothStepEventHandler SmoothStepPause;
        /// <summary>
        /// The First Edge.
        /// </summary>
        public float minumum { get; set; }
        /// <summary>
        /// The Second Edge.
        /// </summary>
        public float maximum { get; set; }
        /// <summary>
        /// Initial Input.
        /// </summary>
        public float input { get; set; }
        /// <summary>
        /// Value by which the initial input will be changed over time.
        /// </summary>
        public float changeValue { get; set; }
        /// <summary>
        /// The closeness of the two numbers for the smooth step to end.
        /// </summary>
        public float closeness { get; set; }
        /// <summary>
        /// The Delay between each step.
        /// </summary>
        public int delay { get; set; }

        private float result;
        private float inputHolder;
        private bool isPaused;
        #endregion
        #region Public Methods
        /// <summary>
        /// Start to smoothly step between the two edges (From and Two) starting from Input and adding changed value to it over time.
        /// </summary>
        /// <param name="minimum">The first clamp edge.</param>
        /// <param name="maximum">The second clamp edge.</param>
        /// <param name="input">Initial Input.</param>
        /// <param name="changeValue">Value to change the initial input by over each iteration.</param>
        /// <param name="closeness">The closeness of the two numbers for the smooth step to end.</param>
        /// <param name="delay">The Delay between each step.</param>
        public SmoothStep(float minimum, float maximum, float input, float changeValue, float closeness, int delay = 1)
        {
            this.minumum = minimum;
            this.maximum = maximum;
            this.input = input;
            this.changeValue = changeValue;
            this.closeness = closeness;
            this.delay = delay;
            inputHolder = input;
            SmoothStepWorker.DoWork += SmoothStepWorker_DoWork;
            SmoothStepWorker.RunWorkerCompleted += SmoothStepWorker_RunWorkerCompleted;
        }
        /// <summary>
        /// Start to smoothly step between the two edges (From and Two) starting from Input and adding changed value to it over time.
        /// </summary>
        public SmoothStep()
        {
            minumum = 0.0f;
            maximum = 100.0f;
            input = 0.0f;
            changeValue = 1.0f;
            closeness = 1.0f;
            delay = 1;
            SmoothStepWorker.DoWork += SmoothStepWorker_DoWork;
            SmoothStepWorker.RunWorkerCompleted += SmoothStepWorker_RunWorkerCompleted;
        }
        /// <summary>
        /// Start the smooth step.
        /// </summary>
        public void Start()
        {
            isPaused = false;
            try
            {
                SmoothStepWorker.RunWorkerAsync();
                OnSmoothStepStart();
            }
            catch
            {

            }
        }
        /// <summary>
        /// Pause the current operation.
        /// </summary>
        public void Pause()
        {
            isPaused = true;
            OnSmoothStepPause();
        }
        #endregion
        #region Private Methods
        private void SmoothStepWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = C_Smoothstep(minumum, maximum, inputHolder);
            inputHolder = inputHolder + changeValue;
            System.Threading.Thread.Sleep(delay);
        }
        private void SmoothStepWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSmoothStepTick();
            if (NDC.NUtils.NMathUtil.isNear(inputHolder, maximum, closeness))
            {
                OnSmoothStepDone();
            }
            else
            {
                if (!isPaused)
                {
                    try
                    {
                        SmoothStepWorker.RunWorkerAsync();
                    }
                    catch
                    {

                    }
                }
            }
        }
        private float C_Smoothstep(float from, float to, float x)
        {
            x = C_Clamp((x - from) / (to - from), 0.0f, 1.0f);
            return x * x * (3 - 2 * x);
        }
        private float C_Clamp(float x, float lowerlimit, float upperlimit)
        {
            if (x < lowerlimit) x = lowerlimit;
            if (x > upperlimit) x = upperlimit;
            return x;
        }
        #endregion
        #region Event Carriers
        public virtual void OnSmoothStepTick()
        {
            SmoothStepTick?.Invoke(this, new SmoothStepArgs(result));
        }
        public virtual void OnSmoothStepDone()
        {
            SmoothStepDone?.Invoke(this, new SmoothStepArgs(result));
        }
        public virtual void OnSmoothStepStart()
        {
            SmoothStepStart?.Invoke(this, new SmoothStepArgs(result));
        }
        public virtual void OnSmoothStepPause()
        {
            SmoothStepPause?.Invoke(this, new SmoothStepArgs(result));
        }
        #endregion
    }
}
