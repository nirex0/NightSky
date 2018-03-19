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

namespace NDC.NStyle.Controls
{
    public class NSRoundSwitch : ProgressBar
    {
        private NDC.NDynamics.Core.Lerp PRGCHANGE;          
        public bool Override { get; set; }

        /// <summary>
        /// Sets the delay of the color transition when a switch action takes place.
        /// </summary>
        public int colorTransitionSpeed { get; set; } = 200;

        private bool _isActive;

        /// <summary>
        /// Sets the value of the Switch Control.
        /// </summary>
        public bool isActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (_isActive)
                {
                     NDC.NStyle.Transition.Smooth.ProgressBar.Foreground(this, Color.FromArgb(0x0, 0x0, 0x0, 0x0), colorTransitionSpeed);
                    SetValue(0);
                }
                else
                {
                     NDC.NStyle.Transition.Smooth.ProgressBar.Foreground(this, Container.Colors.MAIN, colorTransitionSpeed);
                    SetValue(100);
                }
            }
        }
        public NSRoundSwitch()
        {
            MouseEnter += PGME;
            MouseLeave += PGML;
            PreviewMouseDown += RWMD;
            
            Value = 0;
            Height = 20;
            Width = 46;
            Style = (Style)FindResource("NS_ROUNDSWITCH");
        }
        private void RWMD(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_isActive)
            {
                 NDC.NStyle.Transition.Smooth.ProgressBar.Foreground(this, Color.FromArgb(0x0, 0x0, 0x0, 0x0), colorTransitionSpeed);
                SetValue(0);
            }
            else
            {
                 NDC.NStyle.Transition.Smooth.ProgressBar.Foreground(this, Container.Colors.MAIN, colorTransitionSpeed);
                SetValue(100);
            }

            _isActive = !_isActive;
        }

        private void PGME(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!Override)
            {
                 NDC.NStyle.Transition.Smooth.ProgressBar.BorderBrush(this, Container.Colors.GLOW, 250);
                 NDC.NStyle.Transition.Smooth.ProgressBar.Background(this, Container.Colors.BACK, 250);
            }
        }
        private void PGML(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!Override)
            {
                 NDC.NStyle.Transition.Smooth.ProgressBar.BorderBrush(this, Container.Colors.MAIN, 250);
                 NDC.NStyle.Transition.Smooth.ProgressBar.Background(this, Container.Colors.BACK, 250);
            }
        }
        /// <summary>
        /// Used to smoothly change the value of the ProgressBar.
        /// </summary>
        /// <param name="newValue">Value to set.</param>
        /// /// <param name="speed">Speed of value Transition.Smooth [0.01f, 0.05f].</param>
        public void SetValue(uint newValue, float speed = 0.025f)
        {
            if (speed < 0.01f) { speed = 0.01f; }
            if (speed > 0.05f) { speed = 0.05f; }
            if (newValue >= 100) { newValue = 100; }
            if (newValue <= 000) { newValue = 000; }
            if (PRGCHANGE != null) { PRGCHANGE.Pause(); Value = valueToSet; }

            valueToSet = newValue;
            PRGCHANGE = new NDC.NDynamics.Core.Lerp((int)Value, newValue, speed, 0.1f, 1);
            PRGCHANGE.LTick += PRGCHANGE_LerpTick;
            PRGCHANGE.LDone += PRGCHANGE_LerpDone;
            PRGCHANGE.Start();

        }
        private uint valueToSet;
        private void PRGCHANGE_LerpTick(object sender, NDC.NDynamics.Arguments.LerpArgs e)
        {
            Value = e.valueExact;
        }
        private void PRGCHANGE_LerpDone(object sender, NDC.NDynamics.Arguments.LerpArgs e)
        {
            Value = valueToSet;
        }
        public void Update()
        {
            if (!Override)
            {
                 NDC.NStyle.Transition.Smooth.ProgressBar.Foreground(this, Container.Colors.MAIN, 250);
                 NDC.NStyle.Transition.Smooth.ProgressBar.BorderBrush(this, Container.Colors.MAIN, 250);
                 NDC.NStyle.Transition.Smooth.ProgressBar.Background(this, Container.Colors.BACK, 250);
            }
        }
    }
}
