using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using ListenAndWrite.ModelIdentify;

namespace ListenAndWrite.Logic
{
    public class ChartAction
    {
        public const int MAX_LEVEL = 20;

        public static void showChart(List<DatePoint> listScores, Chart chart)
        {
            Series[] listSeries = new Series[MAX_LEVEL + 1];
            Series s = null;
            int level = 0;
            for (int i = 0; i < listScores.Count; i++)
            {
                level = listScores[i].level;
                if (listSeries[level] == null)
                {
                    s = new Series("Level " + level);
                    s.ChartType = SeriesChartType.Column;
                    s["PixelPointWidth"] = "80";
                    listSeries[level] = s;
                    chart.Series.Add(s);
                }
                else
                {
                    s = listSeries[level];
                }
                s.Points.AddXY(listScores[i].date, listScores[i].point);
            }
            Legend legend = new Legend("Gloss");
            chart.Legends.Add(legend);

            chart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
            chart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
        }
    }
}