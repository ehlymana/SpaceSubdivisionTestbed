using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CogoTestBed
{
    public partial class Form1 : Form
    {
        #region Attributes

        bool drawing = false;

        List<HierarchyElement> shapes = new List<HierarchyElement>();

        HierarchyElement temporaryElement = new HierarchyElement();
        Edge temporaryEdge = new Edge();

        List<Point> mouseLine = new List<Point>();

        #endregion

        #region Methods

        public Form1()
        {
            InitializeComponent();
            
        }

        #region Drawing (graphical)

        /// <summary>
        /// Indicate that the drawing state was initiated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDraw_Click(object sender, EventArgs e)
        {
            drawing = true;

            // initialize new temporary element
            temporaryElement = new HierarchyElement();
            temporaryEdge = new Edge();

            // refresh information about the coordinates
            labelX.Text = "X:";
            labelY.Text = "Y:";
        }

        /// <summary>
        /// Indicate that the drawing state was invalidated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            drawing = false;

            // at least three edges were added to create a shape
            if (temporaryElement.Edges.Count > 2)
                shapes.Add(temporaryElement);

            // delete temporary and unfinished edges
            mouseLine.Clear();

            // redraw all shapes
            Refresh();

            // refresh information about the coordinates
            labelX.Text = "X:";
            labelY.Text = "Y:";
        }

        /// <summary>
        /// Delete all objects from the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            // delete all hierarchy elements
            shapes.Clear();
            mouseLine.Clear();

            // delete all shapes which were drawn
            panel1.Refresh();

            // refresh information about the coordinates
            labelX.Text = "X:";
            labelY.Text = "Y:";
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            // drawing mode not active - ignore click
            if (!drawing)
                return;

            // drawing mode active - capture the coordinates of the clicked point
            Point point = panel1.PointToClient(Cursor.Position);

            Node node = new Node()
            { 
                X = point.X,
                Y = point.Y 
            };

            // show information about the coordinates on the UI
            labelX.Text = "X: " + point.X;
            labelY.Text = "Y: " + point.Y;

            // this is the first node - add it to the first edge
            if (temporaryEdge.NodeA.X == 0 && temporaryEdge.NodeA.Y == 0)
            {
                temporaryEdge.NodeA = node;

                // add information for drawing the line on the panel
                mouseLine.Add(point);
            }

            else
            {
                // edge contains both nodes - add it to the hierarchy element
                temporaryEdge.NodeB = node;
                temporaryElement.Edges.Add(temporaryEdge);

                // end node is the beginning node of the next edge
                temporaryEdge = new Edge();
                temporaryEdge.NodeA = node;

                // reset the information for drawing the line on the panel
                mouseLine.Clear();
                mouseLine.Add(point);
            }

            // redraw all shapes
            Refresh();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing && mouseLine.Count > 0)
            {
                // show edge moving with the mouse
                if (mouseLine.Count == 1)
                    mouseLine.Add(e.Location);
                else
                    mouseLine[1] = e.Location;

                // show information about the coordinates on the UI
                labelX.Text = "X: " + e.Location.X;
                labelY.Text = "Y: " + e.Location.Y;

                // redraw all shapes
                Refresh();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 1);
            // draw all hierarchy elements
            foreach (HierarchyElement shape in shapes)
            {
                // draw all edges of the element
                foreach (Edge edge in shape.Edges)
                    e.Graphics.DrawLine(pen, new Point(x: edge.NodeA.X, y: edge.NodeA.Y), new Point(x: edge.NodeB.X, y: edge.NodeB.Y));
            }

            // draw unfinished lines
            if (mouseLine.Count > 1)
                e.Graphics.DrawLine(pen, mouseLine[0], mouseLine[1]);

            foreach (Edge edge in temporaryElement.Edges)
                e.Graphics.DrawLine(pen, new Point(x: edge.NodeA.X, y: edge.NodeA.Y), new Point(x: edge.NodeB.X, y: edge.NodeB.Y));
        }

        private void buttonLoop_Click(object sender, EventArgs e)
        {
            // put starting point of the shape as the end point of the current edge
            Node endPoint = temporaryElement.Edges[0].NodeA;
            temporaryEdge.NodeB = endPoint;

            // add new edge
            temporaryElement.Edges.Add(temporaryEdge);

            // finish drawing
            buttonStop_Click(sender, e);
        }

        #endregion

        #region Drawing (manual)

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            Node node = new Node()
            {
                X = (int)numericUpDown1.Value,
                Y = (int)numericUpDown2.Value
            };
            // this is the first node - add it to the first edge
            if (temporaryEdge.NodeA.X == 0 && temporaryEdge.NodeA.Y == 0)
            {
                temporaryEdge.NodeA = node;

                // add information for drawing the line on the panel
                mouseLine.Add(new Point(node.X, node.Y));
            }

            else
            {
                // edge contains both nodes - add it to the hierarchy element
                temporaryEdge.NodeB = node;
                temporaryElement.Edges.Add(temporaryEdge);

                // end node is the beginning node of the next edge
                temporaryEdge = new Edge();
                temporaryEdge.NodeA = node;

                // reset the information for drawing the line on the panel
                mouseLine.Clear();
                mouseLine.Add(new Point(node.X, node.Y));
            }

            // redraw all shapes
            Refresh();
        }

        #endregion

        #region Subdivision

        private void buttonSubdivide_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion
    }
}