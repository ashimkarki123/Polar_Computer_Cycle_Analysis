using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolarComputerCycleAnalysis
{
    public partial class Form1 : Form
    {
        private List<int> smode = new List<int>();
        private int count = 0;
        private string endTime;
        private Dictionary<string, List<string>> _hrData = new Dictionary<string, List<string>>();
        private Dictionary<string, string> _param = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            InitGrid();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string text = File.ReadAllText(openFileDialog1.FileName);
                Dictionary<string, object> hrData = new TableFiller().FillTable(text, dataGridView1);
                _hrData = hrData.ToDictionary(k => k.Key, k => k.Value as List<string>);
                var param = hrData["params"] as Dictionary<string, string>;
                var sMode = param["SMode"];
                for (int i = 0; i < sMode.Length; i++)
                {
                    smode.Add((int)Char.GetNumericValue(param["SMode"][i]));
                }
                if (smode[0] == 0)
                {
                    dataGridView1.Columns[0].Visible = false;
                }
                else if (smode[1] == 0)
                {
                    dataGridView1.Columns[1].Visible = false;
                }
                else if (smode[2] == 0)
                {
                    dataGridView1.Columns[2].Visible = false;
                }
                else if (smode[3] == 0)
                {
                    dataGridView1.Columns[3].Visible = false;
                }
                else if (smode[4] == 0)
                {
                    dataGridView1.Columns[4].Visible = false;
                }
                dataGridView2.Rows.Clear();
                dataGridView2.Rows.Add(new TableFiller().FillDataInSumaryTable(hrData, hrData["params"] as Dictionary<string, string>, hrData["endTime"] as string));

            }
        }

        private void CalculateSpeed(string type)
        {
            if (_hrData.Count > 0)
            {
                List<string> data = new List<string>();
                if (type == "mile")
                {
                    dataGridView1.Columns[4].Name = "Speed(Mile/hr)";

                    data.Clear();
                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        string temp = (Convert.ToDouble(_hrData["speed"][i]) / 1.60934).ToString();
                        data.Add(temp);
                    }

                    _hrData["speed"] = data;

                    dataGridView1.Rows.Clear();
                    DateTime dateTime = DateTime.Parse(_param["StartTime"]);
                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        if (i > 0) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
                        string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i], _hrData["watt"][i], _hrData["speed"][i], dateTime.TimeOfDay.ToString() };
                        dataGridView1.Rows.Add(hrData);
                    }
                }
                else
                {
                    dataGridView1.Columns[4].Name = "Speed(km/hr)";

                    data.Clear();

                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        string temp = (Convert.ToDouble(_hrData["speed"][i]) * 1.60934).ToString();
                        data.Add(temp);
                    }

                    _hrData["speed"] = data;

                    dataGridView1.Rows.Clear();
                    DateTime dateTime = DateTime.Parse(_param["StartTime"]);
                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        if (i > 0) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
                        string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i], _hrData["watt"][i], _hrData["speed"][i], dateTime.TimeOfDay.ToString() };
                        dataGridView1.Rows.Add(hrData);
                    }
                }
            }
        }

        private void InitGrid()
        {
            //Viewing data in grid 
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Cadence (RPM)";
            dataGridView1.Columns[1].Name = "Altitude (m/ft)";
            dataGridView1.Columns[2].Name = "Heart rate (BPM)";
            dataGridView1.Columns[3].Name = "Power (Watts)";
            dataGridView1.Columns[4].Name = "Speed(Mile/hr)";
            dataGridView1.Columns[5].Name = "Time";

            dataGridView2.ColumnCount = 10;
            dataGridView2.Columns[0].Name = "Total distance covered (KM)";
            dataGridView2.Columns[1].Name = "Average speed (Km/Hr)";
            dataGridView2.Columns[2].Name = "Maximum speed (Km/Hr)";
            dataGridView2.Columns[3].Name = "Average heart rate (BPM)";
            dataGridView2.Columns[4].Name = "Maximum heart rate (BPM)";
            dataGridView2.Columns[5].Name = "Minimum heart rate (BPM)";
            dataGridView2.Columns[6].Name = "Average power (Watts)";
            dataGridView2.Columns[7].Name = "Maximum power (Watts)";
            dataGridView2.Columns[8].Name = "Average altitude(m/ft)";
            dataGridView2.Columns[9].Name = "Maximum altitude(m/ft)";
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Error handeled while there is no data
            if (_hrData.Count < 1)
            {
                MessageBox.Show("Please enter data first");
            }
            else
            {
                GraphicalRepresentation._hrData = _hrData;
                new GraphicalRepresentation().Show();
            }
        }

        private void individualGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_hrData.Count < 1)
            {
                MessageBox.Show("Please enter data first");
            }
            else
            {
                IndividualGraph._hrData = _hrData;
                new IndividualGraph(smode).Show();
            }

        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PolarComputerCycle By:" + Environment.NewLine +                    //Showing in Dialog box//
                "Name: Ashim Karki" + Environment.NewLine +
                "Email: Lashimkarki@gmail.com" + Environment.NewLine +
                "Contact Num: +9779841647756" + Environment.NewLine +
                "Developed In: Microsoft Visual Studio 2017 Community" + Environment.NewLine,
                "Version 1.0.0 freeware"
                );
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            count++;
            if (count > 1) CalculateSpeed("mile");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CalculateSpeed("km");
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void compareFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FileCompare().Show();
        }
    }
}
