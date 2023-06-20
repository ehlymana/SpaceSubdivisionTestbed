using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogoTestBed
{
    public class Edge
    {
        public Node NodeA { get; set; }

        public Node NodeB { get; set; }

        public Edge()
        {
            NodeA = new Node();
            NodeB = new Node();
        }
    }
}
