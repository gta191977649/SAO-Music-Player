using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using Un4seen.Bass.Misc;
namespace SAO_Player
{
    public class SAOPlayer
    {
        private static Visuals visual = new Un4seen.Bass.Misc.Visuals();
        private int stream;//播放媒体流
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="handle">主窗体句柄</param>
        public SAOPlayer(IntPtr handle)
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_CPSPEAKERS, handle);
       
        }
        public void play(string file)
        {
            if (Bass.BASS_ChannelIsActive(stream) == BASSActive.BASS_ACTIVE_PLAYING) Bass.BASS_StreamFree(stream);
            stream = Bass.BASS_StreamCreateFile(file,0L,0L,BASSFlag.BASS_SAMPLE_FLOAT);
            Bass.BASS_ChannelPlay(stream, true);
        }

        public Visuals getVisual()
        {
            return visual;
        }
        public int getStream()
        {
            return stream;
        }

        public bool isPlay()
        {
            return Bass.BASS_ChannelIsActive(stream) == BASSActive.BASS_ACTIVE_PLAYING ? true :false;
        }
    }
}
