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

namespace NDC.NStyle.Controls
{
    public class NSTextBox : TextBox
    {
        public bool ShouldFlash { get; set; } = true;
        public bool Override { get; set; } = false;
        public Label NLBL_Reference { get; set; }

        private NDC.NDynamics.Core.Lerp BRHOV;
        private NDC.NDynamics.Core.Lerp BRUNH;

        private bool isFocused;
        public NSTextBox()
        {
            TextChanged += NTB_TXCH;
            KeyDown += NTB_KDWN;
            MouseEnter += NTB_MSE;
            MouseLeave += NTB_MSL;
            GotFocus += NTB_GETF;
            LostFocus += NTB_LOSF;

            Style = (Style)FindResource("NS_TEXTBOX");
            VerticalContentAlignment = VerticalAlignment.Center;
            BorderThickness = new Thickness(0, 0, 0, 0);
            FontSize = 14;
            FontWeight = FontWeights.Bold;
            FontFamily = new FontFamily("Global User Interface");
            Foreground = new SolidColorBrush(Container.Colors.MAIN);
            BorderBrush = new SolidColorBrush(Container.Colors.MAIN);
            Background = new SolidColorBrush(Container.Colors.BACK);
            SelectionBrush = new SolidColorBrush(Container.Colors.MAIN);
        }
        private void NTB_GETF(object sender, RoutedEventArgs e)
        {
            isFocused = true;
            BorderThickness = new Thickness(2, 0, 0, 0);
        }
        private void NTB_LOSF(object sender, RoutedEventArgs e)
        {
            isFocused = false;
            BOR_UNHOVER();
        }
        private void NTB_MSE(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BOR_HOVER();
        }
        private void NTB_MSL(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BOR_UNHOVER();
        }
        private void BOR_HOVER()
        {
            try
            {
                if (BRUNH != null)
                {
                    BRUNH.Pause();
                }
                if (isFocused == false)
                {
                    BRHOV = new NDC.NDynamics.Core.Lerp(0, 2, 0.01f, 0.1f, 1);
                    BRHOV.LTick += BRHOV_LerpTick;
                    BRHOV.LDone += BRHOV_LerpDone;
                    BRHOV.Start();
                }
            }
            catch { }
        }
        private void BRHOV_LerpTick(object sender, NDC.NDynamics.Arguments.LerpArgs e)
        {
            BorderThickness = new Thickness(e.valueExact, 0, BorderThickness.Right, 0);
        }
        private void BRHOV_LerpDone(object sender, NDC.NDynamics.Arguments.LerpArgs e)
        {
            BorderThickness = new Thickness(2, 0, BorderThickness.Right, 0);
        }
        private void BOR_UNHOVER()
        {
            try
            {
                if (BRHOV != null)
                {
                    BRHOV.Pause();
                }
                if (isFocused == false)
                {
                    BRUNH = new NDC.NDynamics.Core.Lerp(2, 0, 0.01f, 0.1f, 1);
                    BRUNH.LTick += BRUNH_LerpTick;
                    BRUNH.LDone += BRUNH_LerpDone;
                    BRUNH.Start();
                }
            }
            catch { }
        }
        private void BRUNH_LerpTick(object sender, NDC.NDynamics.Arguments.LerpArgs e)
        {
            BorderThickness = new Thickness(e.valueExact, 0, BorderThickness.Right, 0);
        }
        private void BRUNH_LerpDone(object sender, NDC.NDynamics.Arguments.LerpArgs e)
        {
            BorderThickness = new Thickness(0, 0, BorderThickness.Right, 0);
        }
        private void NTB_KDWN(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.LeftCtrl || e.Key == System.Windows.Input.Key.RightCtrl)
            {
                return;
            }
            if (BRUNH != null)
            {
                BRUNH.Pause();
            }
            if (BRHOV != null)
            {
                BRHOV.Pause();
            }
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
                 NDC.NStyle.Transition.Smooth.TextBox.Background(this, Container.Colors.NIGHTSKY_TRANSP, 250);
                 NDC.NStyle.Transition.Smooth.TextBox.SelectionBrush(this, Container.Colors.MAIN, 250);
            }
        }
    }
}
