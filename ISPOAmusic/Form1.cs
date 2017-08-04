using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ISPOAmusic
{
    public partial class Form1 : Form
    {
        bool inPlay = false;
        bool isStopped = false;
        double curpos = 0;
        
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();

        bool playpause(bool test)
        {
            if (test == true)
            {
                player.controls.pause();
                play_btn.Text = "Play";
                curpos = player.controls.currentPosition;
                label1.Text = "Paused";
                return false;
            }
/*
            else if ((test == true) && (isStopped == true))
            {
                player.URL = textBox1.Text;
                player.controls.play();
                play_btn.Text = "Pause";
                label1.Text = "In Play";
                test = true;
                return test;
            }
*/
            else
            {
                player.URL = textBox1.Text;
                player.controls.play();
                label2.Text = player.controls.currentPosition.ToString();
                player.controls.currentPosition = curpos;
                play_btn.Text = "Pause";
                label1.Text = "In Play" + progressBar1.Maximum;
                test = true;
                
                return true;
            }
        }

        public Form1()
        {
            InitializeComponent();
            label1.Text = "Not Started";
            inPlay = false;
            stop_btn.Enabled = false;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fw_btn.Enabled = false;
            rew_btn.Enabled = false;
            mute_btn.Enabled = false;
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MP3 files (*.mp3)|*.mp3|All Files (*.*)|*.*";
            ofd.ShowDialog();
            textBox1.Text = ofd.FileName;
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "") {
                    MessageBox.Show("Error, Choose a file first");
                    return;
                }
                inPlay = playpause(inPlay);
                stop_btn.Enabled = true;
                timer1.Start();
                fw_btn.Enabled = true;
                rew_btn.Enabled = true;
                mute_btn.Enabled = true;
                //if (player != null)
                //{
                //    progressBar1.Maximum = (int)(player.controls.currentItem.duration * 60);
                //    progressBar1.Minimum = 0;
                //}
                
               
                
            }
            catch(Win32Exception exp){
                MessageBox.Show(exp.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            player.controls.stop();
            label1.Text = "Stopped";
            //make cur pos 0
            player.controls.currentPosition = 0;
            stop_btn.Enabled = false;
            isStopped = true;
            play_btn.Text = "Play";
            inPlay = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (player!=null) 
            { label2.Text = player.controls.currentPositionString + "/" + player.controls.currentItem.durationString;
            //check for picture in foler
            //and make pictureBox1.source = image
            
            label3.Text = handleProgress().ToString();
            progressBar1.Value = handleProgress();
            if (progressBar1.Value == 100) { progressBar1.Value = 0; }
            //code to stop and display stopped
            //implement with delegates and eventhandlers
            //implement functionality on progress bar
            //album art
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            player.controls.fastForward();
            double fwdpos = player.controls.currentPosition + 5;
            player.controls.play();
            player.controls.currentPosition = fwdpos;
        }

        private void rew_btn_Click(object sender, EventArgs e)
        {
            player.controls.fastReverse();
            double rewpos = player.controls.currentPosition - 5;
            player.controls.play();
            player.controls.currentPosition = rewpos;
        }

        private int handleProgress(){
	        int percent = (int)((player.controls.currentPosition / player.controls.currentItem.duration) * 100);
            return percent;
        }


        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }

   
}
