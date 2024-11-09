﻿using SkiaSharp;

public static class BlurArchiveLogo
{
    public static void Main()
    {
        Generate("Blue", "Archive", -15, 0);
    }

    public static void Generate(string left, string right, int haloX, int haloY)
    {
        double offsetX = 250 / Math.Tan(double.DegreesToRadians(60));
        SKTypeface face = SKTypeface.FromFile("BlueArchive.ttf");
        SKFont font = new SKFont(face, 84);
        float leftWidth = font.MeasureText(left);
        float rightWidth = font.MeasureText(right);
        float width = (float)(Math.Max(leftWidth, rightWidth) * 2D + offsetX);
        using SKSurface? surface = SKSurface.Create(new SKImageInfo((int)width, 250));
        SKCanvas? canvas = surface.Canvas;
        canvas.Clear(SKColors.White);
        using SKPaint haloPaint = new SKPaint();
        haloPaint.IsAntialias = true;
        canvas.Save();
        SKMatrix matrix = SKMatrix.CreateSkew(-0.5F, 0);
        canvas.Concat(in matrix);
        haloPaint.Color = new SKColor(18, 138, 250);
        canvas.DrawText(left, (float)((width + offsetX) / 2) - (rightWidth - leftWidth) / 2, (float)(250 * 0.68),
            SKTextAlign.Right, font, haloPaint);
        haloPaint.Color = new SKColor(43, 43, 43);
        canvas.DrawText(right, (float)((width + offsetX) / 2) - (rightWidth - leftWidth) / 2, (float)(250 * 0.68),
            SKTextAlign.Left, font, haloPaint);
        canvas.Restore();
        using SKBitmap halo = SKBitmap.Decode("halo.png");
        canvas.DrawBitmap(halo, (width - 250F) / 2F + haloX, haloY, haloPaint);

        using SKData? output = surface.Snapshot().Encode();
        File.WriteAllBytes("output.png", output.ToArray());
    }
}