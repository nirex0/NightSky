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

namespace NDC.NStyle.Controls
{
    public class NSButton : Button
    {
        private NDC.NDynamics.Core.Lerp BRHOV;
        private NDC.NDynamics.Core.Lerp BRUNH;

        private bool isFocused;
        public bool Override { get; set; } = false;
        public NSButton()
        {
            MouseEnter += NBTN_ME;
            MouseLeave += NBTN_ML;
            GotFocus += NBTN_GETF;
            LostFocus += NBTN_LOSF;

            Height = 24;
            Width = 60;
            Content = "Button";
            Style = (Style)FindResource("NS_BUTTON");
            BorderThickness = new Thickness(0, 0, 0, 0);
            FontSize = 14;
            FontWeight = FontWeights.Bold;
            FontFamily = new FontFamily("Global User Interface");
            Foreground = new SolidColorBrush(Container.Colors.MAIN);
            BorderBrush = new SolidColorBrush(Container.Colors.MAIN);
            Background = new SolidColorBrush(Container.Colors.BACK);
        }
        private void NBTN_GETF(object sender, RoutedEventArgs e)
        {
            isFocused = true;
            BorderThickness = new Thickness(2, 0, 0, 0);
        }
        private void NBTN_LOSF(object sender, RoutedEventArgs e)
        {
            isFocused = false;
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
        private void NBTN_ME(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!Override)
            {
                BOR_HOVER();
                 NDC.NStyle.Transition.Smooth.Button.Foreground(this, Container.Colors.GLOW, 250);
                 NDC.NStyle.Transition.Smooth.Button.BorderBrush(this, Container.Colors.GLOW, 250);
                 NDC.NStyle.Transition.Smooth.Button.Background(this, Container.Colors.BACK, 250);
            }
        }
        private void NBTN_ML(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!Override)
            {
                BOR_UNHOVER();
                 NDC.NStyle.Transition.Smooth.Button.Foreground(this, Container.Colors.MAIN, 250);
                 NDC.NStyle.Transition.Smooth.Button.BorderBrush(this, Container.Colors.MAIN, 250);
                 NDC.NStyle.Transition.Smooth.Button.Background(this, Container.Colors.BACK, 250);
            }
        }
        public void Update()
        {
            if (!Override)
            {
                 NDC.NStyle.Transition.Smooth.Button.Foreground(this, Container.Colors.MAIN, 250);
                 NDC.NStyle.Transition.Smooth.Button.BorderBrush(this, Container.Colors.MAIN, 250);
                 NDC.NStyle.Transition.Smooth.Button.Background(this, Container.Colors.BACK, 250);
            }
        }
    }
}