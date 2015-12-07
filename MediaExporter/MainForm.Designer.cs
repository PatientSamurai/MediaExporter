namespace MediaExporter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelSourcePath = new System.Windows.Forms.Label();
            this.textBoxSourcePath = new System.Windows.Forms.TextBox();
            this.buttonOpenSourcePath = new System.Windows.Forms.Button();
            this.openFileDialogSourcePath = new System.Windows.Forms.OpenFileDialog();
            this.labelDestinationPath = new System.Windows.Forms.Label();
            this.textBoxDestinationPath = new System.Windows.Forms.TextBox();
            this.buttonOpenDestinationPath = new System.Windows.Forms.Button();
            this.openFileDialogDestinationPath = new System.Windows.Forms.OpenFileDialog();
            this.buttonExport = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelSourcePath
            // 
            this.labelSourcePath.AutoSize = true;
            this.labelSourcePath.Location = new System.Drawing.Point(31, 16);
            this.labelSourcePath.Name = "labelSourcePath";
            this.labelSourcePath.Size = new System.Drawing.Size(68, 13);
            this.labelSourcePath.TabIndex = 0;
            this.labelSourcePath.Text = "Source path:";
            // 
            // textBoxSourcePath
            // 
            this.textBoxSourcePath.Location = new System.Drawing.Point(105, 13);
            this.textBoxSourcePath.Name = "textBoxSourcePath";
            this.textBoxSourcePath.Size = new System.Drawing.Size(267, 20);
            this.textBoxSourcePath.TabIndex = 1;
            // 
            // buttonOpenSourcePath
            // 
            this.buttonOpenSourcePath.Location = new System.Drawing.Point(378, 11);
            this.buttonOpenSourcePath.Name = "buttonOpenSourcePath";
            this.buttonOpenSourcePath.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenSourcePath.TabIndex = 2;
            this.buttonOpenSourcePath.Text = "Open";
            this.buttonOpenSourcePath.UseVisualStyleBackColor = true;
            this.buttonOpenSourcePath.Click += new System.EventHandler(this.buttonOpenSourcePath_Click);
            // 
            // openFileDialogSourcePath
            // 
            this.openFileDialogSourcePath.AddExtension = false;
            this.openFileDialogSourcePath.CheckFileExists = false;
            this.openFileDialogSourcePath.FileName = "File Name Ignored";
            // 
            // labelDestinationPath
            // 
            this.labelDestinationPath.AutoSize = true;
            this.labelDestinationPath.Location = new System.Drawing.Point(12, 45);
            this.labelDestinationPath.Name = "labelDestinationPath";
            this.labelDestinationPath.Size = new System.Drawing.Size(87, 13);
            this.labelDestinationPath.TabIndex = 3;
            this.labelDestinationPath.Text = "Destination path:";
            // 
            // textBoxDestinationPath
            // 
            this.textBoxDestinationPath.Location = new System.Drawing.Point(105, 42);
            this.textBoxDestinationPath.Name = "textBoxDestinationPath";
            this.textBoxDestinationPath.Size = new System.Drawing.Size(267, 20);
            this.textBoxDestinationPath.TabIndex = 4;
            // 
            // buttonOpenDestinationPath
            // 
            this.buttonOpenDestinationPath.Location = new System.Drawing.Point(378, 40);
            this.buttonOpenDestinationPath.Name = "buttonOpenDestinationPath";
            this.buttonOpenDestinationPath.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenDestinationPath.TabIndex = 5;
            this.buttonOpenDestinationPath.Text = "Open";
            this.buttonOpenDestinationPath.UseVisualStyleBackColor = true;
            this.buttonOpenDestinationPath.Click += new System.EventHandler(this.buttonOpenDestinationPath_Click);
            // 
            // openFileDialogDestinationPath
            // 
            this.openFileDialogDestinationPath.AddExtension = false;
            this.openFileDialogDestinationPath.CheckFileExists = false;
            this.openFileDialogDestinationPath.FileName = "File Name Ignored";
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(459, 11);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 6;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(12, 69);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(519, 286);
            this.textBoxOutput.TabIndex = 7;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Enabled = false;
            this.buttonAbort.Location = new System.Drawing.Point(459, 40);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(75, 23);
            this.buttonAbort.TabIndex = 8;
            this.buttonAbort.Text = "Abort";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 367);
            this.Controls.Add(this.buttonAbort);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonOpenDestinationPath);
            this.Controls.Add(this.textBoxDestinationPath);
            this.Controls.Add(this.labelDestinationPath);
            this.Controls.Add(this.buttonOpenSourcePath);
            this.Controls.Add(this.textBoxSourcePath);
            this.Controls.Add(this.labelSourcePath);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSourcePath;
        private System.Windows.Forms.TextBox textBoxSourcePath;
        private System.Windows.Forms.Button buttonOpenSourcePath;
        private System.Windows.Forms.OpenFileDialog openFileDialogSourcePath;
        private System.Windows.Forms.Label labelDestinationPath;
        private System.Windows.Forms.TextBox textBoxDestinationPath;
        private System.Windows.Forms.Button buttonOpenDestinationPath;
        private System.Windows.Forms.OpenFileDialog openFileDialogDestinationPath;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button buttonAbort;
    }
}

