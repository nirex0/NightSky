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

namespace NDC.NDynamics.Vector
{
    /// <summary>
    /// 2D Smooth step interpolation class.
    /// </summary>
    class Step2D
    {
        #region Events
        /// <summary>
        /// Is called every time the interpolation ticks.
        /// </summary>
        public event Step2DEventHandler VTick;
        /// <summary>
        /// Is called when time the interpolation ticks for the last time.
        /// </summary>
        public event Step2DEventHandler VDone;
        #endregion
        #region Steps
        private Core.SmoothStep step0 = new Core.SmoothStep();
        private Core.SmoothStep step1 = new Core.SmoothStep();
        #endregion
        #region Variables
        private VectorArgs vArgs = new VectorArgs();
        private int condition = 0;
        #endregion
        #region Constructor 
        /// <summary>
        /// Smooth step between two 2D Vectors.
        /// </summary>
        /// <param name="from">First Vector</param>
        /// <param name="to">Second Vector</param>
        /// <param name="initValue">The initial step value.</param>
        /// <param name="changeValue">Value to change the vector over each interpolation.</param>
        /// <param name="closeness">Minimum closeness of the two vectors.</param>
        /// <param name="delay">Delay between each tick.</param>
        public Step2D(Vector2D from, Vector2D to, float initValue, float changeValue, float closeness, int delay)
        {
            step0.minumum = from.X;
            step0.maximum = to.X;
            step0.input = initValue;
            step0.changeValue = changeValue;
            step0.closeness = closeness;
            step0.delay = delay;

            step1.minumum = from.Y;
            step1.maximum = to.Y;
            step1.input = initValue;
            step1.changeValue = changeValue;
            step1.closeness = closeness;
            step1.delay = delay;

            step0.Start();
            step1.Start();

            step0.SmoothStepTick += Step0_SmoothStepTick;
            step1.SmoothStepTick += Step1_SmoothStepTick;

            step0.SmoothStepDone += Step0_SmoothStepDone;
            step1.SmoothStepDone += Step1_SmoothStepDone;

        }
        #endregion
        #region Main Lerp Event Calls
        private void Step0_SmoothStepTick(object sender, SmoothStepArgs e)
        {
            vArgs.Set(e.valueExact, vArgs.Y, vArgs.Z, vArgs.W);
            OnVTick();
        }
        private void Step1_SmoothStepTick(object sender, SmoothStepArgs e)
        {
            vArgs.Set(vArgs.X, e.valueExact, vArgs.Z, vArgs.W);
            OnVTick();
        }
        private void Step0_SmoothStepDone(object sender, SmoothStepArgs e)
        {
            condition++;
            OnVDone();
        }
        private void Step1_SmoothStepDone(object sender, SmoothStepArgs e)
        {
            condition++;
            OnVDone();
        }
        #endregion
        #region Event Carriers
        protected virtual void OnVTick()
        {
            VTick?.Invoke(this, vArgs);
        }
        protected virtual void OnVDone()
        {
            if (VDone != null && condition == 2)
            {
                VDone(this, vArgs);
            }
        }
        #endregion
    }
}
