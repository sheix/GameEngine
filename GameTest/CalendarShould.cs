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
            var calendar = new Calendar();
            Calendar.Today = 25;
            Calendar.NextDay();

            Assert.Equals(Calendar.Today, 26);
        }
    }
}
