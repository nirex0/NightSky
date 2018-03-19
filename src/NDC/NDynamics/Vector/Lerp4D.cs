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
    /// 4D Linear interpolation class.
    /// </summary>
    public class Lerp4D
    {
        #region Events
        /// <summary>
        /// Is called every time the interpolation ticks.
        /// </summary>
        public event Lerp4DEventHandler VTick;
        /// <summary>
        /// Is called when time the interpolation ticks for the last time.
        /// </summary>
        public event Lerp4DEventHandler VDone;
        #endregion
        #region Lerps
        private Core.Lerp lerp0 = new Core.Lerp();
        private Core.Lerp lerp1 = new Core.Lerp();
        private Core.Lerp lerp2 = new Core.Lerp();
        private Core.Lerp lerp3 = new Core.Lerp();
        #endregion
        #region Variables
        private VectorArgs vArgs = new VectorArgs();
        private int condition = 0;
        #endregion
        #region Constructor
        /// <summary>
        /// Lerp between two 4D Vectors.
        /// </summary>
        /// <param name="from">First Vector.</param>
        /// <param name="to">Second Vector.</param>
        /// <param name="alpha">Alpha value.</param>
        /// <param name="delay">Delay between each tick.</param>
        public Lerp4D(Vector4D from, Vector4D to, float alpha, int delay)
        {
            lerp0.first = from.X;
            lerp0.second = to.X;
            lerp0.delay = delay;
            lerp0.alpha = alpha;

            lerp1.first = from.Y;
            lerp1.second = to.Y;
            lerp1.delay = delay;
            lerp1.alpha = alpha;


            lerp2.first = from.Z;
            lerp2.second = to.Z;
            lerp2.delay = delay;
            lerp2.alpha = alpha;

            lerp3.first = from.W;
            lerp3.second = to.W;
            lerp3.delay = delay;
            lerp3.alpha = alpha;

            lerp0.Start();
            lerp1.Start();
            lerp2.Start();
            lerp3.Start();

            lerp0.LTick += lerp0_LerpTick;
            lerp1.LTick += lerp1_LerpTick;
            lerp2.LTick += lerp2_LerpTick;
            lerp3.LTick += lerp3_LerpTick;

            lerp0.LDone += lerp0_LerpDone;
            lerp1.LDone += lerp1_LerpDone;
            lerp2.LDone += lerp2_LerpDone;
            lerp3.LDone += lerp3_LerpDone;
        }
        #endregion
        #region Main Lerp Event Calls
        private void lerp0_LerpTick(object sender, LerpArgs e)
        {
            vArgs.Set(e.valueExact, vArgs.Y, vArgs.Z, vArgs.W);
            OnVTick();
        }
        private void lerp1_LerpTick(object sender, LerpArgs e)
        {
            vArgs.Set(vArgs.X, e.valueExact, vArgs.Z, vArgs.W);
            OnVTick();
        }
        private void lerp2_LerpTick(object sender, LerpArgs e)
        {
            vArgs.Set(vArgs.X, vArgs.Y, e.valueExact, vArgs.W);
            OnVTick();
        }
        private void lerp3_LerpTick(object sender, LerpArgs e)
        {
            vArgs.Set(vArgs.X, vArgs.Y, vArgs.Z, e.valueExact);
            OnVTick();
        }
        private void lerp0_LerpDone(object sender, LerpArgs e)
        {
            condition++;
            OnVDone();
        }
        private void lerp1_LerpDone(object sender, LerpArgs e)
        {
            condition++;
            OnVDone();
        }
        private void lerp2_LerpDone(object sender, LerpArgs e)
        {
            condition++;
            OnVDone();
        }
        private void lerp3_LerpDone(object sender, LerpArgs e)
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
            if (VDone != null && condition == 4)
            {
                VDone(this, vArgs);
            }
        }
        #endregion
    }
}
