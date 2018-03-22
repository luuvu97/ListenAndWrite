using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListenAndWrite.ModelIdentify;
using System.IO;

namespace ListenAndWrite.Logic
{
    public class LogFile
    {
        public static string PREPATH { get; set; }
        public static void WriteAfterAddLesson(int lessonID)
        {
            Lesson l = ModelControl.GetLessionByID(lessonID);
            using (StreamWriter w = File.AppendText(PREPATH + "Lesson.txt"))
            {
                w.WriteLine(l.LessonID);
                w.WriteLine(l.Title);
                w.WriteLine(l.Level);
                w.WriteLine(l.Description);
                w.WriteLine("@@@");
            }
        }
        public static void WriteAfterAddLessonCategory(int lessonID, int categoryID)
        {
            using (StreamWriter w = File.AppendText(PREPATH + "LessonCategory.txt"))
            {
                w.WriteLine(categoryID);
                w.WriteLine(lessonID);
                w.WriteLine("@@@");
            }

        }

        public static void WriteAfterAddCategory(int categoryID)
        {
            var db = new ModelsContent();
            var category = db.Categories.SingleOrDefault(c => c.CategoryID == categoryID);
            if (category != null)
            {
                using (StreamWriter w = File.AppendText(PREPATH + "Cateogry.txt"))
                {
                    w.WriteLine(category.CategoryID);
                    w.WriteLine(category.CategoryName);
                    w.WriteLine("@@@");
                }
            }
            
        }

        public static void WriteAfterAddTrack(TrackNodeForSubmitAudio track)
        {
            using (StreamWriter w = File.AppendText(PREPATH + "Track.txt"))
            {
                w.WriteLine(track.lessonID);
                w.WriteLine(track.lessonName);
                w.WriteLine(track.seqNumber);
                w.WriteLine(track.outPath);
                w.WriteLine(track.start);
                w.WriteLine(track.end);
                w.WriteLine(track.script);
                w.WriteLine("@@@");
            }
        }
    }
}