using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace VectorGraphic.Shapes
{
    public class TriangleShape : BaseShape, IBaseShape
    {
        public string a { get; set; }

        public string b { get; set; }
        public string c { get; set; }


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
             
        }

        public List<Point> GetPoints() => Points.Select(s => s).ToList();

        public void Render(double center, double unit, Canvas myGrid)
        { 
            var newPoints = new PointCollection();
            for (int i = 0; i < 3; i++)
            {
                var x = center + (Points[i].X * unit);
                var y = center - (Points[i].Y * unit);
                newPoints.Add(new Point(Math.Round(x), Math.Round(y)));
            }
            Polygon pl = new Polygon { Points = newPoints, StrokeThickness = 2};
            if (filled)
                pl.Fill = getColor();
            pl.Stroke = getColor();
            myGrid.Children.Add(pl);
        }
    }
}
