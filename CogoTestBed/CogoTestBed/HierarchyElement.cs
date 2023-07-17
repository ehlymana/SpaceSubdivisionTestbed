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

        public List<HierarchyElement> Subdivide()
        {
            // create bounding box around the shape
            HierarchyElement boundingBox = CreateBoundingBox();

            // determine whether to cut horizontally or vertically
            int lengthX = boundingBox.Edges[0].NodeB.X - boundingBox.Edges[0].NodeA.X,
                lengthY = boundingBox.Edges[1].NodeB.Y - boundingBox.Edges[1].NodeA.Y;

            bool performCutHorizontally = lengthX > lengthY;

            // create new edge that signifies the cut
            Edge edgeCut = new Edge();

            if (performCutHorizontally)
            {
                int halfY = boundingBox.Edges[1].NodeB.Y - boundingBox.Edges[1].NodeA.Y;

                edgeCut.NodeA = new Node() { X = boundingBox.Edges[0].NodeA.X, Y = halfY };
                edgeCut.NodeB = new Node() { X = boundingBox.Edges[0].NodeB.X, Y = halfY };
            }
            else
            {
                int halfX = boundingBox.Edges[0].NodeB.X - boundingBox.Edges[0].NodeA.X;

                edgeCut.NodeA = new Node() { X = halfX, Y = boundingBox.Edges[1].NodeA.Y };
                edgeCut.NodeB = new Node() { X = halfX, Y = boundingBox.Edges[1].NodeB.Y };
            }

            List<Tuple<Edge, Node>> intersectEdges = FindEdgesThatIntersect(edgeCut);
            List<Node> nodesToAdd = new List<Node>();

            while (intersectEdges.Count > 0)
            {
                Node nodeToAdd = new Node() { X = intersectEdges[0].Item2.X, Y = intersectEdges[0].Item2.Y };

                Edge e1 = new Edge()
                {
                    NodeA = new Node() { X = intersectEdges[0].Item1.NodeA.X, Y = intersectEdges[0].Item1.NodeA.Y },
                    NodeB = nodeToAdd
                };
                Edge e2 = new Edge()
                {
                    NodeA = nodeToAdd,
                    NodeB = new Node() { X = intersectEdges[0].Item1.NodeB.X, Y = intersectEdges[0].Item1.NodeB.Y }
                };

                Edges.Remove(intersectEdges[0].Item1);
                Edges.Add(e1);
                Edges.Add(e2);

                if (nodesToAdd.Count == 0)
                    nodesToAdd.Add(nodeToAdd);

                // add the intersection edge twice, because it will belong to two shapes
                else
                {
                    Edges.Add(new Edge() { NodeA = nodesToAdd[0], NodeB = nodeToAdd });
                    Edges.Add(new Edge() { NodeA = nodeToAdd, NodeB = nodesToAdd[0] });
                    nodesToAdd.Clear();
                }
            }
            // create new set of polygons by using DFS
            List<HierarchyElement> newElements = new List<HierarchyElement>();

            return newElements;
        }

        /// <summary>
        /// Create bounding box by using the lowest and highest X and Y coordinates of the element edges
        /// </summary>
        /// <returns></returns>
        public HierarchyElement CreateBoundingBox()
        {
            Node lowerLeft = new Node()
            { X = Edges[0].NodeA.X, Y = Edges[0].NodeA.Y },
                 upperRight = new Node()
                 { X = Edges[0].NodeA.X, Y = Edges[0].NodeA.Y };

            foreach (Edge edge in Edges)
            {
                int lowerX = edge.NodeA.X, lowerY = edge.NodeA.Y,
                    upperX = edge.NodeA.X, upperY = edge.NodeA.Y;

                if (edge.NodeB.X < lowerX)
                    lowerX = edge.NodeB.X;
                if (edge.NodeB.X > upperX)
                    upperX = edge.NodeB.X;
                if (edge.NodeB.Y > lowerY)
                    lowerY = edge.NodeB.Y;
                if (edge.NodeB.Y < upperY)
                    upperY = edge.NodeB.Y;

                if (lowerX < lowerLeft.X)
                    lowerLeft.X = lowerX;
                if (lowerY > lowerLeft.Y)
                    lowerLeft.Y = lowerY;
                if (upperX > upperRight.X)
                    upperRight.X = upperX;
                if (upperY < upperRight.Y)
                    upperRight.Y = upperY;
            }

            HierarchyElement boundingBox = new HierarchyElement();

            boundingBox.Edges.Add(new Edge()
            {
                NodeA = new Node() { X = lowerLeft.X, Y = upperRight.Y },
                NodeB = new Node() { X = upperRight.X, Y = upperRight.Y }
            });

            boundingBox.Edges.Add(new Edge()
            {
                NodeA = new Node() { X = upperRight.X, Y = upperRight.Y },
                NodeB = new Node() { X = upperRight.X, Y = lowerLeft.Y }
            });

            boundingBox.Edges.Add(new Edge()
            {
                NodeA = new Node() { X = upperRight.X, Y = lowerLeft.Y },
                NodeB = new Node() { X = lowerLeft.X, Y = lowerLeft.Y }
            });

            boundingBox.Edges.Add(new Edge()
            {
                NodeA = new Node() { X = lowerLeft.X, Y = lowerLeft.Y },
                NodeB = new Node() { X = lowerLeft.X, Y = upperRight.Y }
            });

            return boundingBox;
        }

        public List<Tuple<Edge, Node>> FindEdgesThatIntersect(Edge edge)
        {
            List<Tuple<Edge, Node>> intersectEdges = new List<Tuple<Edge, Node>>();

            // parameters of the line to which the intersection edge belongs
            int k1 = int.MaxValue;
            if (edge.NodeB.X - edge.NodeA.X != 0)
                k1 = (edge.NodeB.Y - edge.NodeA.Y) / (edge.NodeB.X - edge.NodeA.X);
            int n1 = edge.NodeA.Y - k1 * edge.NodeA.X;

            // check if each edge of the shape intersects
            foreach(Edge shapeEdge in Edges)
            {
                // parameters of the line to which the current edge belongs
                int k2 = int.MaxValue;
                if (shapeEdge.NodeB.X - shapeEdge.NodeA.X != 0)
                    k2 = (shapeEdge.NodeB.Y - shapeEdge.NodeA.Y) / (shapeEdge.NodeB.X - shapeEdge.NodeA.X);
                int n2 = shapeEdge.NodeA.Y - k2 * shapeEdge.NodeA.X;

                // lines are parallel and do not intersect
                if (k1 == k2)
                    continue;

                // check if intersection happens outside of segment
                int intersectionX = (-1 * n2 + n1) / (-1 * k1 + k2),
                    intersectionY = (k2 * n1 - k1 * n2) / (-1 * k1 + k2);

                Node intersectionNode = new Node() { X = intersectionX, Y = intersectionY };

                bool intersection = CheckIfPointLiesOnSegment(shapeEdge, intersectionNode);

                if (intersection)
                    intersectEdges.Add(new Tuple<Edge, Node>(shapeEdge, intersectionNode));
            }

            return intersectEdges;
        }

        public bool CheckIfPointLiesOnSegment(Edge e, Node n)
        {
            double crossproduct = (n.Y - e.NodeA.Y) * (e.NodeB.X - e.NodeA.X) - (n.X - e.NodeA.X) * (e.NodeB.Y - e.NodeA.Y);
            if (Math.Abs(crossproduct) > 0.01)
                return false;

            double dotproduct = (n.X - e.NodeA.X) * (e.NodeB.X - e.NodeA.X) + (n.Y - e.NodeA.Y) * (e.NodeB.Y - e.NodeA.Y);
            if (dotproduct < 0)
                return false;

            double squaredlengthba = (e.NodeB.X - e.NodeA.X) * (e.NodeB.X - e.NodeA.X) + (e.NodeB.Y - e.NodeA.Y) * (e.NodeB.Y - e.NodeA.Y);
            if (dotproduct > squaredlengthba)
                return false;

            return true;
        }
        #endregion
    }
}
