using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace desktop_sync_contex
{
    public partial class Form1 : Form
    {
        private static HttpClient httpClient = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            httpClient.GetStringAsync("https://www.google.com/").ContinueWith(downloadTask => {
                button1.Text = downloadTask.Result;
            }, TaskScheduler.FromCurrentSynchronizationContext() );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SynchronizationContext sc = SynchronizationContext.Current;
            httpClient.GetStringAsync("https://www.google.com").ContinueWith(downloadTask => {
                sc.Post(delegate {
                    button2.Text = downloadTask.Result;
                }, null);
            });
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // optimized version
            object scheduler = SynchronizationContext.Current;
            if (scheduler is null && TaskScheduler.Current != TaskScheduler.Default)
            {
                scheduler = TaskScheduler.Current;
            }

            string text = await httpClient.GetStringAsync("https://google.com"); //.ConfigureAwait(false); // cause error
            button3.Text = text;

           
        }

    }
}
