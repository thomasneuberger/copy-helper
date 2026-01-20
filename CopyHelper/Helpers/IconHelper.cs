using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CopyHelper.Helpers;

public static class IconHelper
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool DestroyIcon(IntPtr handle);

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
        IntPtr hIcon = bitmap.GetHicon();
        Icon icon = Icon.FromHandle(hIcon);
        
        // Clone the icon so we can safely destroy the handle
        Icon clonedIcon = (Icon)icon.Clone();
        
        // Clean up the handle to prevent GDI handle leak
        DestroyIcon(hIcon);
        
        return clonedIcon;
    }
}
