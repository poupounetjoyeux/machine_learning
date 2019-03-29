using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;

namespace ProcessCSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chooseDatasetTrain_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                trainDataSetPath.Text = openFileDialog1.FileName;
            }
        }

        private void chooseDatasetTest_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrEmpty(openFileDialog1.FileName))
            {
                testDatasetPath.Text = openFileDialog1.FileName;
            }
        }

        private void processBtn_Click(object sender, EventArgs e)
        {
            var expectedSigns = new List<int>();
            var inputs = new List<double>();
            using (var reader = new StreamReader(trainDataSetPath.Text))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<WineLine>();
                foreach (var record in records)
                {
                    expectedSigns.Add(record.Quality);
                    inputs.Add(record.FixedAcidity);
                    inputs.Add(record.VolatileAcidity);
                    inputs.Add(record.CitricAcid);
                    inputs.Add(record.ResidualSugar);
                    inputs.Add(record.Chlorides);
                    inputs.Add(record.FreeSulfurDioxide);
                    inputs.Add(record.TotalSulfurDioxide);
                    inputs.Add(record.Density);
                    inputs.Add(record.Ph);
                    inputs.Add(record.Sulphates);
                    inputs.Add(record.Alcohol);
                }
            }
        }
    }
}
