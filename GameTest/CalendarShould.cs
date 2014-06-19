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
            var calendar = new Calendar(25);;
            calendar.NextDay();

            Assert.AreEqual(calendar.DayFromStart, 26);
        }

        [Test] 
        public void FlipYearOnEvery360Days()
        {
            var calendar = new Calendar(360);
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
            var calendar = new Calendar(day); 
            
            Assert.AreEqual(calendar.DayInYear,dayinyear);
        }



    }
}
