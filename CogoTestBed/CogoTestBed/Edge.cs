using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSubdivisionTestbed
{
    public class Edge
    {
        #region Properties

        public Node NodeA { get; set; }

        public Node NodeB { get; set; }

        #endregion

        #region Constructor

        public Edge()
        {
            NodeA = new Node();
            NodeB = new Node();
        }

        #endregion
    }
}
