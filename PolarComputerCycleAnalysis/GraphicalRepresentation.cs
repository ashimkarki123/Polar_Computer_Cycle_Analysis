using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace PolarComputerCycleAnalysis
{
    public partial class GraphicalRepresentation : Form
    {
        public static Dictionary<string, List<string>> _hrData;

        public GraphicalRepresentation()
        {
            InitializeComponent();
        }
        

        private void plotGraph()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Setting Titles of graph
            myPane.Title = "Overview";
            myPane.XAxis.Title = "Time in second";
            myPane.YAxis.Title = "Data";


            PointPairList cadencePairList = new PointPairList();
            PointPairList altitudePairList = new PointPairList();
            PointPairList heartPairList = new PointPairList();
            PointPairList powerPairList = new PointPairList();
            PointPairList speedPairList = new PointPairList();


            for (int i = 0; i < _hrData["cadence"].Count; i++)
            {
                cadencePairList.Add(i, Convert.ToInt16(_hrData["cadence"][i]));
            }

            for (int i = 0; i < _hrData["altitude"].Count; i++)
            {
                altitudePairList.Add(i, Convert.ToInt16(_hrData["altitude"][i]));
            }

            for (int i = 0; i < _hrData["heartRate"].Count; i++)
            {
                heartPairList.Add(i, Convert.ToInt16(_hrData["heartRate"][i]));
            }

            for (int i = 0; i < _hrData["watt"].Count; i++)
            {
                powerPairList.Add(i, Convert.ToInt16(_hrData["watt"][i]));
            }

            for (int i = 0; i < _hrData["speed"].Count; i++)
            {
                speedPairList.Add(i, Convert.ToDouble(_hrData["speed"][i]));
            }

            LineItem cadence = myPane.AddCurve("Cadence",
                   cadencePairList, Color.Green, SymbolType.None);

            LineItem altitude = myPane.AddCurve("Altitude",
                  altitudePairList, Color.Black, SymbolType.None);

            LineItem heart = myPane.AddCurve("Heart",
                   heartPairList, Color.Red, SymbolType.None);

            LineItem power = myPane.AddCurve("Power",
                  powerPairList, Color.Blue, SymbolType.None);

            LineItem speed = myPane.AddCurve("Speed",
                  speedPairList, Color.Purple, SymbolType.None);

            zedGraphControl1.AxisChange();
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

        }

        private void GraphicalRepresentation_Load(object sender, EventArgs e)
        {
            plotGraph();
            SetSize();
            InitGraph();

            zedGraphControl1.Visible = true;
            zedGraphControl2.Visible = false;
            zedGraphControl3.Visible = false;
            zedGraphControl4.Visible = false;
            zedGraphControl5.Visible = false;
            zedGraphControl6.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = false;
            zedGraphControl2.Visible = true;
            zedGraphControl3.Visible = false;
            zedGraphControl4.Visible = false;
            zedGraphControl5.Visible = false;
            zedGraphControl6.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = false;
            zedGraphControl2.Visible = false;
            zedGraphControl3.Visible = true;
            zedGraphControl4.Visible = false;
            zedGraphControl5.Visible = false;
            zedGraphControl6.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = false;
            zedGraphControl2.Visible = false;
            zedGraphControl3.Visible = false;
            zedGraphControl4.Visible = true;
            zedGraphControl5.Visible = false;
            zedGraphControl6.Visible = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = false;
            zedGraphControl2.Visible = false;
            zedGraphControl3.Visible = false;
            zedGraphControl4.Visible = false;
            zedGraphControl5.Visible = true;
            zedGraphControl6.Visible = false;
        }

        private void InitGraph()
        {
            GraphPane altitudePane = zedGraphControl2.GraphPane;
            GraphPane heartRatePane = zedGraphControl3.GraphPane;
            GraphPane cadencePane = zedGraphControl4.GraphPane;
            GraphPane powerPane = zedGraphControl5.GraphPane;
            GraphPane speedPane = zedGraphControl6.GraphPane;


            // Setting graph Titles
            altitudePane.Title = "Overview";
            altitudePane.XAxis.Title = "Time in second";
            altitudePane.YAxis.Title = "Data";

            heartRatePane.Title = "Overview";
            heartRatePane.XAxis.Title = "Time in second";
            heartRatePane.YAxis.Title = "Data";

            cadencePane.Title = "Overview";
            cadencePane.XAxis.Title = "Time in second";
            cadencePane.YAxis.Title = "Data";

            powerPane.Title = "Overview";
            powerPane.XAxis.Title = "Time in second";
            powerPane.YAxis.Title = "Data";

            speedPane.Title = "Overview";
            speedPane.XAxis.Title = "Time in second";
            speedPane.YAxis.Title = "Data";

            PointPairList cadencePairList = new PointPairList();
            PointPairList altitudePairList = new PointPairList();
            PointPairList heartPairList = new PointPairList();
            PointPairList powerPairList = new PointPairList();
            PointPairList speedPairList = new PointPairList();


            for (int i = 0; i < _hrData["cadence"].Count; i++)
            {
                cadencePairList.Add(i, Convert.ToInt16(_hrData["cadence"][i]));
            }

            for (int i = 0; i < _hrData["altitude"].Count; i++)
            {
                altitudePairList.Add(i, Convert.ToInt16(_hrData["altitude"][i]));
            }

            for (int i = 0; i < _hrData["heartRate"].Count; i++)
            {
                heartPairList.Add(i, Convert.ToInt16(_hrData["heartRate"][i]));
            }

            for (int i = 0; i < _hrData["watt"].Count; i++)
            {
                powerPairList.Add(i, Convert.ToInt16(_hrData["watt"][i]));
            }

            for (int i = 0; i < _hrData["speed"].Count; i++)
            {
                speedPairList.Add(i, Convert.ToInt16(_hrData["speed"][i]));
            }

            LineItem cadence = cadencePane.AddCurve("Cadence",
                    cadencePairList, Color.Red, SymbolType.None);
            

            LineItem altitude = altitudePane.AddCurve("Altitude",
                  altitudePairList, Color.Cyan, SymbolType.None);

            LineItem heart = heartRatePane.AddCurve("Heart",
                   heartPairList, Color.Black, SymbolType.None);

            LineItem power = powerPane.AddCurve("Power",
                  powerPairList, Color.DarkGreen, SymbolType.None);

            LineItem speed = speedPane.AddCurve("Speed",
                  speedPairList, Color.DarkGreen, SymbolType.None);

            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();
            zedGraphControl5.AxisChange();
            zedGraphControl6.AxisChange();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = false;
            zedGraphControl2.Visible = false;
            zedGraphControl3.Visible = false;
            zedGraphControl4.Visible = false;
            zedGraphControl5.Visible = false;
            zedGraphControl6.Visible = true;
        }
    }
}
