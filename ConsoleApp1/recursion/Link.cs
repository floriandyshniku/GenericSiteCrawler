using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.recursion
{
    public class Link
    {
        public string Name { get; set; }
        public HashSet<Link> Children { get; set; }

        public Link() { }

        public Link(string name)
        {
            Name = name;
            Children = new HashSet<Link>();
        }
    }
}
