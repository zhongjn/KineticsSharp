using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KineticsSharp.Threading;
using System.Threading;
namespace KineticsSharp.Test
{
    [TestClass]
    public class UnitTest_Threading_Legion
    {
        [TestMethod]
        public void Legion_WorkersCount_Correct()
        {
            int s = 0;
            const int count = 4;
            var legion = new Legion(count, () =>
            {
                Interlocked.Increment(ref s);
            });
            legion.Do();
            Assert.AreEqual(count, s);
        }
    }
}
