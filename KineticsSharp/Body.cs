using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineticsSharp
{
    public class Body
    {

        #region "status"
        public Vector Position
        {
            get; set;
        }
        public Vector Velocity
        {
            get; set;
        }
        public double Rotation
        {
            get; set;
        }
        public double AngularVelocity
        {
            get; set;
        }
        #endregion

        #region "attributes"
        public double MomentOfInertia
        {
            get;
        }
        public double Area
        {
            get;
        }
        public double Mass
        {
            get;
        }
        public double Density
        {
            get;
        }
        public Vector[] Margin
        {
            get;
        }
        #endregion

        public Body(Vector[] margin, double density)
        {

            // simplify computation
            Vector m0 = margin[0];
            for (int i = 0; i < margin.Length; i++)
            {
                //margin[i] += new Vector(100, 100);
                margin[i] -= m0;
            }

            double momentOfInertia = 0.0;
            double area = 0.0;
            double mass = 0.0;
            Vector areaWeightedCenter = new Vector();
            Vector center;
            for (int i = 0; i < margin.Length; i++)
            {
                int i_next = (i + 1) % margin.Length;

                // compute area
                double currentArea = Vector.CrossProduct(margin[i], margin[i_next]) / 2.0;
                area += currentArea;

                // compute center (based on m0)
                Vector currentCenter = (margin[i] + margin[i_next]) / 3.0;
                areaWeightedCenter += currentArea * currentCenter;

                // compute moment of inertia (based on m0)
                // see http://www.doc88.com/p-242584373942.html
                double currentMass = currentArea * density;
                double a_Sqr = margin[i].L2;
                double b_Sqr = margin[i_next].L2;
                double c_Sqr = (margin[i] - margin[i_next]).L2;
                momentOfInertia += ((a_Sqr + b_Sqr + c_Sqr) / 36.0 +
                                      currentCenter.L2) * currentMass; // parallel axis theorem
            }
            mass = density * area;
            center = areaWeightedCenter / area;
            momentOfInertia -= (center - margin[0]).L2 * mass; // parallel axis theorem

            // base on center
            for (int i = 0; i < margin.Length; i++)
            {
                margin[i] -= center;
            }

            Margin = margin;
            Density = density;
            if (area >= 0)
            {
                MomentOfInertia = momentOfInertia;
                Area = area;
                Mass = mass;
            }
            else
            {
                MomentOfInertia = -momentOfInertia;
                Area = -area;
                Mass = -mass;
            }

        }
    }
}