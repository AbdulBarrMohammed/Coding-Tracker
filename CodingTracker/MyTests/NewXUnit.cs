using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingTracker.Controller;
using Xunit;

namespace CodingTracker
{
    public class NewXUnit
    {
        [Fact]
        public void isFormattedCorrectlyTest()
        {
            CodingController controller = new CodingController();
            var result = controller.isFormattedCorrectly("12:00");
            Assert.True(result);
        }
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}
