using ICSharpCode.SharpZipLib.Zip;

namespace CIG_Correos_cifrado_clases
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ficheiro.Text = "";
            password.Text = "";

            ficheiro.ReadOnly = true;
            password.ReadOnly = true;
            cifrar.Enabled = false;


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
                ficheiro.Text = openFileDialog1.FileName;

                cifrar.Enabled = true;
                password.Enabled = true;
                password.ReadOnly = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // sem password avisamos que vai sem password!!!
            if(password.Text == "")
            {
                DialogResult result = MessageBox.Show("O ficheiro vai ir sen cifrado, só comprimido!", "Sen cifrado", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                if(result != DialogResult.OK)
                {
                    return;
                }
            }


            // extensiom .zip 
            var filename = Path.ChangeExtension(ficheiro.Text, ".zip");


            // ripped from https://stackoverflow.com/questions/28235804/compress-large-file-using-sharpziplib-causing-out-of-memory-exception
            using (var zipFileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            using (ZipOutputStream zipStream = new ZipOutputStream(zipFileStream))
            {
                //0-9, 9 being the highest level of compression
                zipStream.SetLevel(3);

                // optional. Null is the same as not setting. Required if using AES.
                // -- comprobamos que nom estea baleiro
                if (!String.IsNullOrEmpty(password.Text))
                {
                    zipStream.Password = password.Text;
                }

                // aqui abro o ficheiro orixinal para ir lendo (se queda flipado meter um buffer?)
                using (var itemStream = new FileStream(ficheiro.Text, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var entry = new ZipEntry(Path.GetFileName(filename));
                    zipStream.PutNextEntry(entry);
                    itemStream.CopyTo(zipStream);
                }
                zipStream.Finish();
            }
        }
    }
}