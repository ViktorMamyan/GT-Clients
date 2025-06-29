using System;
using System.Windows.Forms;

namespace GT_Price_Importer
{
    internal partial class DataLoader : DevExpress.XtraEditors.XtraForm
    {
        internal DataLoader()
        {
            InitializeComponent();
        }

        private System.Timers.Timer MainTimer;

        DateTime startTime;

        private void SetTimer()
        {
            MainTimer = new System.Timers.Timer(1000);

            MainTimer.Elapsed += OnTimedEvent;
            MainTimer.AutoReset = true;
            MainTimer.Enabled = true;
        }

        private void StopTimer()
        {
            MainTimer.Enabled = false;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DateTime endTime = DateTime.Now;
                TimeSpan durationTime = endTime.Subtract(startTime);

                label1.Invoke(new Action(() => label1.Text = durationTime.ToString(@"hh\:mm\:ss")));
            }
            catch (Exception)
            {
            }
        }

        private void DataLoader_Load(object sender, EventArgs e)
        {
            startTime = new DateTime();
            startTime = DateTime.Now;

            SetTimer();
        }

        private void DataLoader_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopTimer();
        }
    }
}