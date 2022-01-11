using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ReactiveProfiler
{
    public partial class MainForm : Form
    {
        private BackgroundWorker _worker = new BackgroundWorker();
        public MainForm()
        {
            InitializeComponent();

            txtPath.Text =
                @"C:\Users\USER\source\repos\ExperimentalImplementation\DiagnosticLogging\bin\Debug\DiagnosticLogging.exe";

            _worker.WorkerReportsProgress = true;
            _worker.ProgressChanged += WorkerOnProgressChanged;
            _worker.DoWork += WorkerOnDoWork;
            _worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;
        }

        private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStart.Enabled = true;
        }

        private void WorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtLog.AppendText($"{e.ProgressPercentage}ミリ秒\r\n");
            txtLog.ScrollToCaret();
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Queue<int> queue = new Queue<int>();
                var interval = 5;
                int moratorium = 200;
                int queueMaxCount = (int)(moratorium / interval);
                int cutOff = 100;
                int totalTick = 0;
                bool isFirst = true;
                Process p = Process.Start(e.Argument.ToString());
                int id = p.Id;

                while (true)
                {
                    p = Process.GetProcessById(id);

                    var tick = GetElapsedMillisecond(p.MainWindowHandle);
                    totalTick += tick;

                    queue.Enqueue(tick);
                    if (queue.Count == queueMaxCount)
                    {
                        var max = queue.Max();
                        if (max < cutOff)
                        {
                            if (totalTick > cutOff)
                            {
                                if (isFirst)
                                {
                                    // 起動して最初の時間はプロセス開始時刻から求める
                                    totalTick = (int)(DateTime.Now - p.StartTime).TotalMilliseconds;
                                    isFirst = false;
                                }
                                // 猶予時間中に経過した分を引く
                                totalTick -= queue.Sum();
                                _worker.ReportProgress(totalTick);
                            }
                            totalTick = 0;
                        }
                        queue.Dequeue();
                    }

                    Thread.Sleep(interval);
                }
            }
            catch 
            {
                // 終了
            }
        }


        private int GetElapsedMillisecond(IntPtr pMainWindowHandle)
        {
            int start = Environment.TickCount;
            NativeMethods.SendMessage(pMainWindowHandle, 0, IntPtr.Zero, IntPtr.Zero);
            return Environment.TickCount - start;
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            txtLog.Clear();
            _worker.RunWorkerAsync(txtPath.Text.Trim('"'));
        }

    }
}
