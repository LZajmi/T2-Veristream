using System;
using System.CodeDom;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Controls;

namespace T2MI_Extractor
{
    public class CmdService : IDisposable
    {

        private Process cmdProcess;

        private StreamWriter streamWriter;
        private AutoResetEvent outputWaitHandle;
        private string cmdOutput;

        public CmdService(string cmdPath)
        {
            cmdProcess = new Process();
            outputWaitHandle = new AutoResetEvent(false);
            cmdOutput = String.Empty;

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = cmdPath;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.CreateNoWindow = true;

            cmdProcess.OutputDataReceived += CmdProcess_OutputDataReceived;

            cmdProcess.StartInfo = processStartInfo;
            cmdProcess.Start();

            streamWriter = cmdProcess.StandardInput;
            cmdProcess.BeginOutputReadLine();

        }

        public void Dispose()
        {
            cmdProcess.Close();

            cmdProcess.Dispose();
            streamWriter.Close();
            streamWriter.Dispose();
        }

        public string ExecuteCommand(string command, RichTextBox Console1)
        {
            cmdOutput = String.Empty;

            streamWriter.WriteLine(command);
            
            streamWriter.WriteLine("echo end");
            Console1.AppendText(cmdOutput);

            outputWaitHandle.WaitOne();
            
            return cmdOutput;

        }

        private void CmdProcess_OutputDataReceived(object sender, DataReceivedEventArgs e) 
        {
            if (e.Data == null || e.Data == "end")
                outputWaitHandle.Set();
            else
                cmdOutput += e.Data + Environment.NewLine;
        }
    }
}
