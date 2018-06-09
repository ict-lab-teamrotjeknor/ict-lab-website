using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ict_lab_website.Tests.Schedule
{
    public class ScheduleTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> scheduleTestData = new List<object[]>
        {
            new object[] {new DateTime(2018, 06, 09) },
            new object[] {new DateTime(1818, 01, 22) },
            new object[] {new DateTime(2019, 07, 29) },
            new object[] {new DateTime(2011, 03, 19) },
            new object[] {new DateTime(2018, 03, 20) },
            new object[] {new DateTime(2018, 01, 30) },
            new object[] {new DateTime(2018, 02, 20) },
        };

        public IEnumerator<Object[]> GetEnumerator()
        {
            return scheduleTestData.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
