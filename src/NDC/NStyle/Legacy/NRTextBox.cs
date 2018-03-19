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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NDC.NStyle.Legacy
{
    public class NRTextBox : TextBox
    {
        public bool ShouldFlash { get; set; } = true;
        public bool Override { get; set; } = false;
        public Label NLBL_Reference { get; set; }
        public NRTextBox()
        {
            TextChanged += NTB_TXCH;

            Style = (Style)FindResource("NR_TEXTBOX");
            VerticalContentAlignment = VerticalAlignment.Center;
            BorderThickness = new Thickness(2, 0, 2, 0);
            FontSize = 14;
            FontWeight = FontWeights.Bold;
            FontFamily = new FontFamily("Global User Interface");
            Foreground = new SolidColorBrush(Container.Colors.MAIN);
            BorderBrush = new SolidColorBrush(Container.Colors.MAIN);
            Background = new SolidColorBrush(Container.Colors.BACK);
            SelectionBrush = new SolidColorBrush(Container.Colors.MAIN);
        }
        private void NTB_TXCH(object sender, TextChangedEventArgs e)
        {

            if (ShouldFlash)
            {
                ColorAnimation animation = new ColorAnimation(Container.Colors.MAIN, new Duration(TimeSpan.FromMilliseconds(1000)));
                Foreground = new SolidColorBrush(Container.Colors.GLOW);
                Foreground.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            }
            if (Text == string.Empty)
            {
                if (NLBL_Reference != null)
                {
                    NLBL_Reference.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (NLBL_Reference != null)
                {
                    NLBL_Reference.Visibility = Visibility.Hidden;
                }
            }
        }
        public void Update()
        {
            if (!Override)
            {
                 NDC.NStyle.Transition.Smooth.TextBox.Foreground(this, Container.Colors.MAIN, 250);
                 NDC.NStyle.Transition.Smooth.TextBox.BorderBrush(this, Container.Colors.MAIN, 250);
                 NDC.NStyle.Transition.Smooth.TextBox.Background(this, Container.Colors.BACK, 250);
                 NDC.NStyle.Transition.Smooth.TextBox.SelectionBrush(this, Container.Colors.MAIN, 250);
            }
        }
    }
}
