using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp;
using System.Drawing.Imaging;
using System.Drawing;



namespace openCv
{
    public static class Filters
    {

	    public static Mat ApplyCanny(Mat src, double threshold1 = 50, double threshold2 = 200)
        {
            
            if (threshold1 == 0) {
                threshold1 = 50;
            }
            if (threshold2 == 0)
            {
                threshold2 = 100;
            }
            Mat dst = new Mat();
            if (src != null)
            {
                Cv2.Canny(convertToGrayScale(src), dst, threshold1, threshold2);
            }
            return dst;
        }

        public static Mat _1977(Mat src) {
            Mat dst = new Mat();
            //Cv2.AdaptiveThreshold(src.ExtractChannel(1), dst, 100, AdaptiveThresholdType.GaussianC, ThresholdType.BinaryInv, 3, 3);
            Cv2.Threshold(src.ExtractChannel(1), dst, 100,300,ThresholdType.ToZero);
            dst = Tint(dst);  
            return dst;
        }

        public static Mat ApplyGaussBlur(Mat src, OpenCvSharp.CPlusPlus.Size s)
        {
            Mat dst = new Mat();
            if (s == null)
            {
                dst = ApplyGaussBlur(src);
            }
            else
            {
                if (src != null)
                {
                    if (s.Height % 2 == 0)
                    {
                        s.Height += 1;
                    }
                    if (s.Width % 2 == 0)
                    {
                        s.Width += 1;
                    }
                    Cv2.GaussianBlur(src, dst, s, 0);
                }
            }
            return dst;
        }
        //overload
        public static Mat ApplyGaussBlur(Mat src)
        {
            Mat dst = new Mat();
            if (src != null)
            {
                Cv2.GaussianBlur(src, dst, new OpenCvSharp.CPlusPlus.Size(51,51), 0);
            }
            return dst;
        }

        public static Mat convertToGrayScale(Mat src)
        {
            Mat dest = new Mat();
            if (src != null)
            {
                dest = Mat.FromImageData(src.ToBytes(), LoadMode.GrayScale);
            }
            return dest;
        }

        public static Mat ftr(Mat src) {
            var cm = redCm;

            var img = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(src) ;
            var ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            var bmp = new Bitmap(img.Width, img.Height);
            var gfx = Graphics.FromImage(bmp);
            var rect = new Rectangle(0, 0, img.Width, img.Height);

            gfx.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            Mat dst = OpenCvSharp.Extensions.BitmapConverter.ToMat(bmp);
            return dst;
        }

        public static Mat Tint(Mat src,ColorMatrix colMat)
        {
            Mat dst = new Mat();
            if (colMat != null)
            {
                dynamic cm = new ColorMatrix(new float[][]
                {
                    new float[] {1.3f, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0.3f, 0, 0, 0, 1}
                });
                cm = colMat;
                var img = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(src);
                var ia = new ImageAttributes();
                ia.SetColorMatrix(cm);

                var bmp = new Bitmap(img.Width, img.Height);
                var gfx = Graphics.FromImage(bmp);
                var rect = new Rectangle(0, 0, img.Width, img.Height);

                gfx.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

                dst = OpenCvSharp.Extensions.BitmapConverter.ToMat(bmp);
            }
            else
            {
                dst = Tint(src);
                
            }

           
            return dst;
        }
        //overload
        public static Mat Tint(Mat src)
        {
            var cm = redCm;

            var img = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(src);
            var ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            var bmp = new Bitmap(img.Width, img.Height);
            var gfx = Graphics.FromImage(bmp);
            var rect = new Rectangle(0, 0, img.Width, img.Height);

            gfx.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            Mat dst = OpenCvSharp.Extensions.BitmapConverter.ToMat(bmp);
            return dst;
        }

        //static Mat centerFocus(Mat src) {
        //    Mat dest = new Mat();
        //    Mat mask = new Mat(src.size(),  CV_8UC1);
        //    Core.circle(subImage, point, radius, new Scalar(224, 224, 224, 10), -1);
        //    Mat blurredImage = new Mat();
        //    Imgproc.GaussianBlur(subImage, blurredImage, new Size(55, 55), BLUR_STR);
        //    blurredImage.copyTo(subImage, mask);
        //    return dest;
        //}


        public  static ColorMatrix redCm = new ColorMatrix(new float[][]
                {
                    new float[] {1.3f, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0.3f, 0, 0, 0, 1}
                });
        public static ColorMatrix greenCm = new ColorMatrix(new float[][]
                {
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 1.3f, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0.3f, 0, 0, 1}
                });
        public static ColorMatrix blueCm = new ColorMatrix(new float[][]
                {
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1.3f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0.3f, 0, 1}
                });
        public static ColorMatrix whiteCm = new ColorMatrix(new float[][]
                {
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

    }
}