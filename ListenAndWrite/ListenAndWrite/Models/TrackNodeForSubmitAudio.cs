using System;
using System.Collections.Generic;
using System.Linq;
using ListenAndWrite.ModelIdentify;
using ListenAndWrite.Logic;

namespace ListenAndWrite.ModelIdentify
{
    public class TrackNodeForSubmitAudio
    {
        public int lessonID{get; set;}
        public string lessonName { get; set; }
        public int seqNumber { get; set; }
        public double start { get; set; }
        public double end { get; set; }
        public string script { get; set; }
        public string audioPath { get; set; }
        public string outPath { get; set; }

        public double GetLength()
        {
            return this.end - this.start;
        }

        public long DoubleToMilliseconds(Double inp)
        {
            var tmp = Math.Round(inp * 1000);
            return Convert.ToInt64(tmp);
            
        }

        public static String RemoveAllWhiteSpace(String inp)
        {
            return string.Concat(inp.Where(c => !char.IsWhiteSpace(c)));
        }

        public void TrimTrackAudio(String serverPath)
        {
            this.outPath = ModelsContent.PRE_AUDIO_PATH + this.lessonID + "_" + RemoveAllWhiteSpace(this.lessonName) + "_" + this.seqNumber + ".mp3";
            String outAudioPath = serverPath + this.outPath;
            TrimAudio.TrimMp3(this.audioPath, outAudioPath, TimeSpan.FromMilliseconds(DoubleToMilliseconds(this.start)), TimeSpan.FromMilliseconds(DoubleToMilliseconds(this.end)));
            LogFile.WriteAfterAddTrack(this);
        }

        public void AddLessonTrack()
        {
            ModelControl.AddTrack(this);
        }
    }
}