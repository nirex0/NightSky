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
using System.Windows.Controls;

namespace NDC.NUtils
{
    /// <summary>
    /// Don't use this directly.
    /// </summary>
    public delegate void UAction();

    /// <summary>
    /// Don't use this directly.
    /// </summary>
    public delegate void RAction<R>(ref R RET);

    /// <summary>
    /// Utility Action.
    /// </summary>
    public static class UtilityAction
    {
        private static MediaElement media = new MediaElement();
        public static void PlaySound(string path, UriKind uriKind = UriKind.Relative)
        {
            media.LoadedBehavior = MediaState.Manual;
            media.UnloadedBehavior = MediaState.Manual;
            media.Source = new Uri(path, uriKind);
            media.Volume = 1;
            media.Play();
        }
    }



}
