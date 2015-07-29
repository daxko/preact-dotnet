using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Preact;

namespace Preact_Api_Consumer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region Log Event
        
        private void btnLogEvent_Click(object sender, EventArgs e)
        {
            CreateLogEvent();
        }

        private void CreateLogEvent()
        {
            outputTextbox.AppendText("Logging event\n");
            Preact.Api api = new Preact.Api(codeTextbox.Text, secretTextbox.Text);
            try
            {
                Preact.ActionEventCreateRequest request = new Preact.ActionEventCreateRequest()
                    {
                        Account = new Preact.Account()
                        {
                            Id = accountIdTextbox.Text,
							Name = accountNameTextbox.Text,
                            Properties = new Dictionary<string, object> {{"Lifetime Sent Emails",1000}}
                        },
						Event = new ActionEvent
						{
							Name = eventNameTextbox.Text
						}
                    };
                outputTextbox.AppendText(JsonConvert.SerializeObject(request,Formatting.Indented) + "\n");
                api.LogEvent(request);
            }
            catch (Exception ex)
            {
                outputTextbox.AppendText("Log failure:" + ex.Message);
                outputTextbox.AppendText(ex.StackTrace);
            }
        }

        #endregion

        private void btnLogBackgroundSignal_Click(object sender, EventArgs e)
        {
            outputTextbox.AppendText("Logging signal\n");
            Preact.Api api = new Preact.Api(codeTextbox.Text, secretTextbox.Text);

            try {
                var month = 1;
                var day = 1;
                var timeSpan = DateTime.UtcNow.AddDays(-3) - new DateTime(1970, month, day);

                Preact.BackgroundSignalRequest request = new Preact.BackgroundSignalRequest()
                    {
                        AccountId = accountIdTextbox.Text,
                        Name = signalNameTextbox.Text,
                        Value = int.Parse(signalValueTextbox.Text),
                        Timestamp = timeSpan.TotalSeconds
                    };
                outputTextbox.AppendText(JsonConvert.SerializeObject(request, Formatting.Indented) + "\n");
                api.LogBackgroundSignal(request);
            } 
            catch (Exception ex)
            {
                outputTextbox.AppendText("Log failure:" + ex.Message);
                outputTextbox.AppendText(ex.StackTrace);
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
