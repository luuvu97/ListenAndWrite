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

        public static void showChart1(List<DatePoint> listScores, Chart chart)
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
                //else
                //{
                //    s = chart.Series["Level " + level];
                //}
                s = chart.Series["Level " + level];
                s.Points.AddXY(listScores[i].date, listScores[i].point);
                CustomLabel c = new CustomLabel();
                c.FromPosition = level - 0.5;
                c.ToPosition = level + 0.5;
                c.Text = listScores[i].date.ToShortDateString();
                chart.ChartAreas[0].AxisX.CustomLabels.Add(c);
            }
            Legend legend = new Legend("Gloss");
            chart.Legends.Add(legend);

            chart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
            chart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
        }

        public static void showChart(List<DatePoint> listScores, Chart chart)
        {
            Series[] listSeries = new Series[MAX_LEVEL + 1];
            Dictionary<DateTime, Int32> listDate = new Dictionary<DateTime, int>();
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
                    s.Font = new System.Drawing.Font("Courier New", 10.0f, System.Drawing.FontStyle.Bold);
                    s.IsValueShownAsLabel = true;
                    chart.Series.Add(s);
                }
                int index;
                if (!listDate.TryGetValue(listScores[i].date, out index))
                {
                    listDate.Add(listScores[i].date, listDate.Count + 1);
                }
            }

            foreach (KeyValuePair<DateTime, Int32> entry in listDate)
            {
                CustomLabel cl = new CustomLabel();
                cl.FromPosition = entry.Value - 0.4;
                cl.ToPosition = entry.Value + 0.4;
                cl.Text = entry.Key.ToShortDateString();
                chart.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Courier New", 10.0f, System.Drawing.FontStyle.Bold);
                chart.ChartAreas[0].AxisX.CustomLabels.Add(cl);
            }

            for (int i = 0; i < listScores.Count; i++)
            {
                level = listScores[i].level;
                s = chart.Series["Level " + level];
                s.Points.AddXY(listDate[listScores[i].date], Math.Round(listScores[i].point));
            }

            Legend legend = new Legend("Gloss");
            chart.Legends.Add(legend);

            chart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
            chart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
        }
    }
}