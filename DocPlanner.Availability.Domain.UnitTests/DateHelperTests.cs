namespace DocPlanner.Availability.Domain.UnitTests
{
    public class DateHelperTests
    {
        [Theory]
        [MemberData(nameof(GetMondayOfTheWeek_TestCases))]
        public void GetMondayOfTheWeek_IsProperlyCalculated(DateTime inputDate, DateTime expectedDate)
        {
            var resultDate = inputDate.GetMondayOfTheWeek();

            Assert.Equal(expectedDate, resultDate);
        }

        [Theory]
        [MemberData(nameof(WithHour_TestCases))]
        public void WithHour_IsProperlyCalculated(DateTime inputDate, int hour, DateTime expectedDate)
        {
            var resultDate = inputDate.WithHour(hour);

            Assert.Equal(expectedDate, resultDate);
        }

        public static IEnumerable<object[]> GetMondayOfTheWeek_TestCases
        {
            get
            {
                // 7th October 2024 is Monday for the test week
                yield return new object[] { new DateTime(2024, 10, 7), new DateTime(2024, 10, 7) };
                yield return new object[] { new DateTime(2024, 10, 8), new DateTime(2024, 10, 7) };
                yield return new object[] { new DateTime(2024, 10, 9), new DateTime(2024, 10, 7) };
                yield return new object[] { new DateTime(2024, 10, 10), new DateTime(2024, 10, 7) };
                yield return new object[] { new DateTime(2024, 10, 11), new DateTime(2024, 10, 7) };
                yield return new object[] { new DateTime(2024, 10, 12), new DateTime(2024, 10, 7) };
                yield return new object[] { new DateTime(2024, 10, 13), new DateTime(2024, 10, 7) };

                // Edge-case: Even providing dates with time values will make the same and not use them for the result
                yield return new object[] { new DateTime(2024, 10, 7, 12, 13, 14), new DateTime(2024, 10, 7) };
            }
        }

        public static IEnumerable<object[]> WithHour_TestCases
        {
            get
            {
                yield return new object[] { new DateTime(2024, 10, 7), 10, new DateTime(2024, 10, 7, 10, 0, 0) };
                yield return new object[] { new DateTime(2024, 10, 8, 11, 12, 13), 9, new DateTime(2024, 10, 8, 9, 0, 0) };
            }
        }
    }
}