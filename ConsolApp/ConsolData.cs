using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolApp
{
    public class ConsolData
    {
        public class TestData
        {
            public List<int> Divs { get; set; }
            public List<double> Time { get; set; }
        }

        public TestData ParseFile(string filepath)
        {
            string text = System.IO.File.ReadAllText(filepath);

            TestData test_data = new TestData();

            List<int> div_readings = new List<int>();
            List<double> time_readings = new List<double>();

            for (int i = 0; i < text.Length-1; i+=17)
            {
                string temp = text.Substring(i, 17);

                int result = Int16.Parse(temp.Substring(3, 6));
                int hours = Int16.Parse(temp.Substring(9, 5));
                int minutes = Int16.Parse(temp.Substring(14, 2));
                int tenths = Int16.Parse(temp.Substring(16));
                double root_time = (hours * 60) + minutes + (tenths * 0.1);

                div_readings.Add(result);
                time_readings.Add(root_time);

            }

            test_data.Divs = div_readings;
            test_data.Time = time_readings;

            return test_data;
        }
    }
}
