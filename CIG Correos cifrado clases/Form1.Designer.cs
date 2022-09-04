namespace CIG_Correos_cifrado_clases
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.ficheiro = new System.Windows.Forms.TextBox();
            this.cifrar = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.PictureBox();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(12, 189);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(132, 23);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "Selecciona ficheiro";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // ficheiro
            // 
            this.ficheiro.Location = new System.Drawing.Point(12, 218);
            this.ficheiro.Name = "ficheiro";
            this.ficheiro.ReadOnly = true;
            this.ficheiro.Size = new System.Drawing.Size(306, 23);
            this.ficheiro.TabIndex = 2;
            // 
            // cifrar
            // 
            this.cifrar.Enabled = false;
            this.cifrar.Location = new System.Drawing.Point(243, 258);
            this.cifrar.Name = "cifrar";
            this.cifrar.Size = new System.Drawing.Size(75, 23);
            this.cifrar.TabIndex = 3;
            this.cifrar.Text = "Cifrar";
            this.cifrar.UseVisualStyleBackColor = true;
            this.cifrar.Click += new System.EventHandler(this.cifrar_Click);
            // 
            // logo
            // 
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(12, 15);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(306, 158);
            this.logo.TabIndex = 4;
            this.logo.TabStop = false;
            // 
            // password
            // 
            this.password.Enabled = false;
            this.password.Location = new System.Drawing.Point(88, 259);
            this.password.Name = "password";
            this.password.ReadOnly = true;
            this.password.Size = new System.Drawing.Size(149, 23);
            this.password.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Contrasinal";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(-1, 300);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(332, 11);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 309);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.cifrar);
            this.Controls.Add(this.ficheiro);
            this.Controls.Add(this.btnSelectFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CIG Correos - Cifrado clases";
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private Button btnSelectFile;
        private TextBox ficheiro;
        private Button cifrar;
        private PictureBox logo;
        private TextBox password;
        private Label label1;
        private ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}