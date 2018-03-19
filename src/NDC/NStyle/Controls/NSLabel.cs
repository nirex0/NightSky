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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NDC.NStyle.Controls
{
    public class NSLabel : Label
    {
        public bool isPlaceHolder { get; set; } = false;
        public bool Override { get; set; } = false;
        public TextBox NTB_Reference { get; set; }
        public NSLabel()
        {
            MouseLeave += NLBL_ML;
            MouseEnter += NLBL_ME;
            MouseDown += NLBL_MD;

            BorderThickness = new Thickness(0);
            FontSize = 14;
            FontWeight = FontWeights.Bold;
            FontFamily = new FontFamily("Global User Interface");
            Foreground = new SolidColorBrush(Container.Colors.MAIN);
        }
        private void NLBL_ME(object sender, MouseEventArgs e)
        {
            if (!Override)
            {
                Transition.Smooth.Label.Foreground(this, Container.Colors.GLOW, 250);
            }
            if (isPlaceHolder)
            {
                Mouse.OverrideCursor = null;
                Cursor = Cursors.IBeam;
            }
        }
        private void NLBL_ML(object sender, MouseEventArgs e)
        {
            if (!Override)
            {
                Transition.Smooth.Label.Foreground(this, Container.Colors.MAIN, 250);
            }
            if (isPlaceHolder)
            {
                Mouse.OverrideCursor = null;
                Cursor = Cursors.Arrow;
            }
        }
        private void NLBL_MD(object sender, MouseButtonEventArgs e)
        {
            if (NTB_Reference != null)
            {
                NTB_Reference.Focusable = true;
                NTB_Reference.Focus();
            }
        }
        public void Update()
        {
            if (!Override)
            {
                 NDC.NStyle.Transition.Smooth.Label.Foreground(this, Container.Colors.MAIN, 250);
            }
        }
    }
}
