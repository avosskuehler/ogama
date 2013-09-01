using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ogama.Modules.Rta
{
    public partial class ProgressDialog : Form, IRtaControllerListener
    {
        private CountdownLatch countdownLatch;
        private List<string> filterNames = null;

        /// <summary>
        /// 
        /// </summary>
        public ProgressDialog()
        {
            InitializeComponent();
            
        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {

        }

        public void start()
        {
            this.countdownLatch = new CountdownLatch(1);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.RunWorkerAsync();
            this.ShowDialog();
           
        }

        private void fetchAvailbleVideoFilterNames()
        {

            RtaController rtaController = new RtaController();
            rtaController.register(this);
            this.filterNames = rtaController.getAvailbleVideoFilterNames();
        }

      

        public void onVideoFilterNameDetectionProgress(int currentItem, int maxItems, string currentItemName)
        {
            this.backgroundWorker1.WorkerReportsProgress = true;
            int progress = (100 / maxItems) * currentItem;
            if (progress != 0)
            {
                ProgressDto dto = new ProgressDto();
                dto.currentItem = currentItemName;
                dto.maxItems = ""+maxItems;
                     
                this.backgroundWorker1.ReportProgress(progress,dto);
            }
            
        }

      

        public List<string> getFilterNames()
        {
            return this.filterNames;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            fetchAvailbleVideoFilterNames();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispose();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            ProgressDto dto = (ProgressDto)e.UserState;
            this.labelMaxItems.Text = dto.maxItems;
            this.labelCurrentVideoFilter.Text = dto.currentItem;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }





        class ProgressDto
        {
            public string maxItems;
            public string currentItem;
        }
       
    }
}
