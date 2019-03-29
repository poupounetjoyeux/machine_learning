using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CsvHelper;

namespace ProcessCSV
{
    public enum Type
    {
        MulticoucheClassification
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            typeCbx.DataSource = Enum.GetValues(typeof(Type));
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
            
            
            if ((Type) typeCbx.SelectedItem == Type.MulticoucheClassification)
            {
                var npl = nplParamsTxt.Text.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                    .ToArray();
                var expectedSigns = new List<int>();
                var inputs = new List<double>();
                using (var reader = new StreamReader(trainDataSetPath.Text))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.PrepareHeaderForMatch =
                        (header, idx) => header.Replace(" ", string.Empty).ToLower();
                    var records = csv.GetRecords<WineLine>();
                    foreach (var record in records)
                    {
                        var expected = new double[npl[npl.Length - 1]];
                        for (var i = 0; i < npl[npl.Length - 1]; i++)
                        {
                            expected[i] = i + 3 == record.quality ? 1 : -1;
                        }

                        expectedSigns.AddRange(expected.Select(x => (int)x).ToArray());
                        inputs.Add(record.fixedacidity);
                        inputs.Add(record.volatileacidity);
                        inputs.Add(record.citricacid);
                        inputs.Add(record.residualsugar);
                        inputs.Add(record.chlorides);
                        inputs.Add(record.freesulfurdioxide);
                        inputs.Add(record.totalsulfurdioxide);
                        inputs.Add(record.density);
                        inputs.Add(record.ph);
                        inputs.Add(record.sulphates);
                        inputs.Add(record.alcohol);
                    }
                }
                var model = ClassificationLibrary.createMultilayerModel(npl, npl.Length, (double) learnStepNum.Value);
                ClassificationLibrary.trainModelMultilayerClassification(model, inputs.ToArray(), inputs.Count / 11,
                    expectedSigns.ToArray(), (int) iterationsNum.Value);

                var result = new Dictionary<int, int>();
                for (var i = 0; i < npl[npl.Length - 1]; i++)
                {
                    result.Add(i, 0);
                }

                using (var reader = new StreamReader(testDatasetPath.Text))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.PrepareHeaderForMatch =
                        (header, idx) => header.Replace(" ", string.Empty).ToLower();
                    var records = csv.GetRecords<WineLine>();
                    foreach (var record in records)
                    {
                        inputs.Add(record.fixedacidity);
                        inputs.Add(record.volatileacidity);
                        inputs.Add(record.citricacid);
                        inputs.Add(record.residualsugar);
                        inputs.Add(record.chlorides);
                        inputs.Add(record.freesulfurdioxide);
                        inputs.Add(record.totalsulfurdioxide);
                        inputs.Add(record.density);
                        inputs.Add(record.ph);
                        inputs.Add(record.sulphates);
                        inputs.Add(record.alcohol);
                    }
                }

                for (var i = 0; i < inputs.Count; i += 11)
                {
                    if ((Type)typeCbx.SelectedItem == Type.MulticoucheClassification)
                    {
                        var point = inputs.GetRange(i, 11).ToArray();
                        var output = new double[npl[npl.Length - 1]];
                        var prediction = ClassificationLibrary.predictMultilayerClassificationModel(model, point);
                        Marshal.Copy(prediction, output, 0, npl[npl.Length - 1]);
                        result[Array.IndexOf(output, output.Max())]++;
                    }
                }

                ClassificationLibrary.releaseMultilayerModel(model);
                resultChart.LabelMemberPath = "Key";
                resultChart.ValueMemberPath = "Value";
                resultChart.DataSource = result;
            }
        }
    }
}
