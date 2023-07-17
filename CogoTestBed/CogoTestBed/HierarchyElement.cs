using System;
using System.Collections.Generic;
using System.Globalization;
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

        public Color Color { get; set; }

        #endregion

        #region Constructor

        public HierarchyElement()
        {
            ID = counter;
            counter++;
            Edges = new List<Edge>();

            List<string> colors = new List<string>()
            { "Aqua", "Aquamarine", "Black", "Blue", "BlueViolet", "Brown", "BurlyWood", "CadetBlue",
              "Chartreuse", "Chocolate", "Coral", "CornflowerBlue", "Crimson", "Cyan", "DarkBlue",
              "DarkCyan", "DarkGoldenrod", "DarkGreen", "DarkOliveGreen", "DarkOrange",
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
                Node A = new Node();
                A.X = Edges[i].NodeA.X - Edges[i].NodeB.X;
                A.Y = Edges[i].NodeA.Y - Edges[i].NodeB.Y;

                Node B = new Node();
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

        /// <summary>
        /// Subdivide shape to multiple new shapes and return them as result
        /// </summary>
        /// <returns></returns>
        public List<HierarchyElement> Subdivide()
        {
            #region Cutting

            // create bounding box around the shape
            HierarchyElement boundingBox = CreateBoundingBox();

            // determine whether to cut horizontally or vertically
            double lengthX = boundingBox.Edges[0].NodeB.X - boundingBox.Edges[0].NodeA.X,
                   lengthY = boundingBox.Edges[1].NodeB.Y - boundingBox.Edges[1].NodeA.Y;

            bool performCutHorizontally = lengthX < lengthY;

            // create new edge that signifies the cut
            Edge edgeCut = new Edge();

            if (performCutHorizontally)
            {
                double halfY = (boundingBox.Edges[1].NodeB.Y + boundingBox.Edges[1].NodeA.Y) / 2;

                edgeCut.NodeA = new Node() { X = boundingBox.Edges[0].NodeA.X, Y = halfY };
                edgeCut.NodeB = new Node() { X = boundingBox.Edges[0].NodeB.X, Y = halfY };
            }
            else
            {
                double halfX = (boundingBox.Edges[0].NodeB.X + boundingBox.Edges[0].NodeA.X) / 2;

                edgeCut.NodeA = new Node() { X = halfX, Y = boundingBox.Edges[1].NodeA.Y };
                edgeCut.NodeB = new Node() { X = halfX, Y = boundingBox.Edges[1].NodeB.Y };
            }

            #endregion

            #region Intersections

            List<Tuple<Edge, Node>> intersectEdges = FindEdgesThatIntersect(edgeCut);
            List<Node> nodesToAdd = new List<Node>();

            while (intersectEdges.Count > 0)
            {
                // the new edge contains the intersection point
                Node nodeToAdd = new Node() { X = intersectEdges[0].Item2.X, Y = intersectEdges[0].Item2.Y };

                // divide curent edge into two new edges
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

                // remove the old edge (it was subdivided)
                Edges.Remove(intersectEdges[0].Item1);

                // add subdivided edges if they are not a point (length 0)
                if (e1.NodeA.X != e1.NodeB.X ||e1.NodeA.Y != e1.NodeB.Y)
                    Edges.Add(e1);
                if (e2.NodeA.X != e2.NodeB.X || e2.NodeA.Y != e2.NodeB.Y)
                    Edges.Add(e2);

                // if this is the first intersection point,
                // wait for the next point for creating the intersection edge
                if (nodesToAdd.Count == 0)
                    nodesToAdd.Add(nodeToAdd);

                // add the intersection edge to the list of edges
                else
                {
                    Edges.Add(new Edge() { NodeA = nodesToAdd[0], NodeB = nodeToAdd });
                    Edges.Add(new Edge() { NodeA = nodeToAdd, NodeB = nodesToAdd[0] });
                    nodesToAdd.Clear();
                }

                // remove the first intersection edge from the list of intersections
                intersectEdges.RemoveAt(0);
            }

            #endregion

            #region DFS

            // calculate the number of different points in the polygon
            List<Node> allNodes = new List<Node>();

            foreach (Edge e in Edges)
            {
                if (allNodes.FindIndex(node => node.X == e.NodeA.X && node.Y == e.NodeA.Y) == -1)
                    allNodes.Add(e.NodeA);
                if (allNodes.FindIndex(node => node.X == e.NodeB.X && node.Y == e.NodeB.Y) == -1)
                    allNodes.Add(e.NodeB);
            }

            // create undirected graph adjacency matrix
            bool[,] adjacencyMatrix = new bool[allNodes.Count, allNodes.Count];

            foreach (Edge e in Edges)
            {
                int indexA = allNodes.FindIndex(node => node.X == e.NodeA.X && node.Y == e.NodeA.Y),
                    indexB = allNodes.FindIndex(node => node.X == e.NodeB.X && node.Y == e.NodeB.Y);

                adjacencyMatrix[indexA, indexB] = true;
                adjacencyMatrix[indexB, indexA] = true;
            }

            // perform DFS to find all shapes

            List<Tuple<List<Node>, double>> paths = new List<Tuple<List<Node>, double>>();

            int counter = 0;
            while (counter < allNodes.Count * allNodes.Count)
            {
                List<Node> path = new List<Node>();
                int i = 0, j = 0;
                counter = 0;
                double length = 0;
                while (i < allNodes.Count)
                {
                    // no neighborhood - skip element
                    if (!adjacencyMatrix[i, j])
                    {
                        counter++;
                        if (j < allNodes.Count - 1)
                            j++;
                        else
                        {
                            i++;
                            j = 0;
                        }
                    }
                    // first nodes to be added to the path - add both
                    else if (path.Count == 0)
                    {
                        path.Add(allNodes[i]);
                        path.Add(allNodes[j]);

                        length += Math.Sqrt(Math.Pow(allNodes[i].X - allNodes[j].X, 2) + Math.Pow(allNodes[i].Y - allNodes[j].Y, 2));

                        // delete adjacency
                        adjacencyMatrix[i, j] = false;

                        i = j;
                        j = 0;
                    }
                    // not the first node to be added to the path
                    // don't add same node or if returning to the previous node (e.g. AB - BA)
                    else if (i == j || (path.Count > 1 && allNodes[j] == path[path.Count - 2]))
                    {
                        counter++;
                        if (j < allNodes.Count - 1)
                            j++;
                        else
                        {
                            i++;
                            j = 0;
                        }
                    }
                    // found new adjacency - add it to the path
                    else
                    {
                        path.Add(allNodes[j]);

                        length += Math.Sqrt(Math.Pow(path[path.Count - 2].X - allNodes[j].X, 2) + Math.Pow(path[path.Count - 2].Y - allNodes[j].Y, 2));

                        // delete adjacency
                        adjacencyMatrix[i, j] = false;

                        i = j;
                        j = 0;
                    }
                    // if the first and last node of path are the same, path is complete
                    if (path.Count > 2 && path[0].X == path[path.Count - 1].X && path[0].Y == path[path.Count - 1].Y)
                        break;
                }
                if (path.Count > 2)
                    paths.Add(new Tuple<List<Node>, double>(path, length));
            }

            #endregion

            #region Final paths

            // delete redundant paths

            List<int> pathsToDelete = new List<int>();

            for (int i = 0; i < paths.Count; i++)
            {
                for (int j = i + 1; j < paths.Count; j++)
                {
                    // skip same paths
                    if (i == j)
                        continue;
                    int numberOfNodes = paths[i].Item1.Count;
                    for (int k = 0; k < paths[j].Item1.Count; k++)
                    {
                        // path contains node
                        if (paths[i].Item1.FindIndex(node => node.X == paths[j].Item1[k].X && node.Y == paths[j].Item1[k].Y) > -1)
                            numberOfNodes--;
                        // all nodes the same - this path needs to be deleted
                        if (numberOfNodes == 0)
                            break;
                    }

                    // add path for deleting if it is redundant
                    // delete the longer path
                    if (numberOfNodes == 0 && paths[i].Item1.Count < paths[j].Item1.Count)
                        pathsToDelete.Add(j);
                    else if (numberOfNodes == 0)
                        pathsToDelete.Add(i);
                }
            }

            foreach (int index in pathsToDelete)
                paths.RemoveAt(index);

            #endregion

            #region New polygons

            // sort paths by length increasingly
            paths = paths.OrderBy(p => p.Item2).ToList();

            // formulate new elements
            List<HierarchyElement> newElements = new List<HierarchyElement>();

            foreach (Tuple<List<Node>, double> path in paths)
            {
                HierarchyElement newShape = new HierarchyElement();
                for (int i = 0; i < path.Item1.Count - 1; i++)
                {
                    newShape.Edges.Add(new Edge()
                    {
                        NodeA = new Node() { X = path.Item1[i].X, Y = path.Item1[i].Y },
                        NodeB = new Node() { X = path.Item1[i + 1].X, Y = path.Item1[i + 1].Y }
                    });
                }
                newElements.Add(newShape);
            }

            return newElements;

            #endregion
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
                double lowerX = edge.NodeA.X, lowerY = edge.NodeA.Y,
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

        /// <summary>
        /// Find all edges which have intersection with the intersection edge
        /// This check is necessary for performing subdivision
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public List<Tuple<Edge, Node>> FindEdgesThatIntersect(Edge edge)
        {
            List<Tuple<Edge, Node>> intersectEdges = new List<Tuple<Edge, Node>>();

            // parameters of the line to which the intersection edge belongs
            double k1 = int.MaxValue;
            bool verticalLine = edge.NodeB.X - edge.NodeA.X == 0;
            if (!verticalLine)
                k1 = (edge.NodeB.Y - edge.NodeA.Y) / (edge.NodeB.X - edge.NodeA.X);
            double n1 = edge.NodeA.Y - k1 * edge.NodeA.X;

            // check if each edge of the shape intersects
            foreach(Edge shapeEdge in Edges)
            {
                // parameters of the line to which the current edge belongs
                double k2 = int.MaxValue;
                if (shapeEdge.NodeB.X - shapeEdge.NodeA.X != 0)
                    k2 = (shapeEdge.NodeB.Y - shapeEdge.NodeA.Y) / (shapeEdge.NodeB.X - shapeEdge.NodeA.X);
                double n2 = shapeEdge.NodeA.Y - k2 * shapeEdge.NodeA.X;

                // lines are parallel and do not intersect
                if (Math.Abs(k1 - k2) < 0.01)
                    continue;

                // check if intersection happens outside of segment
                double intersectionX = (-1 * n2 + n1) / (-1 * k1 + k2),
                       intersectionY = (k2 * n1 - k1 * n2) / (-1 * k1 + k2);

                Node intersectionNode;
                if (!verticalLine)
                    intersectionNode = new Node() { X = intersectionX, Y = intersectionY };
                else
                    intersectionNode = new Node() { X = edge.NodeA.X, Y = k2 * edge.NodeA.X + n2 };

                bool intersection = CheckIfPointLiesOnSegment(shapeEdge, intersectionNode);

                if (intersection)
                    intersectEdges.Add(new Tuple<Edge, Node>(shapeEdge, intersectionNode));
            }

            return intersectEdges;
        }

        /// <summary>
        /// Check whether the given point lies on segment
        /// This is an additional check if the intersection can be applied for subdivision
        /// </summary>
        /// <param name="e"></param>
        /// <param name="n"></param>
        /// <returns></returns>
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
