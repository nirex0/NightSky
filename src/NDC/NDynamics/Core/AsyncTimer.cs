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
using System;
using System.Windows.Threading;

namespace NDC.NDynamics.Core
{
    public class AsyncTimer
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int time;
        public event TimerEventHandler End;
        /// <summary>
        /// Initialize and start the Timer.
        /// </summary>
        /// <param name="time">Time in ms.</param>
        public AsyncTimer(int time)
        {
            this.time = time;
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, time);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            OnEnd();
        }
        virtual protected void OnEnd()
        {
            End?.Invoke(this, new TimerEventArgs(time));
        }
    }
}