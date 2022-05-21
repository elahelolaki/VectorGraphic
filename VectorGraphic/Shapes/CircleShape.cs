using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorGraphic.Shapes
{
    public class CircleShape: BaseShape, IBaseShape
    {
        public string center { get; set; }

        public double radius { get; set; } 

        public void SetPoints()
        {
            var cenPoints = center.Split(';');
            var x = double.Parse(cenPoints[0].Replace(',', '.'));
            var y = double.Parse(cenPoints[1].Replace(',', '.')); 
            Points.Add(new Point(x + radius, y));
            Points.Add(new Point(x - radius, y));
            Points.Add(new Point(x , y + radius));
            Points.Add(new Point(x, y - radius));
        }
         
        public List<Point> GetPoints() => Points.Select(s => s).ToList();

        public void Render(double center, double unit, Canvas myGrid)
        {
            Ellipse el = new Ellipse();
            el.Width = radius * 2 * unit;
            el.Height = radius * 2 * unit;
            el.SetValue(Canvas.LeftProperty, (Double)center + ((0) * unit) - (radius * unit));
            el.SetValue(Canvas.TopProperty, (Double)center + ((0) * unit) - (radius * unit));
             
            el.Stroke = getColor();
            el.StrokeThickness = 2;
            if (filled)
            el.Fill = getColor();
            myGrid.Children.Add(el);
        }
         
    }
}
