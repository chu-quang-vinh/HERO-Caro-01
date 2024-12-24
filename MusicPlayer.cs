using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp10
{
    public class MusicPlayer
    {
        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;

        public MusicPlayer(string filePath)
        {
            
            waveOutDevice = new WaveOutEvent();
            audioFileReader = new AudioFileReader(filePath);
            waveOutDevice.Init(audioFileReader);
        }

        public void Play()
        {
            waveOutDevice.Play();
        }

        public void Stop()
        {
            waveOutDevice.Stop();
            audioFileReader.Position = 0;  // Reset nhạc về đầu
        }

        public void SetVolume(float volume)
        {
            audioFileReader.Volume = volume;  // Giá trị từ 0.0f (im lặng) đến 1.0f (to nhất)
        }
    }
}
