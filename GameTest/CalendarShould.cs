using Game;
using NUnit.Framework;

namespace GameTest
{
    [TestFixture]
    public class CalendarShould
    {
        [Test]
        public void OnNextDayReturnDay()
        {
            var calendar = new Calendar {Today = 25};
            calendar.NextDay();

            Assert.AreEqual(calendar.Today, 26);
        }
    }
}
