﻿using System;
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
            dataGridView1.MultiSelect = true;
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

                var metricsCalculation = new AdvancedMetricsCalculation();

                //advance mettrics calculation
                double np = metricsCalculation.CalculateNormalizedPower(hrData);
                label4.Text = "Normalized power = " + Summary.RoundUp(np, 2);

                double ftp = metricsCalculation.CalculateFunctionalThresholdPower(hrData);
                label5.Text = "Training Stress Score = " + Summary.RoundUp(ftp, 2);

                double ifa = metricsCalculation.CalculateIntensityFactor(hrData);
                label6.Text = "Intensity Factor = " + Summary.RoundUp(ifa, 2);

                double pb = metricsCalculation.CalculatePowerBalance(hrData);
                label3.Text = "Power balance = " + Summary.RoundUp(pb, 2);

                var param = hrData["params"] as Dictionary<string, string>;

                //header file
                lblStartTime.Text = lblStartTime.Text + "= " + param["StartTime"];
                lblInterval.Text = lblInterval.Text + "= " + param["Interval"];
                lblMonitor.Text = lblMonitor.Text + "= " + param["Monitor"];
                lblSMode.Text = lblSMode.Text + "= " + param["SMode"];
                lblDate.Text = lblDate.Text + "= " + Summary.ConvertToDate(param["Date"]);
                lblLength.Text = lblLength.Text + "= " + param["Length"];
                lblWeight.Text = lblWeight.Text + "= " + param["Weight"];



                //smode calculation
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
                dataGridView2.Rows.Add(new TableFiller().FillDataInSumaryTable(hrData, hrData["endTime"] as string, hrData["params"] as Dictionary<string, string>));

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
            //count++;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.SelectedCells.Count == 6)
            //{
            //  foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //  {
            //    //string cadence = dataGridView1.SelectedCells[0].Value.ToString();
            //    //string altitude = dataGridView1.SelectedCells[1].Value.ToString();
            //    //string heartRate = dataGridView1.SelectedCells[2].Value.ToString();
            //    //string power = dataGridView1.SelectedCells[3].Value.ToString();
            //    //string speed = dataGridView1.SelectedCells[4].Value.ToString();
            //    //string time = dataGridView1.SelectedCells[5].Value.ToString();

            //    MessageBox.Show(row.Index.ToString());
            //  }



            //  //MessageBox.Show(cadence + "/" + altitude + "/" + heartRate + "/" + power + "/" + speed + "/" + time);
            //  //string id = dataGridView1.SelectedCells[0].Value.ToString();
            //  //MessageBox.Show(id);
            //}
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private Dictionary<string, object> data = new Dictionary<string, object>();
        List<string> listCadence = new List<string>();
        List<string> listAltitude = new List<string>();
        List<string> listHeartRate = new List<string>();
        List<string> listPower = new List<string>();
        List<string> listSpeed = new List<string>();
        List<string> listTime = new List<string>();

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {

                if (e.StateChanged != DataGridViewElementStates.Selected) return;
                int index = Convert.ToInt32(e.Row.Index.ToString());

                string cadence = dataGridView1.Rows[index].Cells[0].Value.ToString();
                listCadence.Add(cadence);

                //_hrData.Add("cadence", new List());
                string altitude = dataGridView1.Rows[index].Cells[1].Value.ToString();
                listAltitude.Add(altitude);

                string heartRate = dataGridView1.Rows[index].Cells[2].Value.ToString();
                listHeartRate.Add(heartRate);

                string power = dataGridView1.Rows[index].Cells[3].Value.ToString();
                listPower.Add(power);

                string speed = dataGridView1.Rows[index].Cells[4].Value.ToString();
                listSpeed.Add(speed);

                string time = dataGridView1.Rows[index].Cells[5].Value.ToString();
                listTime.Add(time);

                Console.WriteLine(cadence + "/" + altitude + "/" + heartRate + "/" + power + "/" + speed + "/" + time);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            data.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                data.Add("cadence", listCadence);
                data.Add("altitude", listAltitude);
                data.Add("heartRate", listHeartRate);
                data.Add("watt", listPower);
                data.Add("speed", listSpeed);
                data.Add("time", listTime);

                var endTime = data["time"] as List<string>;
                int count = endTime.Count();
                Dictionary<string, string> _param = new Dictionary<string, string>();
                _param.Add("StartTime", endTime[0]);

                dataGridView2.Rows.Clear();
                dataGridView2.Rows.Add(new TableFiller().FillDataInSumaryTable(data, endTime[count - 1], _param));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please click on reset button first");
            }
        }

        private void intervalDetectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = _hrData.ToDictionary(k => k.Key, k => k.Value as object);
            new IntervalDetectForm(data).Show();
        }

        Dictionary<string, object> list = new Dictionary<string, object>();
        private void button5_Click(object sender, EventArgs e)
        {
            string val = textBox1.Text;
            int value;
            if (int.TryParse(val, out value))
            {
                int count = 0;
                try
                {
                    count = ((List<string>)data["speed"]).Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please select a row first");
                }

                int portion = count / Convert.ToInt32(val);

                var cadenceData = data["cadence"] as List<string>;
                var altitudeData = data["altitude"] as List<string>;
                var heartRateData = data["heartRate"] as List<string>;
                var wattData = data["watt"] as List<string>;
                var speedData = data["speed"] as List<string>;

                var newCadenceData = new List<string>();
                var newAltitudeData = new List<string>();
                var newHeartRateData = new List<string>();
                var newWattData = new List<string>();
                var newSpeedData = new List<string>();

                int num = 0;
                int portionNumber = 0;

                for (int i = 0; i < count; i++)
                {
                    num++;
                    newCadenceData.Add(cadenceData[i]);
                    newAltitudeData.Add(altitudeData[i]);
                    newHeartRateData.Add(heartRateData[i]);
                    newWattData.Add(wattData[i]);
                    newSpeedData.Add(speedData[i]);

                    if (num == portion)
                    {
                        num = 0;
                        portionNumber++;

                        var listData = new Dictionary<string, List<string>>();
                        listData.Add("cadence", newCadenceData);
                        listData.Add("altitude", newAltitudeData);
                        listData.Add("heartRate", newHeartRateData);
                        listData.Add("watt", newWattData);
                        listData.Add("speed", newSpeedData);

                        list.Add("data" + portionNumber, listData);

                        newCadenceData = new List<string>();
                        newAltitudeData = new List<string>();
                        newHeartRateData = new List<string>();
                        newWattData = new List<string>();
                        newSpeedData = new List<string>();
                    }

                }

                comboBox1.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    comboBox1.Items.Add("Portion " + (i + 1));
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number between 0 - 9");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex + 1;

            dataGridView2.Rows.Clear();

            var a = list["data" + selectedIndex] as Dictionary<string, List<string>>;
            var b = a.ToDictionary(k => k.Key, k => k.Value as object);


            var data = new TableFiller().FillDataInSumaryTable(b, "19:12:15", null);
            dataGridView2.Rows.Add(data);
        }
    }
}
