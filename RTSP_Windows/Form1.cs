using RtspClientSharp;
using SimpleRtspPlayer.GUI;
using SimpleRtspPlayer.RawFramesReceiving;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RTSP_Windows
{
    public partial class Form1 : Form
    {
        private readonly RealtimeVideoSource _realtimeVideoSource = new RealtimeVideoSource();
        private IRawFramesSource _rawFramesSource;
        public event EventHandler<string> StatusChanged;
        public IVideoSource VideoSource => _realtimeVideoSource;

        public Form1()
        {
            InitializeComponent();
            
    }

        public void Start(ConnectionParameters connectionParameters)
        {
            if (_rawFramesSource != null)
                return;

            _rawFramesSource = new RawFramesSource(connectionParameters);
            _rawFramesSource.ConnectionStatusChanged += ConnectionStatusChanged;

            _realtimeVideoSource.SetRawFramesSource(_rawFramesSource);

            _rawFramesSource.Start();
        }

        public void Stop()
        {
            if (_rawFramesSource == null)
                return;

            _rawFramesSource.Stop();
            _realtimeVideoSource.SetRawFramesSource(null);
            _rawFramesSource = null;
        }

        private void ConnectionStatusChanged(object sender, string s)
        {
            StatusChanged?.Invoke(this, s);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
