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
        private static readonly SolidBrush _drawBrush = new SolidBrush(Color.Black);
        private static readonly Pen _pen = new Pen(Color.DimGray, 1);
        private static readonly Pen _pen1 = new Pen(Color.Red, 3);

        public Bitmap DrawGraph(GraphRenderModel model)
        {
            var bitmap = new Bitmap(1000, 600);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                foreach (var point in model.Points)
                {
                    g.FillEllipse(_drawBrush, point.X - 5, point.Y - 5, 10, 10);
                }

                foreach (var line in model.Lines)
                {
                    g.DrawLine(_pen, line.From.X, line.From.Y, line.To.X, line.To.Y);
                }

                foreach (var line in model.Path)
                {
                    g.DrawLine(_pen1, line.From.X, line.From.Y, line.To.X, line.To.Y);
                }

                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            return bitmap;
        }
    }
}
