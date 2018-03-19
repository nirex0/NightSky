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
using System.Windows.Media;

namespace NDC.NStyle.Legacy
{
    public class NRButton : Button
    {
        private NDC.NDynamics.Core.Lerp LCD;
        private NDC.NDynamics.Core.Lerp LCU;

        public bool Override { get; set; } = false;
        public NRButton()
        {
            PreviewMouseDown += NBTN_MD;
            PreviewMouseUp += NBTN_MU;
            MouseEnter += NBTN_ME;
            MouseLeave += NBTN_ML;

            Height = 24;
            Width = 60;
            Content = "Button";
            Style = (Style)FindResource("NR_BUTTON");
            BorderThickness = new Thickness(2, 0, 2, 0);
            FontSize = 14;
            FontWeight = FontWeights.Bold;
            FontFamily = new FontFamily("Global User Interface");
            Foreground = new SolidColorBrush(Container.Colors.MAIN);
            BorderBrush = new SolidColorBrush(Container.Colors.MAIN);
            Background = new SolidColorBrush(Container.Colors.BACK);

        }
        private void NBTN_MD(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Override)
            {
                LCD = new NDC.NDynamics.Core.Lerp(2, 6, 0.01f, 0.1f, 1);
                LCD.LTick += LCD_TICK;
                LCD.LDone += LCD_DONE;
                LCD.Start();
                if (LCU != null)
                {
                    LCU.Pause();
                }
            }
        }
        private void LCD_TICK(object sender, NDC.NDynamics.Arguments.LerpArgs e)
        {
            if (!Override)
            {
                BorderThickness = new Thickness(e.valueExact, 0, e.valueExact, 0);
            }
        }
        private void LCD_DONE(object sender, NDC.NDynamics.Arguments.LerpArgs e)
        {
            if (!Override)
            {
                BorderThickness = new Thickness(6, 0, 6, 0);
            }
        }
        private void NBTN_MU(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Override)
            {
                LCU = new NDC.NDynamics.Core.Lerp(6, 2, 0.01f, 0.1f, 1);
                LCU.LTick += LCU_TICK;
                LCU.LDone += LCU_DONE;
                LCU.Start();
                if (LCD != null)
                {
                    LCD.Pause();
                }
            }
        }
        private void LCU_TICK(object sender, NDC.NDynamics.Arguments.LerpArgs e)
        {
            if (!Override)
            {
                BorderThickness = new Thickness(e.valueExact, 0, e.valueExact, 0);
            }
        }
        private void LCU_DONE(object sender, NDC.NDynamics.Arguments.LerpArgs e)
        {
            if (!Override)
            {
                BorderThickness = new Thickness(2, 0, 2, 0);
            }
        }
        private void NBTN_ME(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!Override)
            {
                 NDC.NStyle.Transition.Smooth.Button.Foreground(this, Container.Colors.GLOW, 250);
                 NDC.NStyle.Transition.Smooth.Button.BorderBrush(this, Container.Colors.GLOW, 250);
                 NDC.NStyle.Transition.Smooth.Button.Background(this, Container.Colors.BACK, 250);
            }
        }
        private void NBTN_ML(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!Override)
            {
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