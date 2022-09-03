namespace CIG_Correos_cifrado_clases
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                //InitialDirectory = @"D:\",
                Title = "Busca o ficheiro .mp4 da clase",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "mp4",
                Filter = "ficheiros mp4 (*.mp4)|*.mp4",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("zip.exe", "-e ", textBox1.Text);
        }
    }
}