namespace ProcessCSV
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.trainDatasetLbl = new System.Windows.Forms.Label();
            this.testDatasetLbl = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.trainDataSetPath = new System.Windows.Forms.TextBox();
            this.testDatasetPath = new System.Windows.Forms.TextBox();
            this.chooseDatasetTrain = new System.Windows.Forms.Button();
            this.chooseDatasetTest = new System.Windows.Forms.Button();
            this.processBtn = new System.Windows.Forms.Button();
            this.nplParamsTxt = new System.Windows.Forms.TextBox();
            this.nplLbl = new System.Windows.Forms.Label();
            this.typeLbl = new System.Windows.Forms.Label();
            this.typeCbx = new System.Windows.Forms.ComboBox();
            this.learnStepLbl = new System.Windows.Forms.Label();
            this.learnStepNum = new System.Windows.Forms.NumericUpDown();
            this.iterationsLbl = new System.Windows.Forms.Label();
            this.iterationsNum = new System.Windows.Forms.NumericUpDown();
            this.resultChart = new Infragistics.Win.DataVisualization.UltraPieChart();
            ((System.ComponentModel.ISupportInitialize)(this.learnStepNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultChart)).BeginInit();
            this.SuspendLayout();
            // 
            // trainDatasetLbl
            // 
            this.trainDatasetLbl.AutoSize = true;
            this.trainDatasetLbl.Location = new System.Drawing.Point(12, 25);
            this.trainDatasetLbl.Name = "trainDatasetLbl";
            this.trainDatasetLbl.Size = new System.Drawing.Size(71, 13);
            this.trainDatasetLbl.TabIndex = 1;
            this.trainDatasetLbl.Text = "Train Dataset";
            // 
            // testDatasetLbl
            // 
            this.testDatasetLbl.AutoSize = true;
            this.testDatasetLbl.Location = new System.Drawing.Point(12, 48);
            this.testDatasetLbl.Name = "testDatasetLbl";
            this.testDatasetLbl.Size = new System.Drawing.Size(68, 13);
            this.testDatasetLbl.TabIndex = 2;
            this.testDatasetLbl.Text = "Test Dataset";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // trainDataSetPath
            // 
            this.trainDataSetPath.Location = new System.Drawing.Point(90, 22);
            this.trainDataSetPath.Name = "trainDataSetPath";
            this.trainDataSetPath.Size = new System.Drawing.Size(433, 20);
            this.trainDataSetPath.TabIndex = 4;
            // 
            // testDatasetPath
            // 
            this.testDatasetPath.Location = new System.Drawing.Point(90, 45);
            this.testDatasetPath.Name = "testDatasetPath";
            this.testDatasetPath.Size = new System.Drawing.Size(433, 20);
            this.testDatasetPath.TabIndex = 5;
            // 
            // chooseDatasetTrain
            // 
            this.chooseDatasetTrain.Location = new System.Drawing.Point(529, 20);
            this.chooseDatasetTrain.Name = "chooseDatasetTrain";
            this.chooseDatasetTrain.Size = new System.Drawing.Size(75, 23);
            this.chooseDatasetTrain.TabIndex = 6;
            this.chooseDatasetTrain.Text = "...";
            this.chooseDatasetTrain.UseVisualStyleBackColor = true;
            this.chooseDatasetTrain.Click += new System.EventHandler(this.chooseDatasetTrain_Click);
            // 
            // chooseDatasetTest
            // 
            this.chooseDatasetTest.Location = new System.Drawing.Point(529, 43);
            this.chooseDatasetTest.Name = "chooseDatasetTest";
            this.chooseDatasetTest.Size = new System.Drawing.Size(75, 23);
            this.chooseDatasetTest.TabIndex = 7;
            this.chooseDatasetTest.Text = "...";
            this.chooseDatasetTest.UseVisualStyleBackColor = true;
            this.chooseDatasetTest.Click += new System.EventHandler(this.chooseDatasetTest_Click);
            // 
            // processBtn
            // 
            this.processBtn.Location = new System.Drawing.Point(15, 73);
            this.processBtn.Name = "processBtn";
            this.processBtn.Size = new System.Drawing.Size(773, 23);
            this.processBtn.TabIndex = 8;
            this.processBtn.Text = "Process";
            this.processBtn.UseVisualStyleBackColor = true;
            this.processBtn.Click += new System.EventHandler(this.processBtn_Click);
            // 
            // nplParamsTxt
            // 
            this.nplParamsTxt.Location = new System.Drawing.Point(434, 167);
            this.nplParamsTxt.Name = "nplParamsTxt";
            this.nplParamsTxt.Size = new System.Drawing.Size(170, 20);
            this.nplParamsTxt.TabIndex = 9;
            this.nplParamsTxt.Text = "2;1";
            // 
            // nplLbl
            // 
            this.nplLbl.AutoSize = true;
            this.nplLbl.Location = new System.Drawing.Point(362, 170);
            this.nplLbl.Name = "nplLbl";
            this.nplLbl.Size = new System.Drawing.Size(66, 13);
            this.nplLbl.TabIndex = 10;
            this.nplLbl.Text = "NPL Params";
            // 
            // typeLbl
            // 
            this.typeLbl.AutoSize = true;
            this.typeLbl.Location = new System.Drawing.Point(365, 128);
            this.typeLbl.Name = "typeLbl";
            this.typeLbl.Size = new System.Drawing.Size(63, 13);
            this.typeLbl.TabIndex = 11;
            this.typeLbl.Text = "Model Type";
            // 
            // typeCbx
            // 
            this.typeCbx.FormattingEnabled = true;
            this.typeCbx.Location = new System.Drawing.Point(434, 128);
            this.typeCbx.Name = "typeCbx";
            this.typeCbx.Size = new System.Drawing.Size(170, 21);
            this.typeCbx.TabIndex = 12;
            // 
            // learnStepLbl
            // 
            this.learnStepLbl.AutoSize = true;
            this.learnStepLbl.Location = new System.Drawing.Point(147, 128);
            this.learnStepLbl.Name = "learnStepLbl";
            this.learnStepLbl.Size = new System.Drawing.Size(59, 13);
            this.learnStepLbl.TabIndex = 13;
            this.learnStepLbl.Text = "Learn Step";
            // 
            // learnStepNum
            // 
            this.learnStepNum.DecimalPlaces = 5;
            this.learnStepNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.learnStepNum.Location = new System.Drawing.Point(213, 128);
            this.learnStepNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.learnStepNum.Name = "learnStepNum";
            this.learnStepNum.Size = new System.Drawing.Size(120, 20);
            this.learnStepNum.TabIndex = 14;
            this.learnStepNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // iterationsLbl
            // 
            this.iterationsLbl.AutoSize = true;
            this.iterationsLbl.Location = new System.Drawing.Point(147, 170);
            this.iterationsLbl.Name = "iterationsLbl";
            this.iterationsLbl.Size = new System.Drawing.Size(50, 13);
            this.iterationsLbl.TabIndex = 15;
            this.iterationsLbl.Text = "Iterations";
            // 
            // iterationsNum
            // 
            this.iterationsNum.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.iterationsNum.Location = new System.Drawing.Point(213, 170);
            this.iterationsNum.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.iterationsNum.Name = "iterationsNum";
            this.iterationsNum.Size = new System.Drawing.Size(120, 20);
            this.iterationsNum.TabIndex = 16;
            this.iterationsNum.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // resultChart
            // 
            this.resultChart.BackColor = System.Drawing.Color.White;
            this.resultChart.FontSize = 14D;
            this.resultChart.Location = new System.Drawing.Point(127, 196);
            this.resultChart.Name = "resultChart";
            this.resultChart.SelectedStyle.Opacity = 1D;
            this.resultChart.SelectedStyle.Stroke = new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))));
            this.resultChart.SelectedStyle.StrokeThickness = 6D;
            this.resultChart.Size = new System.Drawing.Size(499, 245);
            this.resultChart.TabIndex = 17;
            this.resultChart.Text = "ultraPieChart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.resultChart);
            this.Controls.Add(this.iterationsNum);
            this.Controls.Add(this.iterationsLbl);
            this.Controls.Add(this.learnStepNum);
            this.Controls.Add(this.learnStepLbl);
            this.Controls.Add(this.typeCbx);
            this.Controls.Add(this.typeLbl);
            this.Controls.Add(this.nplLbl);
            this.Controls.Add(this.nplParamsTxt);
            this.Controls.Add(this.processBtn);
            this.Controls.Add(this.chooseDatasetTest);
            this.Controls.Add(this.chooseDatasetTrain);
            this.Controls.Add(this.testDatasetPath);
            this.Controls.Add(this.trainDataSetPath);
            this.Controls.Add(this.testDatasetLbl);
            this.Controls.Add(this.trainDatasetLbl);
            this.Name = "Form1";
            this.Text = "Process CSV";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.learnStepNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label trainDatasetLbl;
        private System.Windows.Forms.Label testDatasetLbl;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox trainDataSetPath;
        private System.Windows.Forms.TextBox testDatasetPath;
        private System.Windows.Forms.Button chooseDatasetTrain;
        private System.Windows.Forms.Button chooseDatasetTest;
        private System.Windows.Forms.Button processBtn;
        private System.Windows.Forms.TextBox nplParamsTxt;
        private System.Windows.Forms.Label nplLbl;
        private System.Windows.Forms.Label typeLbl;
        private System.Windows.Forms.ComboBox typeCbx;
        private System.Windows.Forms.Label learnStepLbl;
        private System.Windows.Forms.NumericUpDown learnStepNum;
        private System.Windows.Forms.Label iterationsLbl;
        private System.Windows.Forms.NumericUpDown iterationsNum;
        private Infragistics.Win.DataVisualization.UltraPieChart resultChart;
    }
}

