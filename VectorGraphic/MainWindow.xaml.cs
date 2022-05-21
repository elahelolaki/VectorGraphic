using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using VectorGraphic.Shapes;

namespace VectorGraphic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnJson_Click(object sender, RoutedEventArgs e)
        {
            myGrid.Children.Clear();
            var root = AppDomain.CurrentDomain.BaseDirectory;
            using (StreamReader r = new StreamReader(root.Split("bin")[0] + "Data\\Input.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<JArray>(json);

                List<IBaseShape> shapes = new List<IBaseShape>();
                foreach (var item in items)
                {
                    switch (item["type"].Value<string>())
                    {
                        case "line":
                            var lineShp = new LineShape
                            {
                                a = item["a"].Value<string>(),
                                b = item["b"].Value<string>(),
                                type = item["type"].Value<string>(),
                                color = item["color"].Value<string>()
                            };
                            lineShp.SetPoints();
                            shapes.Add(lineShp);
                            break;
                        case "circle":
                            var circleShp = new CircleShape
                            {
                                center = item["center"].Value<string>(),
                                radius = item["radius"].Value<double>(),
                                type = item["type"].Value<string>(),
                                color = item["color"].Value<string>(),
                                filled = item["filled"].Value<bool>()
                            };
                            circleShp.SetPoints();
                            shapes.Add(circleShp);
                            break;
                        case "triangle":
                            var triangleShp = new TriangleShape
                            {
                                a = item["a"].Value<string>(),
                                b = item["b"].Value<string>(),
                                c = item["c"].Value<string>(),
                                type = item["type"].Value<string>(),
                                color = item["color"].Value<string>(),
                                filled = item["filled"].Value<bool>()
                            };
                            triangleShp.SetPoints();
                            shapes.Add(triangleShp);
                            break;
                        case "rectangle":
                            var rectangleShp = new RectangleShape
                            {
                                a = item["a"].Value<string>(),
                                b = item["b"].Value<string>(),
                                c = item["c"].Value<string>(),
                                d = item["d"].Value<string>(),
                                type = item["type"].Value<string>(),
                                color = item["color"].Value<string>(),
                                filled = item["filled"].Value<bool>()
                            };
                            rectangleShp.SetPoints();
                            shapes.Add(rectangleShp);
                            break;
                        default:
                            break;
                    }
                } 
                showShapes(shapes);

            }
        }

        private void btnXML_Click(object sender, RoutedEventArgs e)
        {
            myGrid.Children.Clear();
            var root = AppDomain.CurrentDomain.BaseDirectory;
            string xmlFileName = root.Split("bin")[0] + "Data\\Input.xml";
            XDocument xdoc = XDocument.Load(xmlFileName);

            List<IBaseShape> shapes = new List<IBaseShape>();
            foreach (XElement elem in xdoc.Descendants("element"))
            {
                switch (elem.Element("type")?.Value)
                {
                    case "line":
                        var lineShp = new LineShape
                        {
                            a = elem.Element("a")?.Value,
                            b = elem.Element("b")?.Value,
                            type = elem.Element("type")?.Value,
                            color = elem.Element("color")?.Value
                        };
                        lineShp.SetPoints();
                        shapes.Add(lineShp);
                        break;
                    case "circle":
                        var circleShp = new CircleShape
                        {
                            center = elem.Element("center")?.Value,
                            radius = double.Parse(elem.Element("radius")?.Value),
                            type = elem.Element("type")?.Value,
                            color = elem.Element("color")?.Value,
                            filled = bool.Parse(elem.Element("filled")?.Value)
                        };
                        circleShp.SetPoints();
                        shapes.Add(circleShp);
                        break;
                    case "triangle":
                        var triangleShp = new TriangleShape
                        {
                            a = elem.Element("a")?.Value,
                            b = elem.Element("b")?.Value,
                            c = elem.Element("c")?.Value,
                            type = elem.Element("type")?.Value,
                            color = elem.Element("color")?.Value,
                            filled = bool.Parse(elem.Element("filled")?.Value)
                        };
                        triangleShp.SetPoints();
                        shapes.Add(triangleShp);
                        break;
                    case "rectangle":
                        var rectangleShp = new RectangleShape
                        {
                            a = elem.Element("a")?.Value,
                            b = elem.Element("b")?.Value,
                            c = elem.Element("c")?.Value,
                            d = elem.Element("d")?.Value,
                            type = elem.Element("type")?.Value,
                            color = elem.Element("color")?.Value,
                            filled = bool.Parse(elem.Element("filled")?.Value)
                        };
                        rectangleShp.SetPoints();
                        shapes.Add(rectangleShp);
                        break;
                    default:
                        break;
                }
            } 
            showShapes(shapes);
        }


        private void showShapes(List<IBaseShape> shapes)
        {
            var allPoint = shapes.SelectMany(s => s.GetPoints().Select(w => new { w.X, w.Y })).ToList();
            var maxX = allPoint.Max(s => Math.Abs(s.X));
            var maxY = allPoint.Max(s => Math.Abs(s.Y));


            var scale = (maxX > maxY ? Math.Ceiling(maxX) : Math.Ceiling(maxY));
            lblBottom.Content = lblLeft.Content = (-scale).ToString();
            lblTop.Content = lblRight.Content = (scale).ToString();

            scale *= 2;
            if (scale % 2 == 1)
                scale++;

            var paperLength = 400;
            var center = paperLength / 2;

            var unit = paperLength / scale;


            for (int i = 0; i <= scale; i++)
            {
                if ((scale / 2) == i)
                {
                    myGrid.Children.Add(new Line { Stroke = System.Windows.Media.Brushes.Black, X1 = 0, Y1 = center, X2 = paperLength, Y2 = center, StrokeThickness = 3 });
                    myGrid.Children.Add(new Line { Stroke = System.Windows.Media.Brushes.Black, X1 = center, Y1 = 0, X2 = center, Y2 = paperLength, StrokeThickness = 3 });
                }
                else
                {
                    myGrid.Children.Add(new Line { Stroke = System.Windows.Media.Brushes.Gray, X1 = 0, Y1 = i * unit, X2 = paperLength, Y2 = i * unit, StrokeThickness = 1 });
                    myGrid.Children.Add(new Line { Stroke = System.Windows.Media.Brushes.Gray, X1 = i * unit, Y1 = 0, X2 = i * unit, Y2 = paperLength, StrokeThickness = 1 });
                }
            }

            foreach (var shp in shapes)
            {
                shp.Render(center, unit, myGrid);
            }
        }
    }
}
