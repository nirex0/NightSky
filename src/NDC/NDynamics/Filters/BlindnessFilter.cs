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
using System.Windows.Media;

namespace NDC.NDynamics.Filters
{
    public static class BlindnessFilter
    {
        public static Color Filter(Color intake, BlindnessType blindnessType)
        {
            Color RETC = new Color();
            switch (blindnessType)
            {
                case BlindnessType.Normal:
                    RETC = intake;
                    break;
                case BlindnessType.Protanopia:
                    RETC = Protanopia(intake);
                    break;
                case BlindnessType.Protanomaly:
                    RETC = Protanomaly(intake);
                    break;
                case BlindnessType.Deuteranopia:
                    RETC = Deuteranopia(intake);
                    break;
                case BlindnessType.Deuteranomaly:
                    RETC = Deuteranomaly(intake);
                    break;
                case BlindnessType.Tritanopia:
                    RETC = Tritanopia(intake);
                    break;
                case BlindnessType.Tritanomaly:
                    RETC = Tritanomaly(intake);
                    break;
                case BlindnessType.Achromatopsia:
                    RETC = Achromatopsia(intake);
                    break;
                case BlindnessType.Achromatomaly:
                    RETC = Achromatomaly(intake);
                    break;
                default:
                    RETC = intake;
                    break;
            }
            return RETC;
        }
        private static Color Protanopia(Color intake)
        {
            double R0 = (intake.R / 100 * 56.667f);
            double R1 = (intake.G / 100 * 43.333f);
            double R2 = (intake.B / 100 * 00.000f);

            double G0 = (intake.R / 100 * 55.833f);
            double G1 = (intake.G / 100 * 44.167f);
            double G2 = (intake.B / 100 * 00.000f);

            double B0 = (intake.R / 100 * 00.000f);
            double B1 = (intake.G / 100 * 24.167f);
            double B2 = (intake.B / 100 * 75.833f);

            Color RETC = new Color();
            RETC.R = Convert.ToByte(R0 + R1 + R2);
            RETC.G = Convert.ToByte(G0 + G1 + G2);
            RETC.B = Convert.ToByte(B0 + B1 + B2);
            return RETC;
        }
        private static Color Protanomaly(Color intake)
        {
            double R0 = (intake.R / 100 * 81.667f);
            double R1 = (intake.G / 100 * 18.333f);
            double R2 = (intake.B / 100 * 00.000f);

            double G0 = (intake.R / 100 * 33.333f);
            double G1 = (intake.G / 100 * 66.667f);
            double G2 = (intake.B / 100 * 00.000f);

            double B0 = (intake.R / 100 * 00.000f);
            double B1 = (intake.G / 100 * 12.500f);
            double B2 = (intake.B / 100 * 87.500f);

            Color RETC = new Color();
            RETC.R = Convert.ToByte(R0 + R1 + R2);
            RETC.G = Convert.ToByte(G0 + G1 + G2);
            RETC.B = Convert.ToByte(B0 + B1 + B2);
            return RETC;
        }
        private static Color Deuteranopia(Color intake)
        {
            double R0 = (intake.R / 100 * 62.500f);
            double R1 = (intake.G / 100 * 37.500f);
            double R2 = (intake.B / 100 * 00.000f);

            double G0 = (intake.R / 100 * 70.000f);
            double G1 = (intake.G / 100 * 30.000f);
            double G2 = (intake.B / 100 * 00.000f);

            double B0 = (intake.R / 100 * 00.000f);
            double B1 = (intake.G / 100 * 30.000f);
            double B2 = (intake.B / 100 * 70.000f);

            Color RETC = new Color();
            RETC.R = Convert.ToByte(R0 + R1 + R2);
            RETC.G = Convert.ToByte(G0 + G1 + G2);
            RETC.B = Convert.ToByte(B0 + B1 + B2);
            return RETC;
        }
        private static Color Deuteranomaly(Color intake)
        {
            double R0 = (intake.R / 100 * 80.000f);
            double R1 = (intake.G / 100 * 20.000f);
            double R2 = (intake.B / 100 * 00.000f);

            double G0 = (intake.R / 100 * 70.000f);
            double G1 = (intake.G / 100 * 30.000f);
            double G2 = (intake.B / 100 * 00.000f);

            double B0 = (intake.R / 100 * 00.000f);
            double B1 = (intake.G / 100 * 30.000f);
            double B2 = (intake.B / 100 * 70.000f);

            Color RETC = new Color();
            RETC.R = Convert.ToByte(R0 + R1 + R2);
            RETC.G = Convert.ToByte(G0 + G1 + G2);
            RETC.B = Convert.ToByte(B0 + B1 + B2);
            return RETC;
        }
        private static Color Tritanopia(Color intake)
        {
            double R0 = (intake.R / 100 * 95.000f);
            double R1 = (intake.G / 100 * 05.000f);
            double R2 = (intake.B / 100 * 00.000f);

            double G0 = (intake.R / 100 * 00.000f);
            double G1 = (intake.G / 100 * 43.333f);
            double G2 = (intake.B / 100 * 56.667f);

            double B0 = (intake.R / 100 * 00.000f);
            double B1 = (intake.G / 100 * 47.500f);
            double B2 = (intake.B / 100 * 52.500f);

            Color RETC = new Color();
            RETC.R = Convert.ToByte(R0 + R1 + R2);
            RETC.G = Convert.ToByte(G0 + G1 + G2);
            RETC.B = Convert.ToByte(B0 + B1 + B2);
            return RETC;
        }
        private static Color Tritanomaly(Color intake)
        {
            double R0 = (intake.R / 100 * 96.667f);
            double R1 = (intake.G / 100 * 03.333f);
            double R2 = (intake.B / 100 * 00.000f);

            double G0 = (intake.R / 100 * 00.000f);
            double G1 = (intake.G / 100 * 74.444f);
            double G2 = (intake.B / 100 * 26.677f);

            double B0 = (intake.R / 100 * 00.000f);
            double B1 = (intake.G / 100 * 18.333f);
            double B2 = (intake.B / 100 * 81.667f);

            Color RETC = new Color();
            RETC.R = Convert.ToByte(R0 + R1 + R2);
            RETC.G = Convert.ToByte(G0 + G1 + G2);
            RETC.B = Convert.ToByte(B0 + B1 + B2);
            return RETC;
        }
        private static Color Achromatopsia(Color intake)
        {
            double R0 = (intake.R / 100 * 29.900f);
            double R1 = (intake.G / 100 * 58.700f);
            double R2 = (intake.B / 100 * 11.400f);

            double G0 = (intake.R / 100 * 29.900f);
            double G1 = (intake.G / 100 * 58.700f);
            double G2 = (intake.B / 100 * 11.400f);

            double B0 = (intake.R / 100 * 29.900f);
            double B1 = (intake.G / 100 * 58.700f);
            double B2 = (intake.B / 100 * 11.400f);

            Color RETC = new Color();
            RETC.R = Convert.ToByte(R0 + R1 + R2);
            RETC.G = Convert.ToByte(G0 + G1 + G2);
            RETC.R = Convert.ToByte(B0 + B1 + B2);
            return RETC;
        }
        private static Color Achromatomaly(Color intake)
        {
            double R0 = (intake.R / 100 * 61.800f);
            double R1 = (intake.G / 100 * 32.000f);
            double R2 = (intake.B / 100 * 06.200f);

            double G0 = (intake.R / 100 * 16.300f);
            double G1 = (intake.G / 100 * 77.500f);
            double G2 = (intake.B / 100 * 06.200f);

            double B0 = (intake.R / 100 * 16.300f);
            double B1 = (intake.G / 100 * 32.000f);
            double B2 = (intake.B / 100 * 51.600f);

            Color RETC = new Color();
            RETC.R = Convert.ToByte(R0 + R1 + R2);
            RETC.G = Convert.ToByte(G0 + G1 + G2);
            RETC.B = Convert.ToByte(B0 + B1 + B2);
            return RETC;
        }
    }

}
