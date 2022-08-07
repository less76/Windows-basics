using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Http.Headers;

namespace DCD_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Full = 1;
        }

        private Int32 full;
        byte[] forwardsWavFileStreamByteArray;
        private Int32 Full
        {
            get => full;

            set => full = value;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            textBox1.Text = trackBar1.Value == 0 ? textBox1.Text = null : Convert.ToString(trackBar1.Value);

            textBox2.Text = trackBar2.Value == 0 ? textBox2.Text = null : Convert.ToString(trackBar2.Value);

            textBox3.Text = trackBar3.Value == 0 ? textBox3.Text = null  : Convert.ToString(trackBar3.Value);

            BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //frequency textbox4
            //duration textbox5
            if(textBox4.Text != "" && textBox5.Text != "")
            {
                Console.Beep(Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox5.Text));
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(this.Full == 1)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                TopMost = true;
                button3.Text = "Full Screen";
            }

            if(this.Full == 0)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                TopMost = false;
                button3.Text = "Normal Screen";
            }


            this.Full = 1-this.Full;
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            /*for(Int32 step = Convert.ToInt32(textBox7.Text); step < Convert.ToInt32(textBox6.Text); step++)
            {
                Console.Beep(step, Convert.ToInt32(textBox9.Text));
                step = step + Convert.ToInt32(textBox8.Text);
            }*/

            
            //using (MemoryStream ms = new MemoryStream(forwardsWavFileStreamByteArray)) //can perform DSP
            //{

            //song_data_read song = new song_data_read();
            //using (MemoryStream ms = new MemoryStream(song.saved_song)) //can perform DSP
            //{
                // Construct the sound player
              //  SoundPlayer player = new SoundPlayer(ms);
               // player.PlayLooping();
            //}
            SoundPlayer player = new SoundPlayer(DCD_test.Properties.Resources._127_to_4572);
            //player.SoundLocation = @"E:\DCD sounds\127 to 4572.wav";
            player.PlayLooping();
            //ProcessStartInfo proStartInfo = new ProcessStartInfo("aplay test.wav", "-c 'ls -l'");


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trackBar1.Value = Convert.ToInt32(textBox1.Text);
                
            }

            catch
            {
                if(string.IsNullOrEmpty(textBox1.Text))
                {
                    trackBar1.Value = 0;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trackBar2.Value = Convert.ToInt32(textBox2.Text);
            }

            catch
            {
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    trackBar2.Value = 0;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trackBar3.Value = Convert.ToInt32(textBox3.Text);
            }

            catch
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    trackBar3.Value = 0;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string mac = null;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up && (!nic.Description.Contains("Virtual") && !nic.Description.Contains("Pseudo")))
                {
                    if (nic.GetPhysicalAddress().ToString() != "")
                    {
                        mac = nic.GetPhysicalAddress().ToString();
                        char[] mac_word = mac.ToCharArray();

                        for (int i = 5; i >= 0; i--)
                        {
                            if (i == 5)
                            {
                                textBox10.Text = "" + mac_word[2 * i] + mac_word[(2 * i) + 1];
                            }
                            else
                            {
                                textBox10.Text = "" + mac_word[2 * i] + mac_word[(2 * i) + 1] + ":" + textBox10.Text;
                            }

                        }
                    }
                }
            }

            if(mac == null)
            {
                MessageBox.Show("Connect to a device with an IP address");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.Stop();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //textBox6.Text = Dns.GetHostByName(Dns.GetHostName()).AddressList[1].ToString();
        }

        private static byte[] PopulateForwardsWavFileByteArray(string forwardsWavFilePath)
        {
            byte[] forwardsWavFileStreamByteArray;
            using (FileStream forwardsWavFileStream = new FileStream(forwardsWavFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                forwardsWavFileStreamByteArray = new byte[forwardsWavFileStream.Length];
                forwardsWavFileStream.Read(forwardsWavFileStreamByteArray, 0, (int)forwardsWavFileStream.Length);
                //int g1 = 0;
            }
            return forwardsWavFileStreamByteArray;
        }

        private void button7_Click(object sender, EventArgs e)
        { 
            string forwardsWavFilePath = @"E:\DCD sounds\127 to 4572.wav";
            forwardsWavFileStreamByteArray = PopulateForwardsWavFileByteArray(forwardsWavFilePath);
            //Properties.Settings.Default.music = forwardsWavFileStreamByteArray;
            //string[] arrayread = new string[forwardsWavFileStreamByteArray.Length];

            //for(int i=0; i < forwardsWavFileStreamByteArray.Length; i++)
            //{
              //  arrayread[i] = Convert.ToString(forwardsWavFileStreamByteArray[i]);
            //}

            //File.WriteAllLines("output_data.csv", arrayread);
           // int gg = 0;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            float high, low, medium;
            high = 13.291125f;
            low = 13.158875f;
            medium = 13.291125f;

            trackBar4.Value = Convert.ToInt32((1 - (((high - medium) - (medium - low)) / (high - low)))*127.5);
            //int gg = 0;
        }
    }
}
