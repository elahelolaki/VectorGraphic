using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VectorGraphic.Shapes
{
    public class RectangleShape : BaseShape, IBaseShape
    {
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string d { get; set; }


        public void SetPoints()
        {
            var aPoints = a.Split(';');
            var x = double.Parse(aPoints[0].Replace(',', '.'));
            var y = double.Parse(aPoints[1].Replace(',', '.'));
            Points.Add(new Point(x, y));

            var bPoints = b.Split(';');
            var x1 = double.Parse(bPoints[0].Replace(',', '.'));
            var y1 = double.Parse(bPoints[1].Replace(',', '.'));
            Points.Add(new Point(x1, y1));

            var cPoints = c.Split(';');
            var x2 = double.Parse(cPoints[0].Replace(',', '.'));
            var y2 = double.Parse(cPoints[1].Replace(',', '.'));
            Points.Add(new Point(x2, y2));

            var dPoints = d.Split(';');
            var x3 = double.Parse(dPoints[0].Replace(',', '.'));
            var y3 = double.Parse(dPoints[1].Replace(',', '.'));
            Points.Add(new Point(x3, y3));

        }

        public List<Point> GetPoints() => Points.Select(s => s).ToList();

        public void Render(double center, double unit, Canvas myGrid)
        { 
            var newPoints = new List<Point>();
            for (int i = 0; i < 4; i++)
            {
                var cx = center + (Points[i].X * unit);
                var cy = center - (Points[i].Y * unit);
                newPoints.Add(new Point(Math.Round(cx), Math.Round(cy)));
            }
            var maxX = newPoints.Max(s => s.X);
            var maxY = newPoints.Max(s => s.Y);
            var minX = newPoints.Min(s => s.X);
            var minY = newPoints.Min(s => s.Y);

            var rect = new System.Windows.Shapes.Rectangle(); 
            rect.Width = maxX- minX;
            rect.Height = maxY - minY;
            Canvas.SetLeft(rect, minX);
            Canvas.SetTop(rect, minY);

            rect.StrokeThickness = 2;
            if (filled)
                rect.Fill = getColor();
            rect.Stroke = getColor();
            myGrid.Children.Add(rect);
        }
    }
}
