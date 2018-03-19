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

namespace NDC.NDynamics.Core
{
    public static class Limiter
    {
        #region Members


        /// <summary>
        /// Is raised when the input reaches/exceed the maximum given value.
        /// </summary>
        public static event LimiterEventHandler MaxReached;

        /// <summary>
        /// Is raised when the input reaches/exceed the minimum given value.
        /// </summary>
        public static event LimiterEventHandler MinReached;

        private static float curHolder;
        #endregion
        #region Public Methods
        /// <summary>
        /// Limit an input number while changing it's value.
        /// </summary>
        /// <param name="max">Maximum number the given input can reach.</param>
        /// <param name="min">Minimum number the given input can reach.</param>
        /// <param name="changeValue">The value by which the input changes.</param>
        /// <param name="input">Input value. (ref)</param>
        public static void Limit(float max, float min, float changeValue, ref float input)
        {
            curHolder = input;
            input += changeValue;
            if (curHolder >= max)
            {
                input = max;
                curHolder = input;
                OnMaxReached();
            }
            if (curHolder <= min)
            {
                input = min;
                curHolder = input;
                OnMinReached();
            }
        }
        /// <summary>
        /// Limit an input number while changing it's value.
        /// </summary>
        /// <param name="max">Maximum number the given input can reach.</param>
        /// <param name="min">Minimum number the given input can reach.</param>
        /// <param name="changeValue">The value by which the input changes.</param>
        /// <param name="input">Input value.</param>
        public static void Limit(float max, float min, float changeValue, float input)
        {
            curHolder = input;
            curHolder += changeValue;
            if (curHolder >= max)
            {
                curHolder = max;
                OnMaxReached();
            }
            if (curHolder <= min)
            {
                curHolder = min;
                OnMinReached();
            }
        }
        #endregion
        #region Event Carriers
        private static void OnMaxReached() { MaxReached?.Invoke(null, new LimiterArgs(curHolder)); }
        private static void OnMinReached() { MinReached?.Invoke(null, new LimiterArgs(curHolder)); }
        #endregion
    }
}