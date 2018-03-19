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


using System.Windows.Media;

namespace NDC.NStyle.Container
{
    public static class Colors
    {
        public static Color MAIN = Color.FromRgb(0xFF, 0xFF, 0xFF);
        public static Color GLOW = Color.FromRgb(0xBB, 0xBB, 0xBB);
        public static Color DISABLED = Color.FromRgb(0x17, 0x17, 0x17);
        public static Color BACK = Color.FromRgb(0x00, 0x00, 0x00);
        public static Color ALT_BACK = Color.FromRgb(0x00, 0x00, 0x00);

        public static void Update(Color C1, Color C2, Color C3, Color C4, Color C5)
        {
            MAIN = C1;
            GLOW = C2;
            DISABLED = C3;
            BACK = C4;
            ALT_BACK = C5;
        }
        /// <summary>
        /// Red
        /// </summary>
        public static void Cinder()
        {
            Update(RED, RED_GLOW, RED_DISABLED, RED_DARK, RED_ALT_DARK);
        }
        /// <summary>
        /// Teal
        /// </summary>
        public static void Deep()
        {
            Update(TEAL, TEAL_GLOW, TEAL_DISABLED, TEAL_DARK, TEAL_ALT_DARK);
        }
        /// <summary>
        /// Yellow
        /// </summary>
        public static void Industrial()
        {
            Update(YELLOW, YELLOW_GLOW, YELLOW_DISABLED, YELLOW_DARK, YELLOW_ALT_DARK);
        }
        /// <summary>
        /// Green
        /// </summary>
        public static void Toxin()
        {
            Update(GREEN, GREEN_GLOW, GREEN_DISABLED, GREEN_DARK, GREEN_ALT_DARK);
        }
        /// <summary>
        /// Purple
        /// </summary>
        public static void Void()
        {
            Update(PURPLE, PURPLE_GLOW, PURPLE_DISABLED, PURPLE_DARK, PURPLE_ALT_DARK);
        }
        /// <summary>
        /// Blue
        /// </summary>
        public static void NewAge()
        {
            Update(BLUE, BLUE_GLOW, BLUE_DISABLED, BLUE_DARK, BLUE_ALT_DARK);
        }
        /// <summary>
        /// Light Green
        /// </summary>
        public static void Grass()
        {
            Update(LIGHT_GREEN, LIGHT_GREEN_GLOW, LIGHT_GREEN_DISABLED, LIGHT_GREEN_DARK, LIGHT_GREEN_ALT_DARK);
        }



        // NIGHTSKY SPECIFIC THEME
        public static void NightSky()
        {
            Update(NIGHTSKY_NORMAL, NIGHTSKY_GLOW, NIGHTSKY_DISABLED, NIGHTSKY_DARK, LIGHT_GREEN_ALT_DARK);
        }

        public static Color NIGHTSKY_GLOW = Color.FromRgb(0x8B, 0xCE, 0xF9);
        public static Color NIGHTSKY_NORMAL = Color.FromRgb(0x00, 0x9B, 0xFF);
        public static Color NIGHTSKY_DISABLED = Color.FromRgb(0x36, 0x36, 0x36);
        public static Color NIGHTSKY_DARK = Color.FromArgb(0, 0x02, 0x03, 0x05);
        public static Color NIGHTSKY_TRANSP = Color.FromArgb(128, 0x02, 0x03, 0x05);
        // NORMAL NDC COLORS

        public static Color BLUE_GLOW = Color.FromRgb(0xFF, 0x00, 0x00);
        public static Color PURPLE_GLOW = Color.FromRgb(0xFF,0x00,0xFF);
        public static Color GREEN_GLOW = Color.FromRgb(0x00, 0xFF, 0x00);
        public static Color TEAL_GLOW = Color.FromRgb(0x1C, 0xED, 0x93);
        public static Color RED_GLOW = Color.FromRgb(0xEF, 0x2E, 0x2E);
        public static Color YELLOW_GLOW = Color.FromRgb(0xFF, 0xFF, 0x00);
        public static Color LIGHT_GREEN_GLOW = Color.FromRgb(0x6E, 0xF7, 0x59);

        public static Color BLUE = Color.FromRgb(0x1D, 0x1D, 0xA2);
        public static Color PURPLE = Color.FromRgb(0xAE, 0x00, 0xAE);
        public static Color GREEN = Color.FromRgb(0x00, 0x9C, 0x00);
        public static Color TEAL = Color.FromRgb(0x17, 0xBF, 0x76);
        public static Color RED = Color.FromRgb(0xB5, 0x24, 0x24);
        public static Color YELLOW = Color.FromRgb(0xA8, 0xA8, 0x00);
        public static Color LIGHT_GREEN = Color.FromRgb(0x81, 0xB4, 0x79);
        
        public static Color BLUE_DISABLED = Color.FromRgb(0x41, 0x41, 0xA2);
        public static Color PURPLE_DISABLED = Color.FromRgb(0x7C, 0x4B, 0x7C);
        public static Color GREEN_DISABLED = Color.FromRgb(0x4B, 0x66, 0x3B);
        public static Color TEAL_DISABLED = Color.FromRgb(0x5D, 0x95, 0x7C);
        public static Color RED_DISABLED = Color.FromRgb(0x83, 0x3D, 0x3D);
        public static Color YELLOW_DISABLED = Color.FromRgb(0x6C, 0x6C, 0x41);
        public static Color LIGHT_GREEN_DISABLED = Color.FromRgb(0x6C,0x91,0x87);

        public static Color BLUE_DARK = Color.FromRgb(0x11, 0x15, 0x21);
        public static Color PURPLE_DARK = Color.FromRgb(0x21, 0x21, 0x21);
        public static Color GREEN_DARK = Color.FromRgb(0x21, 0x21, 0x21);
        public static Color TEAL_DARK = Color.FromRgb(0x12, 0x15, 0x1E);
        public static Color RED_DARK = Color.FromRgb(0x12, 0x12, 0x12);
        public static Color YELLOW_DARK = Color.FromRgb(0x21, 0x21, 0x21);
        public static Color LIGHT_GREEN_DARK = Color.FromRgb(0x3C, 0x3D, 0x5B);
        
        public static Color BLUE_ALT_DARK = Color.FromRgb(0x21, 0x23, 0x2F);
        public static Color PURPLE_ALT_DARK = Color.FromRgb(0x17, 0x17, 0x17);
        public static Color GREEN_ALT_DARK = Color.FromRgb(0x17, 0x17, 0x17);
        public static Color TEAL_ALT_DARK = Color.FromRgb(0x21, 0x23, 0x2F);
        public static Color RED_ALT_DARK = Color.FromRgb(0x00, 0x00, 0x00);
        public static Color YELLOW_ALT_DARK = Color.FromRgb(0x17, 0x17, 0x17);
        public static Color LIGHT_GREEN_ALT_DARK = Color.FromRgb(0x21, 0x23, 0x2F);

        public static Color _BLACK = Color.FromRgb(0x00, 0x00, 0x00);
        public static Color _WHITE = Color.FromRgb(0xFF, 0xFF, 0xFF);

        public static Color _ORANGE = Color.FromRgb(0xCA, 0x51, 0x00);
        public static Color _DIMBLUE = Color.FromRgb(0x0E, 0x63, 0x9C);
        public static Color _BLUE = Color.FromRgb(0x00, 0x7A, 0xCC);
    }
}
