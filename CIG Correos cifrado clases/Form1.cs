using ICSharpCode.SharpZipLib.Zip;
using NLog;
using System.ComponentModel;

namespace CIG_Correos_cifrado_clases
{
    public partial class Form1 : Form
    {
        private readonly NLog.Logger logger;

        public Form1()
        {
            InitializeComponent();

            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork); ;
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.WorkerReportsProgress = true;

            //// Log, para ter um .exe e nom depender do nlog.config creo programaticamente aqui
            //// @Todo revisar para quitar a clase propia
            //NLog.LogManager.Setup().LoadConfiguration(builder => {
            //    //builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "${currentdir}/cigcorreos.txt");
            //    //builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToFile(fileName: "${currentdir}/cigcorreos.txt");
            //});
            
            logger = NLog.LogManager.GetCurrentClassLogger();
        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // extensiom .zip 
            var filename = Path.ChangeExtension(ficheiro.Text, ".zip");

            if (e.Cancelled == true)
            {
                MessageBox.Show("Cifrado cancelado de " + filename, "Cifrado cancelado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Se ha producido un error al cifrar " + filename +": " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Cifrado finalizado correctamente de " + filename, "Cifrado ok!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // restauramos openFileDialog e baleiramos datos
                btnSelectFile.Enabled = true;

                password.Text = "";
                ficheiro.Text = "";
            }
        }

        /**
         * zipea en background
         * 
         * imos gardar mediante buffer o ficheiro, pois pesam bastante e permitenos um progressbar
         * aínda que valía cum CopyTo() do itemStream, pero nom nos dá feedback
         */
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;


            // extensiom .zip 
            var filename = Path.ChangeExtension(ficheiro.Text, ".zip");

            var sizeFilenameOpened = new FileInfo(ficheiro.Text).Length;
            long sizeFilenameWriting = 0;
            logger.Info("tamanho do ficheiro seleccionado em sizeFilenameOpened: {sizeFilenameOpened}", sizeFilenameOpened);

            // ripped from https://stackoverflow.com/questions/28235804/compress-large-file-using-sharpziplib-causing-out-of-memory-exception
            using (var zipFileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            using (ZipOutputStream zipStream = new ZipOutputStream(zipFileStream))
            {
                //0-9, 9 being the highest level of compression
                zipStream.SetLevel(3);
                logger.Info("zipStream.SetLevel({level})", zipStream.GetLevel());

                // optional. Null is the same as not setting. Required if using AES.
                // -- comprobamos que nom estea baleiro
                if (!String.IsNullOrEmpty(password.Text))
                {
                    logger.Info("seteamos password {password}", password.Text);
                    zipStream.Password = password.Text;
                }


                const int BufferSize = 8192;
                byte[] buffer = new byte[BufferSize];

                List<int> listSleep = new List<int>();

                // aqui abro o ficheiro orixinal para ir lendo (se queda flipado meter um buffer?)
                using (var itemStream = new FileStream(ficheiro.Text, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    logger.Info("escribindo ficheiro");

                    var entry = new ZipEntry(Path.GetFileName(ficheiro.Text));
                    logger.Info("entry: {entry}", entry);

                    zipStream.PutNextEntry(entry);
                    //itemStream.CopyTo(zipStream); // todo o read() de buffer sobra empregando so isto, so teria que calcular o porcentagem da progressBar


                    int read;
                    while ((read = itemStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        sizeFilenameWriting += BufferSize;
                        logger.Debug("dentro de while(), sizeFilenameWriting: {sizeFilenameWriting}", sizeFilenameWriting);

                        int tantoPorCientoProgressbar = (int)(sizeFilenameWriting / (sizeFilenameOpened / 100));

                        logger.Debug("sizeFilenameWriting / (sizeFilenameOpened / 100): {tantoPorCientoProgressbar}", tantoPorCientoProgressbar);
                        if (tantoPorCientoProgressbar > 100)
                        {
                            // workaround:
                            logger.Debug("Forzamos tantoPorCientoProgressbar a 100 para que nom dea umha exception");
                            tantoPorCientoProgressbar = 100;
                        }

                        worker.ReportProgress(tantoPorCientoProgressbar);

                        zipStream.Write(buffer, 0, read);
                    }

                }
                zipStream.Finish();
                logger.Info("Escrito ficheiro!");
            }
        }


        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            ficheiro.Text = "";
            password.Text = "";

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

        private void cifrar_Click(object sender, EventArgs e)
        {
            logger.Info("------");
            logger.Info("ficheiro.Text: {ficheiro.Text}", ficheiro.Text);
            logger.Info("password.Text: {password.Text} ", password.Text);

            // dessativamos mentres ciframos
            password.ReadOnly = true;
            cifrar.Enabled = false;
            btnSelectFile.Enabled = false;

            // sem password avisamos que vai sem password!!!
            if (password.Text == "")
            {
                DialogResult result = MessageBox.Show("O ficheiro vai ir sen cifrado, só comprimido!", "Sen cifrado", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                if(result != DialogResult.OK)
                {
                    logger.Info("Non se deu a OK para cifrar sem password!. Saindo do programa.");
                    return;
                }
            }


            // Start the asynchronous operation.
            backgroundWorker1.RunWorkerAsync();
        }
    }
}