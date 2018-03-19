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

namespace NDC.NDynamics.Handlers
{
    /// <summary>
    /// Lerp main event handler.
    /// </summary>
    /// <param name="sender">The Sender Object.</param>
    /// <param name="e">An instance of the LerpArgs Class.</param>
    public delegate void LerpEventHandler(object sender, LerpArgs e);
    /// <summary>
    /// Lerp main event handler.
    /// </summary>
    /// <param name="sender">The Sender Object.</param>
    /// <param name="e">An instance of the VectorArgs Class.</param>
    public delegate void Lerp2DEventHandler(object sender, VectorArgs e);
    /// <summary>
    /// Lerp main event handler.
    /// </summary>
    /// <param name="sender">The Sender Object.</param>
    /// <param name="e">An instance of the VectorArgs Class.</param>
    public delegate void Lerp3DEventHandler(object sender, VectorArgs e);
    /// <summary>
    /// Lerp main event handler.
    /// </summary>
    /// <param name="sender">The Sender Object.</param>
    /// <param name="e">An instance of the VectorArgs Class.</param>
    public delegate void Lerp4DEventHandler(object sender, VectorArgs e);
}
