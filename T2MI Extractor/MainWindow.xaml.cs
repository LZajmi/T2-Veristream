using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace T2MI_Extractor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ExecuteCommandSync(string exePath, string arguments)
        {
            try
            {
                string workingDir = Path.GetDirectoryName(txt_destination.Text);
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = arguments,
                    WorkingDirectory = workingDir,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (var proc = new System.Diagnostics.Process { StartInfo = procStartInfo })
                {
                    proc.Start();
                    string output = proc.StandardOutput.ReadToEnd();
                    string error = proc.StandardError.ReadToEnd();
                    proc.WaitForExit();
                    Console1.AppendText(output);
                    if (!string.IsNullOrEmpty(error))
                        Console1.AppendText("\nERROR:\n" + error);
                }
            }
            catch (Exception ex)
            {
                Console1.AppendText("Exception: " + ex.Message + "\n");
            }
        }

        private void btn_analyzer_Click(object sender, RoutedEventArgs e)
        {
            Console1.Document.Blocks.Clear();
            string filePath = "\"" + txt_destination.Text + "\"";
            ExecuteCommandSync("tsanalyze.exe", filePath);
        }

        private void btn_extract_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "\"" + txt_destination.Text + "\"";
            string outputFile = "\"extr_" + Path.GetFileName(txt_destination.Text) + "\"";
            ExecuteCommandSync("tsp.exe", $"-I file {filePath} -P t2mi -O file {outputFile}");
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            txt_destination.Text = Globals.OFDbutton();
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_destination.Text))
            {
                Console1.AppendText("No file selected. Please browse for a file first.\n");
                return;
            }

            string sourceDir = Path.GetDirectoryName(txt_destination.Text);
            string sourceFileName = Path.GetFileName(txt_destination.Text);
            string outputFile = "extr_" + sourceFileName;
            string outputPath = Path.Combine(sourceDir, outputFile);

            Console1.AppendText("Looking for: " + outputPath + "\n");

            if (!File.Exists(outputPath))
            {
                Console1.AppendText("No extracted file found. Please extract first.\n");
                return;
            }

            try
            {
                string vlcPath = @"C:\Program Files\VideoLAN\VLC\vlc.exe";

                if (!File.Exists(vlcPath))
                {
                    Console1.AppendText("VLC not found. Please install VLC or check the path.\n");
                    return;
                }

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = vlcPath,
                    Arguments = "\"" + outputPath + "\"",
                    UseShellExecute = false
                });
            }
            catch (Exception ex)
            {
                Console1.AppendText("Playback error: " + ex.Message + "\n");
            }
        }
    }
}