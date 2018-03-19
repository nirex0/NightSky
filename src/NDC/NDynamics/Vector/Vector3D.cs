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
    public class Vector3D
    {

        private float y;
        private float x;
        private float z;

        /// <summary>
        /// Creates An empty 3D Vector.
        /// </summary>
        public Vector3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
        /// <summary>
        /// Creates a vector.
        /// </summary>
        /// <param name="x">Vector X.</param>
        /// <param name="y">Vector Y.</param>
        /// <param name="z">Vector Z.</param>
        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
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
        /// <summary>
        /// Vector Z.
        /// </summary>
        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public Thickness ToThickness()
        {
            return new Thickness(x, y, z, 0);
        }

        #region Operator Overloading

        public static Vector3D operator +(Vector3D LHS, Vector3D RHS)
        {
            return new Vector3D(LHS.x + RHS.x, LHS.y + RHS.y, LHS.z + RHS.z);
        }
        public static Vector3D operator -(Vector3D LHS, Vector3D RHS)
        {
            return new Vector3D(LHS.x - RHS.x, LHS.y - RHS.y, LHS.z - RHS.z);
        }
        public static Vector3D operator *(Vector3D LHS, Vector3D RHS)
        {
            return new Vector3D(LHS.x * RHS.x, LHS.y * RHS.y, LHS.z * RHS.z);
        }
        public static Vector3D operator /(Vector3D LHS, Vector3D RHS)
        {
            return new Vector3D(LHS.x / RHS.x, LHS.y / RHS.y, LHS.z / RHS.z);
        }

        #endregion
    }
}