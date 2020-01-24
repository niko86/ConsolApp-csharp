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
            PlotModel tmp = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };

            // Create two line series (markers are hidden by default)
            LineSeries series = new LineSeries { Title = "Series", MarkerType = MarkerType.Circle };
            TestData testdata = MainWindow.PlotTestData;

            if (testdata != null)
            {
                for (int i = 0; i < testdata.Divs.Count; i++)
                {
                    series.Points.Add(new DataPoint(testdata.Divs[i], testdata.Time[i]));
                }
            }

            // Add the series to the plot model
            tmp.Series.Add(series);

            // Axes are created automatically if they are not defined

            // Set the Model property, the INotifyPropertyChanged event will make the WPF Plot control update its content
            Model = tmp;
        }

        public PlotModel Model { get; private set; }
    }
}
