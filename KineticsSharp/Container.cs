using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineticsSharp
{
    class Container
    {
        LinkedList<Body> bodies;
        void AddBody(Body body)
        {
            bodies.AddLast(body);
        }
        void Update()
        {
            
        }
    }
}
