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
    public class NRListBoxItem : ListBoxItem
    {
        public NRListBox NContainer { get; set; }
        public bool Override { get; set; } = false;
        public NRListBoxItem()
        {
            Selected += NLBI_SEL;

            Style = (Style)FindResource("NR_LISTBOXITEM");
            BorderThickness = new Thickness(2, 2, 2, 2);
            FontSize = 14;
            FontWeight = FontWeights.Bold;
            FontFamily = new FontFamily("Global User Interface");
            Foreground = new SolidColorBrush(Container.Colors.MAIN);
            BorderBrush = new SolidColorBrush(Container.Colors.BACK);
            Background = new SolidColorBrush(Container.Colors.BACK);
        }
        private void NLBI_SEL(object sender, RoutedEventArgs e)
        {
            foreach (var item in NContainer.Items)
            {
                NRListBoxItem tmpItem = item as NRListBoxItem;
                 NDC.NStyle.Transition.Smooth.ListBoxItem.BorderBrush(tmpItem, Container.Colors.BACK, 250);
                 NDC.NStyle.Transition.Smooth.ListBoxItem.Background(tmpItem, Container.Colors.BACK, 250);
                 NDC.NStyle.Transition.Smooth.ListBoxItem.Foreground(tmpItem, Container.Colors.MAIN, 250);
            }
             NDC.NStyle.Transition.Smooth.ListBoxItem.BorderBrush(this, Container.Colors.MAIN, 250);
             NDC.NStyle.Transition.Smooth.ListBoxItem.Background(this, Container.Colors.MAIN, 250);
             NDC.NStyle.Transition.Smooth.ListBoxItem.Foreground(this, Container.Colors.BACK, 250);
        }
        public void Update()
        {
            if (!Override)
            {
                 NDC.NStyle.Transition.Smooth.ListBoxItem.Foreground(this, Container.Colors.MAIN, 250);
                 NDC.NStyle.Transition.Smooth.ListBoxItem.BorderBrush(this, Container.Colors.BACK, 250);
                 NDC.NStyle.Transition.Smooth.ListBoxItem.Background(this, Container.Colors.BACK, 250);
            }
        }
    }
}
