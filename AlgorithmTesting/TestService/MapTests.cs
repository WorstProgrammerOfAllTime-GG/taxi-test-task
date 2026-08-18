using DeliverySystem.Services;

namespace Testing.TestService
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void InitializeCorrectSizesAndEmptyDriverMap()
        {          
            int m = 10, n = 10;

            var map = new Map(m, n);

            Assert.That(map.M, Is.EqualTo(m));
            Assert.That(map.N, Is.EqualTo(n));
            Assert.That(map.DriversOnMap, Is.Not.Null);
            Assert.That(map.DriversOnMap, Is.Empty);
        }

    
        [TestCase(0, 0, 10, 10, true)]
        [TestCase(5, 5, 10, 10, true)]
        [TestCase(9, 9, 10, 10, true)] 
        [TestCase(-1, 5, 10, 10, false)] 
        [TestCase(5, -1, 10, 10, false)] 
        [TestCase(10, 5, 10, 10, false)] 
        [TestCase(5, 10, 10, 10, false)] 
        [TestCase(100, 100, 10, 10, false)]
        public void VerificationValidCoordinates(int x, int y, int m, int n, bool expectedResult)
        {

            var map = new Map(m, n);

            bool result = map.VerificationValidCoordinates(x, y);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}


