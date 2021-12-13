using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentReplacementProblem.Business.Dto;
using EquipmentReplacementProblem.Render.Models;

namespace EquipmentReplacementProblem.Render.Drawing
{
    public class GraphRender
    {
        private static readonly Font _font = new Font("Arial", 10);
        private static readonly SolidBrush _drawBrush = new SolidBrush(Color.Black);
        private static readonly Pen _pen = new Pen(Color.DimGray, 1);
        private static readonly Pen _pen1 = new Pen(Color.Red, 3);
        private static readonly Pen _pen3 = new Pen(Color.DarkSlateGray, 2);

        public Bitmap DrawGraph(GraphRenderModel model)
        {
            var bitmap = new Bitmap(1000, 600);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                foreach (var point in model.Points)
                {
                    g.FillEllipse(_drawBrush, point.X - 5, nY(point.Y + 5), 10, 10);
                }

                foreach (var line in model.Lines)
                {
                    g.DrawLine(_pen, line.From.X, nY(line.From.Y), line.To.X, nY(line.To.Y));
                }

                foreach (var line in model.Path)
                {
                    g.DrawLine(_pen1, line.From.X, nY(line.From.Y), line.To.X, nY(line.To.Y));
                }

                var marksCount = model.Path.Count + 5;
                g.DrawLine(_pen3, 0, nY(0), marksCount * 50, nY(0));
                
                for (var i = 1; i <= marksCount; i++)
                {
                    g.DrawLine(_pen3, i * 50, nY(0), i * 50, nY(10));
                    g.DrawString($"{i}", _font, _drawBrush, i * 50 + 3, nY(18));
                }

                g.DrawLine(_pen3, 0, nY(0), 0, nY(marksCount * 50));
                for (var i = 1; i <= marksCount; i++)
                {
                    g.DrawLine(_pen3, 0, nY(i * 50), 10, nY(i * 50));
                    g.DrawString($"{i}", _font, _drawBrush, 8, nY(i * 50 + 3));
                }

                g.DrawString("Sell", _font, _drawBrush, model.SellPoint.X + 10, nY(model.SellPoint.Y - 5));
            }

            return bitmap;
        }

        private int nY(int Y) => 600 - Y;
    }
}
