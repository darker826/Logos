/* @Auther Jaeung Choi(darker826, darker826@gmail.com)
* 녹화 기능을 담당하는 클래스입니다.
* 객체를 생성 후 함수를 부르기만 하면 사용이 가능함.
* .avi 동영상 파일의 생성에는 AForge.Video.FFMPEG wrapper를 사용하여 FFMPEG 라이브러리를 통해 생성합니다.
* 동영상 파일의 프레임은 KinectColorViewer에서 WriteableBitmap를 Bitmap으로 Convert하여 Bitmap을 넣는 것으로 처리합니다.
*/


namespace Microsoft.Samples.Kinect.WpfViewers
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;
    using System.Windows.Media.Imaging;
    using System.Runtime.InteropServices;

    using Microsoft.Kinect;
    using AForge.Video.FFMPEG;

    public class RecordController
    {
        private static VideoFileWriter writer;
        public static bool openBool = true;
        public static bool recordBool = false;

        public RecordController()
        {
            writer = new VideoFileWriter();
        }

        //녹화 시작할 때 부를 함수.
        public void recordingStart(ColorImageFrame colorImage, WriteableBitmap writeBmp)
        {
            if (recordBool)
            {
                //        Bitmap bmap = colorFrameToBitmap(colorImage);
                Bitmap bmap = bitmapFromWriteableBitmap(writeBmp);

                if (openBool)
                {
                    String fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".avi";
                    String filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                    String file = filePath + "//" + fileName;

                    openBool = false;
                    writer.Open(file, colorImage.Width, colorImage.Height, 25, VideoCodec.MPEG4);
                }

                writer.WriteVideoFrame(bmap);
            }
        }

        //녹화를 중지할때 부를 함수
        public static void recordingStop()
        {
            writer.Close();
        }

        //colorImageFrame을 받아서 Bitmap으로 변환해주는 함수.
        /*
        private Bitmap colorFrameToBitmap(ColorImageFrame colorImage)
        {
            Bitmap bmap;
            BitmapData bmapdata;
            byte[] pixeldata;
            pixeldata = new byte[colorImage.PixelDataLength];
            colorImage.CopyPixelDataTo(pixeldata);
            bmap = new Bitmap(colorImage.Width, colorImage.Height, PixelFormat.Format24bppRgb);
            bmap.SetPixel(colorImage.Width-1, colorImage.Height-1, Color.Red);
            bmapdata = bmap.LockBits(
            new System.Drawing.Rectangle(0, 0, colorImage.Width, colorImage.Height),
            ImageLockMode.WriteOnly,
            bmap.PixelFormat);
            IntPtr ptr = bmapdata.Scan0;
            Marshal.Copy(pixeldata, 0, ptr, bmapdata.Stride * bmap.Height);
            bmap.UnlockBits(bmapdata);
            return bmap;
        }
        */

        //기존에 쓰이던 WriteableBitmap을 bitmap으로 convert해주는 함수
        public Bitmap bitmapFromWriteableBitmap(WriteableBitmap writeBmp)
        {
            Bitmap bmp;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)writeBmp));
                enc.Save(outStream);
                bmp = new System.Drawing.Bitmap(outStream);
            }
            return bmp;
        }
    }
}
