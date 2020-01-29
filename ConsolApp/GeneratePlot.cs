using System;
using System.Collections.Generic;
using System.Text;
using static ConsolApp.ConsolData;

namespace ConsolApp
{

    using OxyPlot;
    using OxyPlot.Series;

    public class GeneratePlot
    {
        public GeneratePlot()
        {
            this.GraphData = new List<DataPoint>
                {
                    new DataPoint(0.0, 0.0),
                };

        }

        public IList<DataPoint> GraphData { get; set; }

        public void NewPlot(TestData testdata)
        {
            if (testdata != null)
            {
                GraphData.Clear();

                for (int i = 0; i < testdata.Divs.Count; i++)
                    {
                        GraphData.Add(new DataPoint(testdata.Time[i], testdata.Divs[i]));
                    }
            }
        }
    }
}
