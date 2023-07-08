using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CogoTestBed
{
    public class HierarchyElement
    {
        static int counter = 1;

        #region Properties

        public int ID { get; set; }

        public ShapeType ShapeType { get; set; }

        public List<Edge> Edges { get; set; }

        public List<HierarchyElement> Holes { get; set; }

        public HierarchyElement? Parent { get; set; }

        public List<HierarchyElement> Siblings { get; set; }

        public Color Color { get; set; }

        #endregion

        #region Constructor

        public HierarchyElement()
        {
            ID = counter;
            counter++;

            Edges = new List<Edge>();
            Holes = new List<HierarchyElement>();
            Siblings = new List<HierarchyElement>();

            List<string> colors = new List<string>()
            { "Aqua", "Aquamarine", "Black", "Blue", "BlueViolet", "Brown", "BurlyWood", "CadetBlue",
              "Chartreuse", "Chocolate", "Coral", "CornflowerBlue", "Crimson", "Cyan", "DarkBlue",
              "DarkCyan", "DarkGoldenrod", "DarkGreen", "DarkMagena", "DarkOliveGreen", "DarkOrange",
              "DarkOrchid", "DarkRed", "DarkSalmon", "DarkSlateBlue", "DarkSlateGray", "DarkTurquoise",
              "DarkViolet", "DeepPink", "DeepSkyBlue", "DimGray", "DodgerBlue", "Firebrick", "ForestGreen",
              "Fuschia", "Gold", "Goldenrod", "Gray", "Green", "HotPink", "IndianRed", "Indigo", "LawnGreen",
              "LightCoral", "LightGreen", "LightPink", "LightSalmon", "LightSeaGreen", "LightSkyBlue",
              "LightSlateGray", "LightSteelBlue", "Lime", "LimeGreen", "Magenta", "Maroon", "MediumAquamarine",
              "MediumBlue", "MediumOrchid", "MediumPurple", "MediumSeaGreen", "MediumSlateBlue", "MediumSpringGreen",
              "MediumTurquoise", "MediumVioletRed", "MidnightBlue", "Navy", "Olive", "OliveDrab", "Orange",
              "OrangeRed", "Orchid", "PaleVioletRed", "Peru", "Plum", "Purple", "Red", "RosyBrown",
              "RoyalBlue", "SaddleBrown", "Salmon", "SandyBrown", "SeaGreen", "Sienna", "SkyBlue",
              "SlateBlue", "SlateGray", "SpringGreen", "SteelBlue", "Tan", "Teal", "Tomato", "Turquoise",
              "Violet", "Yellow", "YellowGreen"
            };

            Random random = new Random();
            Color = Color.FromName(colors[random.Next(colors.Count)].ToString());

        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculate whether the shape is rectangular, axis-aligned, convex or irregular
        /// </summary>
        public void DetermineShapeType()
        {
            // check if the shape is rectangular
            // rectangular shapes have exactly four edges
            if (Edges.Count == 4)
            {
                // calculate lengths of diagonals
                double a1 = Edges[1].NodeB.X - Edges[0].NodeA.X,
                       b1 = Edges[1].NodeB.Y - Edges[0].NodeA.Y,
                       a2 = Edges[1].NodeA.X - Edges[3].NodeA.X,
                       b2 = Edges[3].NodeA.Y - Edges[1].NodeA.Y;
                double diagonal1 = Math.Sqrt(a1 * a1 + b1 * b1),
                       diagonal2 = Math.Sqrt(a2 * a2 + b2 * b2);

                // if lengths of diagonals are the same, the shape is a rectangle
                if (Math.Abs(diagonal1 - diagonal2) < 0.001)
                {
                    ShapeType = ShapeType.Rectangular;
                    return;
                }
            }

            // check the angles between all edges
            List<double> angles = new List<double>();
            double sum = 0;

            for (int i = 0; i < Edges.Count; i++)
            {
                // last check is between last and first edge
                int nextIndex = i + 1;
                if (i + 1 == Edges.Count)
                    nextIndex = 0;

                // calculate angle between three points on the polygon
                Point A = new Point();
                A.X = Edges[i].NodeA.X - Edges[i].NodeB.X;
                A.Y = Edges[i].NodeA.Y - Edges[i].NodeB.Y;

                Point B = new Point();
                B.X = Edges[nextIndex].NodeB.X - Edges[nextIndex].NodeA.X;
                B.Y = Edges[nextIndex].NodeB.Y - Edges[nextIndex].NodeA.Y;

                double ALen = Math.Sqrt(Math.Pow(A.X, 2) + Math.Pow(A.Y, 2));
                double BLen = Math.Sqrt(Math.Pow(B.X, 2) + Math.Pow(B.Y, 2));

                double dotProduct = A.X * B.X + A.Y * B.Y;

                double theta = (180 / Math.PI) * Math.Acos(dotProduct / (ALen * BLen));

                angles.Add(theta);

                sum += 180 - theta;
            }

            // all angles exactly 90 degrees - the shape is axis-aligned
            if (angles.All(x => x % 90 == 0))
                ShapeType = ShapeType.AxisAligned;

            // convex shapes always have less than 180 degrees
            else if (Math.Abs(sum -  360) < 0.5)
                ShapeType = ShapeType.Convex;

            // all other shapes are irregular
            else
                ShapeType = ShapeType.Irregular;
        }

        #endregion
    }
}
