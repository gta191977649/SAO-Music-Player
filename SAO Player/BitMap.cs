using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Interop;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.IO;
namespace SAO_Player
{
    class BitMap
    {
        IntPtr hBitmap;
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

   

        public static ImageSource ChangeBitmapToImageSource(Bitmap bitmap)
        {
            
            //Bitmap bitmap = icon.ToBitmap();

            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(

                hBitmap,

                IntPtr.Zero,

                Int32Rect.Empty,

                BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(hBitmap))
            {

                throw new System.ComponentModel.Win32Exception();

            }
            
            return wpfBitmap;

        }
    }

}
