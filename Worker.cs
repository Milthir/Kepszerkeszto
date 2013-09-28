using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Kepszerkeszto
{
    class Worker
    {
        public static void Invert(Bitmap im)
        {
            Bitmap bmp = im;

            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                           ImageLockMode.ReadWrite,
                                           PixelFormat.Format32bppArgb);

            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - bmp.Width * 4;
                int nWidth = bmp.Width;

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < nWidth; x++)
                    {
                        p[0] = (byte)(255 - p[0]); 
                        p[1] = (byte)(255 - p[1]); 
                        p[2] = (byte)(255 - p[2]); 

                        p += 4;
                    }
                    p += nOffset;
                }
            }

            bmp.UnlockBits(bmData);
        }
        public static void GrayScale(Bitmap im)
        {
            BitmapData bmData = im.LockBits(new Rectangle(0, 0, im.Width, im.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - im.Width * 3;

                byte red, green, blue;

                for (int y = 0; y < im.Height; ++y)
                {
                    for (int x = 0; x < im.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        p[0] = p[1] = p[2] = (byte)(.299 * red + .587 * green + .114 * blue);

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            im.UnlockBits(bmData);
        }
    }
}
