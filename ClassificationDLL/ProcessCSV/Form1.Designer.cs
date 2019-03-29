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
            this.ultraDataChart1 = new Infragistics.Win.DataVisualization.UltraDataChart();
            this.trainDatasetLbl = new System.Windows.Forms.Label();
            this.testDatasetLbl = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.trainDataSetPath = new System.Windows.Forms.TextBox();
            this.testDatasetPath = new System.Windows.Forms.TextBox();
            this.chooseDatasetTrain = new System.Windows.Forms.Button();
            this.chooseDatasetTest = new System.Windows.Forms.Button();
            this.processBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraDataChart1
            // 
            this.ultraDataChart1.BackColor = System.Drawing.Color.White;
            this.ultraDataChart1.CrosshairPoint = new Infragistics.Win.DataVisualization.Point(double.NaN, double.NaN);
            this.ultraDataChart1.Location = new System.Drawing.Point(12, 138);
            this.ultraDataChart1.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))));
            this.ultraDataChart1.Name = "ultraDataChart1";
            this.ultraDataChart1.PreviewRect = new Infragistics.Win.DataVisualization.Rectangle(double.PositiveInfinity, double.PositiveInfinity, double.NegativeInfinity, double.NegativeInfinity);
            this.ultraDataChart1.Size = new System.Drawing.Size(776, 300);
            this.ultraDataChart1.TabIndex = 0;
            this.ultraDataChart1.Text = "ultraDataChart1";
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
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 102);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(773, 23);
            this.progressBar1.TabIndex = 3;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.processBtn);
            this.Controls.Add(this.chooseDatasetTest);
            this.Controls.Add(this.chooseDatasetTrain);
            this.Controls.Add(this.testDatasetPath);
            this.Controls.Add(this.trainDataSetPath);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.testDatasetLbl);
            this.Controls.Add(this.trainDatasetLbl);
            this.Controls.Add(this.ultraDataChart1);
            this.Name = "Form1";
            this.Text = "Process CSV";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataChart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.DataVisualization.UltraDataChart ultraDataChart1;
        private System.Windows.Forms.Label trainDatasetLbl;
        private System.Windows.Forms.Label testDatasetLbl;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox trainDataSetPath;
        private System.Windows.Forms.TextBox testDatasetPath;
        private System.Windows.Forms.Button chooseDatasetTrain;
        private System.Windows.Forms.Button chooseDatasetTest;
        private System.Windows.Forms.Button processBtn;
    }
}

