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


using System;

/// <summary>
/// DON'T USE THIS NAMESPACE DIRECTLY!
/// </summary>
namespace NDC.NDynamics.Arguments
{
    /// <summary>
    /// Vector (2D, 3D and 4D) Lerp Arguments, Don't use this directly.
    /// </summary>
    public class VectorArgs : EventArgs
    {
        public VectorArgs()
        {
            X = 0;
            Y = 0;
            Z = 0;
            W = 0;
            roundedX = 0;
            roundedY = 0;
            roundedZ = 0;
            roundedW = 0;
        }
        public void Set(float x = 0, float y = 0, float z = 0, float w = 0)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            roundedX = (int)x;
            roundedY = (int)y;
            roundedZ = (int)z;
            roundedW = (int)w;
        }
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }
        public float W { get; private set; }
        public int roundedX { get; private set; }
        public int roundedY { get; private set; }
        public int roundedZ { get; private set; }
        public int roundedW { get; private set; }
    }
}
