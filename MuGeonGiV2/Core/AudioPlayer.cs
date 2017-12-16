using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using CSCore.Codecs;
using CSCore.Codecs.AAC;
using CSCore.Streams;
using CSCore.Streams.SampleConverter;

namespace MuGeonGiV2.Core
{
    public class AudioPlayer: Instrument
    {
        private readonly EditableStream Stream;
        private IWaveSource AudioSource;
        
        public AudioPlayer()
        {
            Stream = new EditableStream(Read);
            OutputJack = new OutputJack(Stream);
        }

        public void SetFile(string filePath)
        {
            AudioSource = CodecFactory.Instance.GetCodec(filePath)
                .ChangeSampleRate(44100)
                .ToSampleSource()
                .ToWaveSource(16);
            if (AudioSource.WaveFormat.Channels == 2)
            {
                AudioSource = AudioSource.ToMono();
            }
            AudioSource.Position = AudioSource.Length;
        }

        public void Play()
        {
            AudioSource.Position = 0;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            if (AudioSource != null)
            {
                var read = AudioSource.Read(buffer, offset, count);
                var availableLength = (AudioSource.Length - AudioSource.Position);
                Console.WriteLine($"{count}-{read}-{availableLength}");
                if (availableLength <= 0)
                {
                    AudioSource.Position = AudioSource.Length;
                    Array.Clear(buffer, offset + read + (int)availableLength, count - read);
                    return count;
                }
                if (read != 0)
                {
                    return read;
                }
                if (availableLength > count)
                {
                    return 0;
                }
            }
            Array.Clear(buffer, offset, count);
            return count;
        }
    }
}
