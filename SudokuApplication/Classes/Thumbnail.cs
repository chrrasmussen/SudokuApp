using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace SudokuApplication
{
    class Thumbnail
    {
        public static Bitmap CreateThumbnail(int boardSize, int blockWidth, int blockHeight)
        {
            // Configuration
            int margin = 5;
            int boardFactor = 10;
            int width = boardFactor * boardSize + margin * 2;
            int height = boardFactor * boardSize + margin * 2;
            Pen stroke = new Pen(Color.Black, 2.0f);
            Brush cellFill = new SolidBrush(Color.White);

            Bitmap thumbnail = new Bitmap(width, height);
            Graphics graphicsContext = Graphics.FromImage(thumbnail);

            int rectangleWidth = boardFactor * blockWidth;
            int rectangleHeight = boardFactor * blockHeight;

            // Background color
            graphicsContext.Clear(SudokuForm.DefaultBackColor);

            for (int i = 0; i != boardSize / blockHeight; i++)
            {
                for (int j = 0; j != boardSize / blockWidth; j++)
                {
                    Rectangle rectangle = new Rectangle(margin + j * rectangleWidth, margin + i * rectangleHeight, rectangleWidth, rectangleHeight);
                    graphicsContext.FillRectangle(cellFill, rectangle);
                    graphicsContext.DrawRectangle(stroke, rectangle);
                }
            }

            return thumbnail;
        }
    }
}
