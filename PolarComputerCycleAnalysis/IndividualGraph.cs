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
    public partial class IndividualGraph : Form
    {
        public static Dictionary<string, List<string>> _hrData;
        private List<int> _smode;

        public IndividualGraph(List<int> smode)
        {
            _smode = smode;
            InitializeComponent();
            plotGraph();
        }

        private void plotGraph()
        {
            GraphPane altitudePane = zedGraphControl1.GraphPane;
            GraphPane heartRatePane = zedGraphControl2.GraphPane;
            GraphPane cadencePane = zedGraphControl3.GraphPane;
            GraphPane powerPane = zedGraphControl4.GraphPane;
            GraphPane speedPane = zedGraphControl5.GraphPane;


            // Setting Titles for each graph
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
                   cadencePairList, Color.Green, SymbolType.None);

            LineItem altitude = altitudePane.AddCurve("Altitude",
                  altitudePairList, Color.Black, SymbolType.None);

            LineItem heart = heartRatePane.AddCurve("Heart",
                   heartPairList, Color.Red, SymbolType.None);

            LineItem power = powerPane.AddCurve("Power",
                  powerPairList, Color.Blue, SymbolType.None);

            LineItem speed = speedPane.AddCurve("Speed",
                  speedPairList, Color.Purple, SymbolType.None);

            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();
            zedGraphControl5.AxisChange();

            if (_smode[0] == 0)
            {
                zedGraphControl1.Visible = false;
            }
            else if (_smode[1] == 0)
            {
                zedGraphControl2.Visible = false;
            }
            else if (_smode[2] == 0)
            {
                zedGraphControl3.Visible = false;
            }
            else if (_smode[3] == 0)
            {
                zedGraphControl4.Visible = false;
            }
            else if (_smode[4] == 0)
            {
                zedGraphControl5.Visible = false;
            }
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            zedGraphControl2.Location = new Point(0, 0);
            zedGraphControl2.IsShowPointValues = true;
            zedGraphControl2.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            zedGraphControl3.Location = new Point(0, 0);
            zedGraphControl3.IsShowPointValues = true;
            zedGraphControl3.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            zedGraphControl4.Location = new Point(0, 0);
            zedGraphControl4.IsShowPointValues = true;
            zedGraphControl4.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);
        }

        private void IndividualGraph_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            plotGraph();
        }
    }
}
