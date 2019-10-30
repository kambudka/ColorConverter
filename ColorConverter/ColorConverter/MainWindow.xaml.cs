using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ColorPicker1_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            R.Value = ColorPicker1.SelectedColor.Value.R;
            G.Value = ColorPicker1.SelectedColor.Value.G;
            B.Value = ColorPicker1.SelectedColor.Value.B;

            CmykColor color = ConvertRgbToCmyk(ColorPicker1.SelectedColor.Value.R, 
                                                ColorPicker1.SelectedColor.Value.G,
                                                ColorPicker1.SelectedColor.Value.B);
            C.Value = (double)color.C *100;
            M.Value = (double)color.M * 100;
            Y.Value = (double)color.Y * 100;
            K.Value = (double)color.K * 100;

        }

        private void ChangeToCMYK_Click(object sender, RoutedEventArgs e)
        {
            CmykColor color = ConvertRgbToCmyk(R.Value.GetValueOrDefault(),
                                    G.Value.GetValueOrDefault(),
                                    B.Value.GetValueOrDefault());
            C.Value = (double)color.C * 100;
            M.Value = (double)color.M * 100;
            Y.Value = (double)color.Y * 100;
            K.Value = (double)color.K * 100;
        }

        private void ChangeToRGB_Click(object sender, RoutedEventArgs e)
        {
            Color color = ConvertCmykToRgb(C.Value.GetValueOrDefault(),
                                            M.Value.GetValueOrDefault(),
                                            Y.Value.GetValueOrDefault(),
                                            K.Value.GetValueOrDefault());

            R.Value = color.R;
            G.Value = color.G;
            B.Value = color.B;
        }

        public static ColorConverter.CmykColor ConvertRgbToCmyk(int r, int g, int b)
        {
            float c;
            float m;
            float y;
            float k;
            float rf;
            float gf;
            float bf;

            rf = r / 255F;
            gf = g / 255F;
            bf = b / 255F;

            k = ClampCmyk(1 - Math.Max(Math.Max(rf, gf), bf));
            c = ClampCmyk((1 - rf - k) / (1 - k));
            m = ClampCmyk((1 - gf - k) / (1 - k));
            y = ClampCmyk((1 - bf - k) / (1 - k));

            return new ColorConverter.CmykColor(c, m, y, k);
        }

        public static Color ConvertCmykToRgb(double c, double m, double y, double k)
        {
            byte r;
            byte g;
            byte b;

            r = Convert.ToByte(255 * (1 - c) * (1 - k));
            g = Convert.ToByte(255 * (1 - m) * (1 - k));
            b = Convert.ToByte(255 * (1 - y) * (1 - k));

            return Color.FromRgb(r, g, b);
        }

        private static float ClampCmyk(float value)
        {
            if (value < 0 || float.IsNaN(value))
            {
                value = 0;
            }

            return value;
        }
    }
}
