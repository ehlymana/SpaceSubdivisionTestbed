﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSubdivisionTestbed
{
    public class HierarchyElement
    {
        #region Attributes

        static int counter = 1;

        #endregion

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
              "Gold", "Goldenrod", "Gray", "Green", "HotPink", "IndianRed", "Indigo", "LawnGreen",
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
            // check the angles between all edges
            List<double> angles = new List<double>();

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

                // calculate angle
                double dotProduct = A.X * B.X + A.Y * B.Y;
                double p = Math.Acos(dotProduct / (ALen * BLen));
                double angle = (180 / Math.PI) * Math.Acos(dotProduct / (ALen * BLen));

                // get direction of angle so that angle can be inverted if necessary
                double d = direction(Edges[i].NodeA.X, Edges[i].NodeA.Y, Edges[i].NodeB.X, Edges[i].NodeB.Y, Edges[nextIndex].NodeB.X, Edges[nextIndex].NodeB.Y);
                
                if (d > 0)
                    angle = 360 - angle;

                angles.Add(angle);
            }

            // all angles 90 or 180 degrees - the shape is rectangular
            if (angles.All(x => Math.Abs(x - 90) < 0.01 || Math.Abs(x - 180) < 0.01))
                ShapeType = ShapeType.Rectangular;

            // all angles divisible by 90 degrees - the shape is axis-aligned
            else if (angles.All(x => Math.Round(x) % 90 == 0))
                ShapeType = ShapeType.AxisAligned;

            // all angles at max 180 degrees - the shape is convex
            else if (angles.All(x => x <= 180))
                ShapeType = ShapeType.Convex;

            // all other shapes are irregular
            else
                ShapeType = ShapeType.Irregular;
        }

        /// <summary>
        /// Helper function for determining the direction of angle between two edges of a polygon
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns></returns>
        public double direction(double x1, double y1, double x2, double y2, double x3, double y3)
        {

            double d = ((x2 - x1) * (y3 - y1)) - ((y2 - y1) * (x3 - x1));

            return d;
        }

        /// <summary>
        /// Subdivide shape to multiple new shapes and return them as result
        /// </summary>
        /// <returns></returns>
        public List<HierarchyElement>? Subdivide()
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
            List<Edge> newEdges = new List<Edge>();
            List<Node> newNodes = new List<Node>();
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

                bool e1Exists = Edges.FindIndex(e => Math.Abs(e.NodeA.X - e1.NodeA.X) < 0.01 && Math.Abs(e.NodeA.Y - e1.NodeA.Y) < 0.01 &&
                                                     Math.Abs(e.NodeB.X - e1.NodeB.X) < 0.01 && Math.Abs(e.NodeB.Y - e1.NodeB.Y) < 0.01) > -1,
                     e2Exists = Edges.FindIndex(e => Math.Abs(e.NodeA.X - e2.NodeA.X) < 0.01 && Math.Abs(e.NodeA.Y - e2.NodeA.Y) < 0.01 &&
                                                     Math.Abs(e.NodeB.X - e2.NodeB.X) < 0.01 && Math.Abs(e.NodeB.Y - e2.NodeB.Y) < 0.01) > -1;

                // add subdivided edges if they are not a point (length 0)
                if ((Math.Abs(e1.NodeA.X - e1.NodeB.X) > 0.01 || Math.Abs(e1.NodeA.Y - e1.NodeB.Y) > 0.01 ) && !e1Exists)
                    Edges.Add(e1);
                if ((Math.Abs(e2.NodeA.X - e2.NodeB.X) > 0.01 || Math.Abs(e2.NodeA.Y - e2.NodeB.Y) > 0.01) && !e2Exists)
                    Edges.Add(e2);

                if (newNodes.FindIndex(n => Math.Abs(n.X - nodeToAdd.X) < 0.01 && Math.Abs(n.Y - nodeToAdd.Y) < 0.01) < 0)
                    newNodes.Add(nodeToAdd);

                // remove the first intersection edge from the list of intersections
                intersectEdges.RemoveAt(0);
            }

            // sort new nodes by the corresponding coordinate
            if (performCutHorizontally)
                newNodes = newNodes.OrderBy(n => n.X).ToList();
            else
                newNodes = newNodes.OrderBy(n => n.Y).ToList();

            // add new edges for each cut in the polygon
            for (int i = 0; i < newNodes.Count; i += 2)
            {
                newEdges.Add(new Edge() { NodeA = newNodes[i], NodeB = newNodes[i + 1] });
                newEdges.Add(new Edge() { NodeA = newNodes[i + 1], NodeB = newNodes[i] });
            }

            #endregion

            #region Polygon subdivision

            List<HierarchyElement> newElements = new List<HierarchyElement>();

            while (Edges.Count > 0)
            {
                List<Edge> newElement = new List<Edge>();
                newElement.Add(Edges[0]);
                Edges.RemoveAt(0);
                bool usedNewEdge = false;
                do
                {
                    // search for edge which appends to the last one
                    Edge nextEdge;

                    if (newNodes.FindIndex(n => Math.Abs(n.X - newElement[newElement.Count - 1].NodeB.X) < 0.01 &&
                                                Math.Abs(n.Y - newElement[newElement.Count - 1].NodeB.Y) < 0.01) < 0 ||
                                                usedNewEdge)
                    {
                        nextEdge = Edges.Find(el => Math.Abs(el.NodeA.X - newElement[newElement.Count - 1].NodeB.X) < 0.01 &&
                                                    Math.Abs(el.NodeA.Y - newElement[newElement.Count - 1].NodeB.Y) < 0.01)
                                              ?? new Edge();
                        Edges.Remove(nextEdge);
                        usedNewEdge = false;
                    }
                    else
                    {
                        nextEdge = newEdges.Find(el => Math.Abs(el.NodeA.X - newElement[newElement.Count - 1].NodeB.X) < 0.01 &&
                                                       Math.Abs(el.NodeA.Y - newElement[newElement.Count - 1].NodeB.Y) < 0.01)
                                                ?? new Edge();
                        newEdges.Remove(nextEdge);
                        usedNewEdge = true;
                    }

                    newElement.Add(nextEdge);
                }
                while (Math.Abs(newElement[0].NodeA.X - newElement[newElement.Count - 1].NodeB.X) > 0.01 ||
                       Math.Abs(newElement[0].NodeA.Y - newElement[newElement.Count - 1].NodeB.Y) > 0.01);

                HierarchyElement element = new HierarchyElement();
                element.Edges = newElement;

                newElements.Add(element);
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
            bool verticalLine = Math.Abs(edge.NodeB.X - edge.NodeA.X) < 0.01;
            if (!verticalLine)
                k1 = (edge.NodeB.Y - edge.NodeA.Y) / (edge.NodeB.X - edge.NodeA.X);
            double n1 = edge.NodeA.Y - k1 * edge.NodeA.X;
            bool changeHappened = false;

            // possible that this process needs to be done multiple times due to same lines
            do
            {
                intersectEdges.Clear();
                changeHappened = false;
                // check if each edge of the shape intersects
                foreach (Edge shapeEdge in Edges)
                {
                    // parameters of the line to which the current edge belongs
                    double k2 = int.MaxValue;
                    if (Math.Abs(shapeEdge.NodeB.X - shapeEdge.NodeA.X) > 0.01)
                        k2 = (shapeEdge.NodeB.Y - shapeEdge.NodeA.Y) / (shapeEdge.NodeB.X - shapeEdge.NodeA.X);
                    double n2 = shapeEdge.NodeA.Y - k2 * shapeEdge.NodeA.X;

                    // lines are the same - special case
                    if (Math.Abs(k1 - k2) < 0.01 && (Math.Abs(n1 - n2) < 0.01 || (k1 == int.MaxValue && Math.Abs(shapeEdge.NodeA.X - edge.NodeA.X) < 0.01)))
                    {
                        // find the further cut edge point - that one will not be modified
                        Node nonIntersectPoint = edge.NodeA;
                        if ((Math.Abs(shapeEdge.NodeA.X - nonIntersectPoint.X) < 0.01 && Math.Abs(shapeEdge.NodeA.Y - nonIntersectPoint.Y) < 0.01) ||
                            (Math.Abs(shapeEdge.NodeB.X - nonIntersectPoint.X) < 0.01 && Math.Abs(shapeEdge.NodeB.Y - nonIntersectPoint.Y) < 0.01))
                        {
                            nonIntersectPoint = edge.NodeB;
                        }

                        // find the closer edge point - that one will be replaced
                        Node closerIntersectPoint = shapeEdge.NodeA;
                        double distanceA = Math.Sqrt(Math.Pow((shapeEdge.NodeA.X - nonIntersectPoint.X), 2) + Math.Pow((shapeEdge.NodeA.Y - nonIntersectPoint.Y), 2)),
                               distanceB = Math.Sqrt(Math.Pow((shapeEdge.NodeB.X - nonIntersectPoint.X), 2) + Math.Pow((shapeEdge.NodeB.Y - nonIntersectPoint.Y), 2));

                        if (distanceB < distanceA)
                            closerIntersectPoint = shapeEdge.NodeB;

                        if ((edge.NodeA == nonIntersectPoint && edge.NodeB == closerIntersectPoint) ||
                            (edge.NodeB == nonIntersectPoint && edge.NodeA == closerIntersectPoint))
                            continue;

                        edge.NodeA = nonIntersectPoint;
                        edge.NodeB = closerIntersectPoint;

                        changeHappened = true;
                        break;
                    }

                    // lines are parallel and do not intersect
                    else if (Math.Abs(k1 - k2) < 0.01)
                        continue;

                    // check if intersection happens outside of segment
                    double intersectionX = (-1 * n2 + n1) / (-1 * k1 + k2),
                           intersectionY = (k2 * n1 - k1 * n2) / (-1 * k1 + k2);

                    Node intersectionNode;
                    if (!verticalLine)
                        intersectionNode = new Node() { X = intersectionX, Y = intersectionY };
                    else
                        intersectionNode = new Node() { X = edge.NodeA.X, Y = k2 * edge.NodeA.X + n2 };

                    bool intersection = CheckIfPointLiesOnSegment(shapeEdge, intersectionNode) && CheckIfPointLiesOnSegment(edge, intersectionNode);

                    if (intersection)
                        intersectEdges.Add(new Tuple<Edge, Node>(shapeEdge, intersectionNode));
                }
            }
            while (changeHappened);
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
            if (dotproduct < 0 && Math.Abs(dotproduct) > 0.01)
                return false;

            double squaredlengthba = (e.NodeB.X - e.NodeA.X) * (e.NodeB.X - e.NodeA.X) + (e.NodeB.Y - e.NodeA.Y) * (e.NodeB.Y - e.NodeA.Y);
            if (dotproduct > squaredlengthba && Math.Abs(dotproduct - squaredlengthba) > 0.01)
                return false;

            return true;
        }
        
        /// <summary>
        /// Perform ray-tracing to check if point is inside of polygon
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool RayTracing(Node point)
        {
            HierarchyElement boundingBox = CreateBoundingBox();

            // no edge can be drawn, because the end of the bounding box is before the point
            if (boundingBox.Edges[0].NodeB.X < point.X)
                return false;

            Edge edge = new Edge()
            {
                NodeA = point,
                NodeB = new Node() { X = boundingBox.Edges[0].NodeB.X, Y = point.Y }
            };

            List<Tuple<Edge, Node>> intersections = FindEdgesThatIntersect(edge);

            return intersections.Count % 2 == 1;
        }

        /// <summary>
        /// Calculate area of polygon using the sum of cross-products of edge segments
        /// </summary>
        /// <returns></returns>
        public double DetermineArea()
        {
            double area = 0;
            foreach (Edge e in Edges)
            {
                area += e.NodeA.X * e.NodeB.Y - e.NodeB.X * e.NodeA.Y;
            }

            area = 0.5 * Math.Abs(area);
            return area;
        }

        #endregion
    }
}
