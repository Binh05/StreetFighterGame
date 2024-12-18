/*using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreetFighterGame.GameEngine
{
    static class Audio
    {
        static private WaveOutEvent waveOut;
        static private AudioFileReader audioFile;
        static private string audioFilePath = ".\\sound\\PunchHit1.wav"; // Đường dẫn đến file âm thanh của bạn
        static private bool isPlaying = false;

        private static void PlaySound()
        {
            // Kiểm tra xem có đang phát âm thanh hay không
            if (isPlaying)
            {
                // Nếu đang phát, tạo một instance mới của WaveOutEvent và AudioFileReader để phát lại từ đầu
                ThreadPool.QueueUserWorkItem(_ => PlayNewInstance());
            }
            else
            {
                // Nếu không phát, bắt đầu phát âm thanh
                ThreadPool.QueueUserWorkItem(_ => PlayAudio());
            }
        }
        private static void PlayNewInstance()
        {
            using (var newWaveOut = new WaveOutEvent())
            using (var newAudioFile = new AudioFileReader(audioFilePath))
            {
                newWaveOut.Init(newAudioFile);
                newWaveOut.Play();
                while (newWaveOut.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100); // Đợi cho đến khi âm thanh kết thúc
                }
            }
        }
        private static void PlayAudio()
        {
            try
            {
                isPlaying = true;
                waveOut = new WaveOutEvent();
                audioFile = new AudioFileReader(audioFilePath);
                waveOut.Init(audioFile);
                waveOut.Play();
                waveOut.PlaybackStopped += WaveOut_PlaybackStopped;

                // Đợi cho đến khi âm thanh kết thúc trên một thread riêng để không block UI thread
                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100); // Kiểm tra trạng thái mỗi 100ms
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi phát âm thanh: " + ex.Message);
            }
        }
        private static void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // Đảm bảo rằng isPlaying được đặt thành false khi âm thanh kết thúc
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => isPlaying = false));
            }
            else
            {
                isPlaying = false;
            }
            // Giải phóng tài nguyên
            if (waveOut != null)
            {
                waveOut.Dispose();
                waveOut = null;
            }
            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
        }
    }
}
*/