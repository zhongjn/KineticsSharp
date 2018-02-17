using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KineticsSharp.Test
{
    [TestClass]
    public class UnitTest_Body
    {
        void IsEqualWithEpsilon(double expected, double actual, double epsilon = 1e-6)
        {
            Assert.IsTrue(expected + epsilon >= actual && expected - epsilon <= actual);
        }

        static Vector[] squareMargin = { new Vector(1, 1), new Vector(1, -1), new Vector(-1, -1), new Vector(-1, 1) };
        static Body squareBody = new Body(squareMargin, 2);

        [TestMethod]
        public void SquareBody_Area_Correct()
        {
            IsEqualWithEpsilon(4, squareBody.Area);
        }

        [TestMethod]
        public void SquareBody_Mass_Correct()
        {
            IsEqualWithEpsilon(8, squareBody.Mass);
        }

        [TestMethod]
        public void SquareBody_MomentOfInertia_Correct()
        {
            IsEqualWithEpsilon(64.0 / 12.0, squareBody.MomentOfInertia); // I=m(a^2+b^2)/12
        }
    }
}
