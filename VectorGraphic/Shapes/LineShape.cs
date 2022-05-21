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
    public class LineShape : BaseShape, IBaseShape
    {
        public string a { get; set; }

        public string b { get; set; } 

        public void SetPoints()
        { 
            var aPoints=a.Split(';');
            var x = double.Parse(aPoints[0].Replace(',', '.'));
            var y = double.Parse(aPoints[1].Replace(',', '.'));
            Points.Add(new Point(x, y)); 

            var bPoints = b.Split(';');
            var x1 = double.Parse(bPoints[0].Replace(',', '.'));
            var y1 = double.Parse(bPoints[1].Replace(',', '.'));
            Points.Add(new Point(x1, y1)); 
        }

        public List<Point> GetPoints() => Points.Select(s => s).ToList();

        public void Render(double center,double unit, Canvas myGrid)
        {
            var myLine = new Line(); 
            myLine.Stroke = getColor(); 

            myLine.X1 = center + ((Points[0].X) * unit);
            myLine.Y1 = center - ((Points[0].Y) * unit);
            myLine.X2 = center + ((Points[1].X) * unit);
            myLine.Y2 = center - ((Points[1].Y) * unit);
            myLine.StrokeThickness = 2;
            myGrid.Children.Add(myLine);
        }

    }
}
