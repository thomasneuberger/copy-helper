using System.Drawing;
using System.Drawing.Imaging;

namespace CopyHelper.Helpers;

public static class IconHelper
{
    public static Icon CreateIcon(string text, Color backgroundColor)
    {
        // Create a bitmap
        using var bitmap = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.Clear(backgroundColor);
            
            // Draw text
            using var font = new Font("Arial", 10, FontStyle.Bold);
            var textSize = graphics.MeasureString(text, font);
            var x = (bitmap.Width - textSize.Width) / 2;
            var y = (bitmap.Height - textSize.Height) / 2;
            
            graphics.DrawString(text, font, Brushes.White, x, y);
        }

        // Create icon from bitmap
        var icon = Icon.FromHandle(bitmap.GetHicon());
        return icon;
    }
}
