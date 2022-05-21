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
    public class BaseShape 
    {
        public string color { get; set; }

        public string type { get; set; }

        public bool filled { get; set; }

        public PointCollection Points { get; set; } = new PointCollection();

        public List<byte> colorBytes => color.Split(';').Select(s => byte.Parse(s)).ToList();



        public Brush getColor()
        {
            return new SolidColorBrush(Color.FromArgb(colorBytes[0], colorBytes[1], colorBytes[2], colorBytes[3]));
        }
    }
    public interface IBaseShape
    {
        void SetPoints();

        List<Point> GetPoints();
        void Render(double center, double unit, Canvas myGrid);
    }


}
