using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBaseCode
{
    public partial class ProgressBarTest : Form
    {
        public ProgressBarTest()
        {
            InitializeComponent();
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
        }

        

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Thread th = new Thread(InProgressBarInvoke);
            //th.Start();

            InProgressBarInvoke();
        }

        private void InProgressBarInvoke()
        {
            InProgressDelegate invoker = new InProgressDelegate(Progress);
            invoker.BeginInvoke(new AsyncCallback(CallBackInvoke), invoker);
            
        }

        private void Progress()
        {
            Action<int> uiInvoker = SetProgress;
            Action<int> textInvoker = SetTextBox;

            for (int i = 0; i <= 100; i++)
            {
                progressBar1.Invoke(uiInvoker, i);
                Thread.Sleep(10);
                textBox1.Invoke(textInvoker, i);
            }

           // MessageBox.Show("끝");
        }

        private void SetTextBox(int progress)
        {
            textBox1.Text = progress.ToString();
        }

        private void SetProgress(int progress)
        {
            progressBar1.Value = progress;
        }

        void CallBackInvoke(IAsyncResult iar)
        {
            InProgressDelegate result =  iar.AsyncState as InProgressDelegate;

            result.EndInvoke(iar);

        }


        private delegate void InProgressDelegate();



    }
}
