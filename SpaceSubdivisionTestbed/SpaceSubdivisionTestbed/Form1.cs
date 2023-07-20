using Newtonsoft.Json;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SpaceSubdivisionTestbed
{
    public partial class Form1 : Form
    {
        #region Attributes

        bool drawing = false, id = false;

        List<HierarchyElement> shapes = new List<HierarchyElement>();

        HierarchyElement temporaryElement;
        Edge temporaryEdge;

        List<Point> mouseLine = new List<Point>();

        #endregion

        #region Constructor

        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "";
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

            toolStripStatusLabel1.Text = "Initiated drawing!";
        }

        /// <summary>
        /// Indicate that the drawing state was invalidated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";

            if (!drawing)
                return;

            // at least three edges were added to create a shape
            if (temporaryElement.Edges.Count < 3)
                toolStripStatusLabel1.Text = "Shape is not completed, it contains less than 3 edges!";

            // do not let the user finish drawing if loop is not closed
            else if (Math.Abs(temporaryElement.Edges[0].NodeA.X - temporaryElement.Edges[temporaryElement.Edges.Count - 1].NodeB.X) > 0.01 ||
                Math.Abs(temporaryElement.Edges[0].NodeA.Y - temporaryElement.Edges[temporaryElement.Edges.Count - 1].NodeB.Y) > 0.01)
                    toolStripStatusLabel1.Text = "The loop was not closed!";

            drawing = false;
            
            if (toolStripStatusLabel1.Text.Length < 1)
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

            toolStripStatusLabel1.Text = "Finished drawing!";
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

            toolStripStatusLabel1.Text = "Reset finished!";
            richTextBox1.Text = "";
        }

        /// <summary>
        /// Handle mouse click on the panel - draw a new line (edge)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Click(object sender, EventArgs e)
        {
            if (id)
            {
                Point location = panel1.PointToClient(Cursor.Position);
                HierarchyElement element = shapes.Find(s => s.RayTracing(new Node() { X = location.X, Y = location.Y }));
                if (element != null)
                    toolTip1.SetToolTip(panel1, element.ID.ToString());
                else
                    toolTip1.SetToolTip(panel1, "");

                id = false;
                toolStripStatusLabel1.Text = "";
                return;
            }
            else
                toolTip1.SetToolTip(panel1, "");

            // drawing mode not active - ignore click
            if (!drawing)
            {
                toolStripStatusLabel1.Text = "Drawing needs to be initiated first!";
                return;
            }

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
            if (id)
                toolTip1.SetToolTip(panel1, "");

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
            comboBox3.Text = "Import/export data";
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

                // draw all edges and nodes of the element
                foreach (Edge edge in shapes[i].Edges)
                {
                    e.Graphics.DrawLine(pen, new Point(x: (int)edge.NodeA.X, y: (int)edge.NodeA.Y), new Point(x: (int)edge.NodeB.X, y: (int)edge.NodeB.Y));
                    e.Graphics.DrawEllipse(new Pen(Color.Black, 2), new Rectangle((int)edge.NodeA.X, (int)edge.NodeA.Y, 2, 2));
                    e.Graphics.DrawEllipse(new Pen(Color.Black, 2), new Rectangle((int)edge.NodeB.X, (int)edge.NodeB.Y, 2, 2));
                }
            }

            // draw unfinished edges
            pen = new Pen(Color.Black, 2);
            if (mouseLine.Count > 1)
                e.Graphics.DrawLine(pen, mouseLine[0], mouseLine[1]);

            // draw finished edges
            if (temporaryElement != null)
            {
                foreach (Edge edge in temporaryElement.Edges)
                    e.Graphics.DrawLine(pen, new Point(x: (int)edge.NodeA.X, y: (int)edge.NodeA.Y), new Point(x: (int)edge.NodeB.X, y: (int)edge.NodeB.Y));
            }

            toolStripStatusLabel1.Text = "";
        }

        /// <summary>
        /// Finish shape automatically by adding a line connecting the first edge with the last created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoop_Click(object sender, EventArgs e)
        {
            // cannot create element with one edge + closed loop (total of 2 edges)
            if (temporaryElement.Edges.Count < 2)
            {
                toolStripStatusLabel1.Text = "Element needs to have at least two edges!";
                return;
            }

            // put starting point of the shape as the end point of the current edge
            Node endPoint = temporaryElement.Edges[0].NodeA;
            temporaryEdge.NodeB = endPoint;

            // add new edge
            temporaryElement.Edges.Add(temporaryEdge);

            // finish drawing
            buttonStop_Click(sender, e);

            toolStripStatusLabel1.Text = "Finished drawing automatically!";
        }

        /// <summary>
        /// Enable identifying shapes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonID_Click(object sender, EventArgs e)
        {
            id = true;
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
                mouseLine.Add(new Point((int)node.X, (int)node.Y));
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
                mouseLine.Add(new Point((int)node.X, (int)node.Y));
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

            List<HierarchyElement> newShapes = shape.Subdivide();

            richTextBox1.Text += "Subdivision complete! Total new shapes: " + newShapes.Count + "\n";

            // remove the old shape from hierarchy elements
            shapes.Remove(shape);

            // add new shapes to hierarchy elements
            foreach (HierarchyElement newShape in newShapes)
                shapes.Add(newShape);

            // refresh the panel
            Refresh();
        }

        #endregion

        #region Import/Export data

        /// <summary>
        /// Perform data export or import based on selected combo box item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == -1)
            {
                comboBox3.Text = "Import/export data";
                return;
            }
            else if (comboBox3.SelectedIndex == 0)
                ImportData();
            else
                ExportData();

            comboBox3.Text = "Import/export data";
        }

        /// <summary>
        /// Import data from existing JSON file
        /// </summary>
        public void ImportData()
        {
            // open existing file
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "json files (*.json)|*.json";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            string import = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetFullPath(openFileDialog1.FileName);
                using (StreamReader reader = new StreamReader(path))
                    import = reader.ReadToEnd();

                // import data from file
                shapes = JsonConvert.DeserializeObject<List<HierarchyElement>>(import);
                Refresh();

                toolStripStatusLabel1.Text = "Import successfully completed!";
            }

            else
                toolStripStatusLabel1.Text = "Import failed!";

            comboBox3.Text = "Import/export data";
        }

        /// <summary>
        /// Export data to new JSON file
        /// </summary>
        public void ExportData()
        {
            // form the contents of the export
            string export = JsonConvert.SerializeObject(shapes);

            // save the data to a file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "json files (*.json)|*.json";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetFullPath(saveFileDialog1.FileName);
                using (StreamWriter writer = new StreamWriter(path))
                    writer.Write(export);

                toolStripStatusLabel1.Text = "Export completed successfully!";
            }

            else
                toolStripStatusLabel1.Text = "Export failed!";

            comboBox3.Text = "Import/export data";
        }

        #endregion

        #endregion
    }
}