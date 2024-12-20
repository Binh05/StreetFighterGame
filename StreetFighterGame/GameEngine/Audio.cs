using NAudio.Wave;
using System;
using System.Threading;
using System.Windows.Forms;

namespace StreetFighterGame.GameEngine
{
    public class Audio
    {
        private string audioFilePath = ".\\sound\\PunchHit1.wav"; // Đường dẫn đến file âm thanh
        private Control audioControl;

        public Audio(Control control)
        {
            audioControl = control;
        }

        public void PlaySound()
        {
            // Chạy phát âm thanh trên một thread khác để không ảnh hưởng tới UI
            ThreadPool.QueueUserWorkItem(_ => PlayNewInstance());
        }

        private void PlayNewInstance()
        {
            try
            {
                using (var waveOut = new WaveOutEvent())
                using (var audioFile = new AudioFileReader(audioFilePath))
                {
                    waveOut.Init(audioFile);
                    waveOut.Play();

                    // Chờ cho đến khi âm thanh kết thúc
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                if (audioControl.InvokeRequired)
                {
                    audioControl.Invoke(new Action(() =>
                        MessageBox.Show("Lỗi phát âm thanh: " + ex.Message)));
                }
                else
                {
                    MessageBox.Show("Lỗi phát âm thanh: " + ex.Message);
                }
            }
        }
    }
}
