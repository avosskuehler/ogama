using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GTApplication.Tools
{
    #region Includes

    

    #endregion

    public static class ScreenShot
    {
        // Partial adoptation from http://www.grumpydev.com/2009/01/03/taking-wpf-screenshots/
        // Thanks Steven Robbins

        public static BitmapSource GetScreenShot(this UIElement source, double scale)
        {
            double actualHeight = source.RenderSize.Height;
            double actualWidth = source.RenderSize.Width;
            double renderHeight = actualHeight*scale;
            double renderWidth = actualWidth*scale;

            var renderTarget = new RenderTargetBitmap((int) renderWidth, (int) renderHeight, 96, 96,
                                                      PixelFormats.Pbgra32);
            var sourceBrush = new VisualBrush(source);

            var drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            using (drawingContext)
            {
                drawingContext.PushTransform(new ScaleTransform(scale, scale));
                drawingContext.DrawRectangle(sourceBrush, null,
                                             new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
            }

            renderTarget.Render(drawingVisual);
            return renderTarget;
        }
    }
}