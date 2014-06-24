using Contracts;
using Game;
using Moq;
using NUnit.Framework;

namespace GameTest
{
    [TestFixture]
    public class CalendarShould
    {
        private Mock<IGame> GameMock;

        [SetUp]
        public void Init()
        {
            GameMock = new Mock<IGame>();
        }

        [Test]
        public void OnNextDayReturnDay()
        {
            var calendar = new Calendar(GameMock.Object, 25);
            calendar.NextDay();

            Assert.AreEqual(calendar.DayFromStart, 26);
        }

        [Test] 
        public void FlipYearOnEvery360Days()
        {
            var calendar = new Calendar(GameMock.Object, 360);
            Assert.AreEqual(calendar.Year,Calendar.StartYear);
            calendar.NextDay();
            Assert.AreEqual(calendar.Year, Calendar.StartYear + 1);
            Assert.AreEqual(calendar.DayInYear,1);
        }

        [Test()]
        [TestCase(25,25)]
        [TestCase(361,1)]
        public void ReturnCorrectNumberOfDayInYear(int day, int dayinyear)
        {
            var calendar = new Calendar(GameMock.Object, day); 
            
            Assert.AreEqual(calendar.DayInYear,dayinyear);
        }

        [Test]
        public void MoveAllMoonsPositions()
        {
            var calendar = new Calendar(GameMock.Object);
            var moonPosition = (calendar.Moons[0] as Moon).Position;
            calendar.NextDay();
            Assert.AreNotEqual((calendar.Moons[0] as Moon).Position, moonPosition);
        }

        [Test]
        public void GetSpecialDay()
        {
            var calendar = new Calendar(GameMock.Object);

            bool specialDay = false;
            for (int i = 1; i < 19; i++)
            {
                calendar.NextDay();
                if ((calendar.Today as Date).SpecialDay) specialDay = true;
            }

            Assert.AreEqual(true, specialDay);
        }

    }
}
