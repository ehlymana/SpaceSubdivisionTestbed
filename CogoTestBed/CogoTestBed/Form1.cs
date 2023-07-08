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

        HierarchyElement temporaryElement;
        Edge temporaryEdge;

        List<Point> mouseLine = new List<Point>();

        #endregion

        #region Constructor

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

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
            if (!drawing)
                return;

            drawing = false;

            // at least three edges were added to create a shape
            if (temporaryElement.Edges.Count > 2)
                shapes.Add(temporaryElement);

            temporaryElement = null;
            temporaryEdge = null;

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
            Refresh();

            // refresh information about the coordinates
            labelX.Text = "X:";
            labelY.Text = "Y:";

            temporaryElement = null;
            temporaryEdge = null;
        }

        /// <summary>
        /// Handle mouse click on the panel - draw a new line (edge)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Show information about the current coordinates of the panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Draw lines on the panel to show the desired shapes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox2.Text = "";
            comboBox2.BackColor = Color.White;

            // draw all hierarchy elements

            Pen pen;
            for (int i = 0; i < shapes.Count; i++)
            {
                pen = new Pen(shapes[i].Color, 2);
                // add shape to combo box for enabling subdivision
                comboBox2.Items.Add(shapes[i].ID + ", color: " + shapes[i].Color.ToKnownColor().ToString());
                if (i == 0)
                {
                    comboBox2.SelectedIndex = 0;
                    comboBox2.BackColor = shapes[i].Color;
                }

                // draw all edges of the element
                foreach (Edge edge in shapes[i].Edges)
                    e.Graphics.DrawLine(pen, new Point(x: edge.NodeA.X, y: edge.NodeA.Y), new Point(x: edge.NodeB.X, y: edge.NodeB.Y));
            }

            // draw unfinished edges
            pen = new Pen(Color.Black, 2);
            if (mouseLine.Count > 1)
                e.Graphics.DrawLine(pen, mouseLine[0], mouseLine[1]);

            // draw finished edges
            if (temporaryElement != null)
            {
                foreach (Edge edge in temporaryElement.Edges)
                    e.Graphics.DrawLine(pen, new Point(x: edge.NodeA.X, y: edge.NodeA.Y), new Point(x: edge.NodeB.X, y: edge.NodeB.Y));
            }
        }

        /// <summary>
        /// Finish shape automatically by adding a line connecting the first edge with the last created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoop_Click(object sender, EventArgs e)
        {
            if (temporaryElement.Edges.Count < 3)
                return;

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

        /// <summary>
        /// Manually insert new edge by using coordinates specified by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (!drawing)
                return;

            if (temporaryElement == null)
            {
                temporaryElement = new HierarchyElement();
                temporaryEdge = new Edge();
            }

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

        /// <summary>
        /// Change the color of combo box to make selection of the desired shape easier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
                comboBox2.BackColor = shapes[comboBox2.SelectedIndex].Color;
            else
            {
                comboBox2.Text = "";
                comboBox2.BackColor = Color.White;
            }
        }

        #endregion

        #region Subdivision

        /// <summary>
        /// Begin subdivision of the desired shape automatically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSubdivide_Click(object sender, EventArgs e)
        {
            // if no item is selected, do nothing
            if (comboBox2.SelectedItem == null)
                return;

            // determine which shape is selected
            string item = comboBox2.SelectedItem.ToString().Split(",")[0];
            HierarchyElement shape = shapes.Find(x => x.ID == Int32.Parse(item));

            if (shape == null)
                return;

            // determine the shape of the element
            shape.DetermineShapeType();

            richTextBox1.Text = "Input shape type: " + shape.ShapeType.ToString() + "\n";


        }

        #endregion

        #endregion

        
    }
}