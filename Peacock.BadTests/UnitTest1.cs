using System.Diagnostics;
using NUnit.Framework;

namespace Peacock.BadTests
{
    [TestFixture]
    public class When_requesting_a_movie
    {
        [OneTimeSetUp]
        public void Init()
        {

        }

        [Test]
        public void Then_X_is_mapped()
        {
            Debug.WriteLine("X is mapped");
        }

        [Test]
        public void Then_Y_is_mapped()
        {
            Debug.WriteLine("Y is mapped");
        }
    }
}
