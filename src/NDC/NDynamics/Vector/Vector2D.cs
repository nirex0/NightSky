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


using System.Windows;

namespace NDC.NDynamics.Vector
{
    public class Vector2D
    {

        private float y;
        private float x;

        /// <summary>
        /// Creates An empty 2D Vector.
        /// </summary>
        public Vector2D()
        {
            X = 0;
            Y = 0;
        }
        /// <summary>
        /// Creates a vector.
        /// </summary>
        /// <param name="x">Vector X.</param>
        /// <param name="y">Vector Y.</param>
        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Vector Y.
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Vector X.
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public Thickness ToThickness()
        {
            return new Thickness(x, y, 0, 0);
        }

        #region Operator Overloading

        public static Vector2D operator +(Vector2D LHS, Vector2D RHS)
        {
            return new Vector2D(LHS.x + RHS.x, LHS.y + RHS.y);
        }
        public static Vector2D operator -(Vector2D LHS, Vector2D RHS)
        {
            return new Vector2D(LHS.x - RHS.x, LHS.y - RHS.y);
        }
        public static Vector2D operator *(Vector2D LHS, Vector2D RHS)
        {
            return new Vector2D(LHS.x * RHS.x, LHS.y * RHS.y);
        }
        public static Vector2D operator /(Vector2D LHS, Vector2D RHS)
        {
            return new Vector2D(LHS.x / RHS.x, LHS.y / RHS.y);
        }

        #endregion
    }
}
