using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogoTestBed
{
    public class HierarchyElement
    {
        public List<Edge> Edges { get; set; }

        public List<HierarchyElement> Holes { get; set; }

        public HierarchyElement? Parent { get; set; }

        public List<HierarchyElement> Siblings { get; set; }

        public HierarchyElement()
        {
            Edges = new List<Edge>();
            Holes = new List<HierarchyElement>();
            Siblings = new List<HierarchyElement>();
        }
    }
}
