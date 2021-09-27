using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CryptographWPF
{
    public static class ColorSchemes
    { 
        static public Color BackgroundColor { get; } = (Color)ColorConverter.ConvertFromString("#343d46");
        static public Color ButtonColor { get; } = (Color)ColorConverter.ConvertFromString("#4f5b66");
        static public Color color3 { get; } = (Color)ColorConverter.ConvertFromString("#65737e");
        static public Color color4 { get; } = (Color)ColorConverter.ConvertFromString("#a7adba");
        static public Color color5 { get; } = (Color)ColorConverter.ConvertFromString("#c0c5ce");

    }
}
