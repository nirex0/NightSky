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

namespace NDC.NDynamics.Window
{
    /// <summary>
    /// Main window transition class.
    /// </summary>
    public static class NWindow
    {
        public static class LERP
        {
            #region SWITCH
            private static Core.Lerp SWITCH_LerpFade = new Core.Lerp();
            private static Core.Lerp SWITCH_LerpShow = new Core.Lerp();
            private static System.Windows.Window _WFade;
            private static System.Windows.Window _WShow;
            private static bool __shouldClose = true;
            /// <summary>
            /// Make a smooth transition between two windows using Lerp.
            /// </summary>
            /// <param name="WFade">Window to fade.</param>
            /// <param name="WShow">Window to reveal.</param>
            /// <param name="Delay">Delay in ms.</param>
            /// <param name="shouldClose ">Close if true, Hide if false.</param>
            public static void Switch(System.Windows.Window WFade, System.Windows.Window WShow, int Delay = 1, bool shouldClose = true)
            {
                __shouldClose = shouldClose;

                SWITCH_LerpFade = new Core.Lerp(1, 0, 0.01f, 0.05f, 1);
                SWITCH_LerpShow = new Core.Lerp(0, 1, 0.01f, 0.05f, 1);

                SWITCH_LerpFade.delay = Delay;
                SWITCH_LerpShow.delay = Delay;

                _WFade = WFade;
                _WShow = WShow;

                _WShow.Hide();
                _WFade.Show();


                SWITCH_LerpShow.LStart += LerpShow_LerpStart;
                SWITCH_LerpShow.LTick += LerpShow_LerpTick;
                SWITCH_LerpShow.LDone += LerpShow_LerpDone;

                SWITCH_LerpFade.LStart += LerpFade_LerpStart;
                SWITCH_LerpFade.LTick += LerpFade_LerpTick;
                SWITCH_LerpFade.LDone += LerpFade_LerpDone;

                SWITCH_LerpFade.Start();
                SWITCH_LerpShow.Start();
            }
            private static void LerpShow_LerpStart(object sender, LerpArgs e)
            {
                _WShow.Opacity = 0;
                _WShow.Show();
            }
            private static void LerpShow_LerpTick(object sender, LerpArgs e)
            {
                _WShow.Opacity = e.valueExact;
            }
            private static void LerpShow_LerpDone(object sender, LerpArgs e)
            {
                _WShow.Opacity = 1;
            }
            private static void LerpFade_LerpStart(object sender, LerpArgs e)
            {
                _WFade.Opacity = 1;
            }
            private static void LerpFade_LerpTick(object sender, LerpArgs e)
            {
                _WFade.Opacity = e.valueExact;
            }
            private static void LerpFade_LerpDone(object sender, LerpArgs e)
            {
                if (__shouldClose) { _WFade.Close(); }
                else { _WFade.Opacity = 1; _WFade.Hide(); }
            }
            #endregion
            #region FADE/REVEAL SINGLE
            private static Core.Lerp SINGLE_LerpFade = new Core.Lerp();
            private static Core.Lerp SINGLE_LerpShow = new Core.Lerp();
            private static System.Windows.Window HWND;
            private static bool _shouldClose;
            /// <summary>
            /// Smoothly reveals a window.
            /// </summary>
            /// <param name="WReveal">Window to reveal.</param>
            /// <param name="delay">Delay in ms.</param>
            public static void Reveal(System.Windows.Window WReveal, int delay = 1)
            {
                SINGLE_LerpShow = new Core.Lerp(0, 1, 0.01f, 0.05f, 1);
                SINGLE_LerpShow.LStart += SINGLE_LerpShow_LerpStart;
                SINGLE_LerpShow.LTick += SINGLE_LerpShow_LerpTick;
                SINGLE_LerpShow.LDone += SINGLE_LerpShow_LerpDone;

                HWND = WReveal;
                HWND.Opacity = 0;
                HWND.Show();
                SINGLE_LerpShow.delay = delay;
                SINGLE_LerpShow.Start();
            }
            private static void SINGLE_LerpShow_LerpStart(object sender, LerpArgs e)
            {
                HWND.Opacity = 0;
            }
            private static void SINGLE_LerpShow_LerpTick(object sender, LerpArgs e)
            {
                HWND.Opacity = e.valueExact;
            }
            private static void SINGLE_LerpShow_LerpDone(object sender, LerpArgs e)
            {
                HWND.Opacity = 1;
            }
            /// <summary>
            /// Smoothly hides/closes a window.
            /// </summary>
            /// <param name="WHide">Window to Hide/Close.</param>
            /// <param name="delay">Delay in ms.</param>
            /// <param name="shouldClose">Close if true, Hide if false.</param>
            public static void Hide(System.Windows.Window WHide, int delay = 1, bool shouldClose = false)
            {
                _shouldClose = shouldClose;
                SINGLE_LerpFade = new Core.Lerp(1, 0, 0.01f, 0.05f, 1);
                SINGLE_LerpFade.LStart += SINGLE_LerpFade_LerpStart;
                SINGLE_LerpFade.LTick += SINGLE_LerpFade_LerpTick;
                SINGLE_LerpFade.LDone += SINGLE_LerpFade_LerpDone;

                HWND = WHide;
                HWND.Opacity = 1;
                HWND.Show();
                SINGLE_LerpFade.delay = delay;
                SINGLE_LerpFade.Start();
            }
            private static void SINGLE_LerpFade_LerpStart(object sender, LerpArgs e)
            {
                HWND.Opacity = 1;
            }
            private static void SINGLE_LerpFade_LerpTick(object sender, LerpArgs e)
            {
                HWND.Opacity = e.valueExact;
            }
            private static void SINGLE_LerpFade_LerpDone(object sender, LerpArgs e)
            {
                HWND.Opacity = 0;
                if (_shouldClose) { HWND.Close(); }
                else { HWND.Hide(); HWND.Opacity = 1; }
            }
            #endregion
        }
        public static class SmoothStep
        {
            #region SWITCH
            private static Core.SmoothStep SWITCH_StepFade = new Core.SmoothStep();
            private static Core.SmoothStep SWITCH_StepShow = new Core.SmoothStep();
            private static System.Windows.Window _WFade;
            private static System.Windows.Window _WShow;
            private static bool __shouldClose = true;

            /// <summary>
            /// Make a smooth transition between two windows using Smooth Step.
            /// </summary>
            /// <param name="WFade">Window to fade.</param>
            /// <param name="WShow">Window to reveal.</param>
            /// <param name="Delay">Delay in ms.</param>
            /// <param name="shouldHideAfterSwitch">Close if true, Hide if false.</param>
            public static void Switch(System.Windows.Window WFade, System.Windows.Window WShow, int Delay = 1, bool shouldClose = true)
            {
                __shouldClose = shouldClose;

                SWITCH_StepFade = new Core.SmoothStep(0, 1, 1.00f, -0.001f, 0.01f, 1);
                SWITCH_StepShow = new Core.SmoothStep(0, 1, 0.00f, +0.001f, 0.01f, 1);


                SWITCH_StepFade.delay = Delay;
                SWITCH_StepShow.delay = Delay;

                _WFade = WFade;
                _WShow = WShow;

                _WFade.Show();
                _WShow.Hide();

                SWITCH_StepShow.SmoothStepStart += SWITCH_StepShow_SmoothStepStart; ;
                SWITCH_StepShow.SmoothStepTick += SWITCH_StepShow_SmoothStepTick; ;
                SWITCH_StepShow.SmoothStepDone += SWITCH_StepShow_SmoothStepDone; ;

                SWITCH_StepFade.SmoothStepStart += SWITCH_StepFade_SmoothStepStart; ;
                SWITCH_StepFade.SmoothStepTick += SWITCH_StepFade_SmoothStepTick; ;
                SWITCH_StepFade.SmoothStepDone += SWITCH_StepFade_SmoothStepDone; ;

                SWITCH_StepFade.Start();
                SWITCH_StepShow.Start();
            }
            private static void SWITCH_StepShow_SmoothStepStart(object sender, Arguments.SmoothStepArgs e)
            {
                _WShow.Opacity = 0;
                _WShow.Show();
            }
            private static void SWITCH_StepShow_SmoothStepTick(object sender, Arguments.SmoothStepArgs e)
            {
                _WShow.Opacity = e.valueExact;
            }
            private static void SWITCH_StepShow_SmoothStepDone(object sender, Arguments.SmoothStepArgs e)
            {
                _WShow.Opacity = 1;
            }
            private static void SWITCH_StepFade_SmoothStepStart(object sender, Arguments.SmoothStepArgs e)
            {
                _WFade.Opacity = 1;
            }
            private static void SWITCH_StepFade_SmoothStepTick(object sender, Arguments.SmoothStepArgs e)
            {
                _WFade.Opacity = e.valueExact;
            }
            private static void SWITCH_StepFade_SmoothStepDone(object sender, Arguments.SmoothStepArgs e)
            {
                _WFade.Opacity = 0;
                if (__shouldClose) { _WFade.Close(); }
                else { _WFade.Hide(); _WFade.Opacity = 1; }
            }

            #endregion
            #region FADE/REVEAL SINGLE
            private static Core.SmoothStep SINGLE_StepFade = new Core.SmoothStep();
            private static Core.SmoothStep SINGLE_StepShow = new Core.SmoothStep();
            private static System.Windows.Window HWND;
            private static bool _shouldClose;
            /// <summary>
            /// Smoothly reveals a window.
            /// </summary>
            /// <param name="WReveal">Window to reveal.</param>
            /// <param name="delay">Delay in ms.</param>
            public static void Reveal(System.Windows.Window WReveal, int delay = 1)
            {
                SINGLE_StepShow = new Core.SmoothStep(0, 1, 0.01f, 0.05f, 1);
                SINGLE_StepShow.SmoothStepStart += SINGLE_StepShow_SmoothStepStart; ;
                SINGLE_StepShow.SmoothStepTick += SINGLE_StepShow_SmoothStepTick; ;
                SINGLE_StepShow.SmoothStepDone += SINGLE_StepShow_SmoothStepDone; ;

                HWND = WReveal;
                HWND.Opacity = 0;
                HWND.Show();
                SINGLE_StepShow.delay = delay;
                SINGLE_StepShow.Start();
            }
            private static void SINGLE_StepShow_SmoothStepStart(object sender, SmoothStepArgs e)
            {
                HWND.Opacity = 0;
            }
            private static void SINGLE_StepShow_SmoothStepTick(object sender, SmoothStepArgs e)
            {
                HWND.Opacity = e.valueExact;
            }
            private static void SINGLE_StepShow_SmoothStepDone(object sender, SmoothStepArgs e)
            {
                HWND.Opacity = 1;
            }
            /// <summary>
            /// Smoothly hides/closes a window.
            /// </summary>
            /// <param name="WHide">Window to Hide/Close.</param>
            /// <param name="delay">Delay in ms.</param>
            /// <param name="shouldClose">Close if true, Hide if false.</param>
            public static void Hide(System.Windows.Window WHide, int delay = 1, bool shouldClose = false)
            {
                _shouldClose = shouldClose;
                SINGLE_StepFade = new Core.SmoothStep(1, 0, 0.01f, 0.05f, 1);
                SINGLE_StepFade.SmoothStepStart += SINGLE_StepFade_SmoothStepStart; ;
                SINGLE_StepFade.SmoothStepTick += SINGLE_StepFade_SmoothStepTick; ;
                SINGLE_StepFade.SmoothStepDone += SINGLE_StepFade_SmoothStepDone; ;

                HWND = WHide;
                HWND.Opacity = 1;
                HWND.Show();
                SINGLE_StepFade.delay = delay;
                SINGLE_StepFade.Start();
            }
            private static void SINGLE_StepFade_SmoothStepStart(object sender, SmoothStepArgs e)
            {
                HWND.Opacity = 1;
            }
            private static void SINGLE_StepFade_SmoothStepTick(object sender, SmoothStepArgs e)
            {
                HWND.Opacity = e.valueExact;
            }
            private static void SINGLE_StepFade_SmoothStepDone(object sender, SmoothStepArgs e)
            {
                HWND.Opacity = 0;
                if (_shouldClose) { HWND.Close(); }
                else { HWND.Hide(); HWND.Opacity = 1; }
            }
            #endregion
        }
    }
}
