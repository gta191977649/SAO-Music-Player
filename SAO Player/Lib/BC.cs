using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
namespace SAO_Player
{
    /// <summary>
    /// 
    /// </summary>
    public class BC
    {
        public static Brush brushFromHex(string hex)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(hex);
            brush.Freeze();
            return brush;
        }
  

    }
}
