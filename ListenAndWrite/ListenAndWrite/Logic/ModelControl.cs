using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListenAndWrite.ModelIdentify;

namespace ListenAndWrite.Logic
{
    public class ModelControl
    {
        public static ApplicationUser GetMemberByUserName(string name)
        {
            var db = new ModelsContent();
            return db.Members.SingleOrDefault(
                m => m.UserName == name
                );
        }

        public static Lesson GetLessionByID(int lessonID)
        {
            var db = new ModelsContent();
            return db.Lessons.Where(l => l.LessonID == lessonID).SingleOrDefault();
        }

        public static String TestTypeToString(TestType type)
        {
            if (type == TestType.NewMode)
            {
                return "NewMode";
            }
            else if (type == TestType.FullMode)
            {
                return "FullMode";
            }
            else
            {
                return "Unset";
            }
        }

        public static TestType GetTestType(String type)
        {
            type = type.ToUpper();
            if (type == "NEWMODE")
            {
                return TestType.NewMode;
            }
            else if (type == "FULLMODE")
            {
                return TestType.FullMode;
            }
            else
            {
                return TestType.Unset;
            }
        }

        public static List<TrackNode> GenTrackNode(List<Double> pointTrack, double maxPointTrack)
        {
            List<TrackNode> list = new List<TrackNode>();
            for (int i = 0; i < pointTrack.Count; i++)
            {
                list.Add(new TrackNode{
                    seqNumber = i,
                    point = pointTrack[i],
                    maxPoint = maxPointTrack,
                });
            }
            return list;
        }

        public static List<ScoreNode> GenScoreNode(List<Score> scores, double maxPointFullMode, double maxPointNewMode)
        {
            List<ScoreNode> list = new List<ScoreNode>();
            for (int i = 0; i < scores.Count; i++)
            {
                double maxScore = 0;
                if (scores[i].TestType == TestType.FullMode)
                {
                    maxScore = maxPointFullMode;
                }
                else
                {
                    maxScore = maxPointNewMode;
                }
                list.Add(new ScoreNode
                {
                    testType = ModelControl.TestTypeToString(scores[i].TestType),
                    totalScore = TestAction.CalculateTotalScore(scores[i].MaxScore),
                    maxScore = maxScore,
                });
            }
            return list;
        }

        public static int AddLesson(Lesson lesson)
        {
            var db = new ModelsContent();
            db.Lessons.Add(lesson);
            db.SaveChanges();
            return db.Lessons.Max(l => l.LessonID);
        }

        public static void AddLessonCategory(int lessonID, int categoryID)
        {
            using (var db = new ModelsContent())
            {
                LessonCategory lc = new LessonCategory
                {
                    LessonID = lessonID,
                    CategoryID = categoryID,
                };
                var tmp = db.LessonCateogies.SingleOrDefault(c => c.CategoryID == lc.CategoryID && c.LessonID == lc.LessonID);
                
                    if (tmp == null)
                    {
                        db.LessonCateogies.Add(lc);
                        db.SaveChanges();
                    }
                
            }
        }

        public static void AddTrack(TrackNodeForSubmitAudio track)
        {
            using (var db = new ModelsContent())
            {
                db.Tracks.Add(new Track
                {
                    LessonID = track.lessonID,
                    AudioPath = track.outPath,
                    ScriptText =  track.script,
                });
                var lesson = db.Lessons.SingleOrDefault(l => l.LessonID == track.lessonID);
                lesson.Length += track.GetLength();
                db.SaveChanges();
            }
        }

        public static String Standardized(String str)
        {
            foreach (char ch in str.ToCharArray())
            {
                if (!((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || ch == ' ' || ch == '\''))
                {
                    str = str.Replace(ch, ' ');
                }
            }
            str = str.Trim();
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < str.Length - 1; i++)
                {
                    if (str[i] == ' ' && str[i + 1] == ' ')
                    {
                        str = str.Remove(i, 1);
                        flag = true;
                    }
                }
            }

            return str;
        }

        public static void IncreaseViewCount(int lessonID)
        {
            using (var db = new ModelsContent())
            {
                var lesson = db.Lessons.SingleOrDefault(l => l.LessonID == lessonID);
                if (lesson != null)
                {
                    lesson.ViewCount += 1;
                    db.SaveChanges();
                }
            }
        }

        public static void updateLength(int lessonID, double offset)
        {
            using (var db = new ModelsContent())
            {
                var lesson = db.Lessons.SingleOrDefault(l => l.LessonID == lessonID);
                lesson.Length += offset;
                db.SaveChanges();
            }
        }

        public static void removeTrack(int trackID){
            using (var db = new ModelsContent())
            {
                var track = new Track { TrackID = trackID };
                db.Tracks.Attach(track);
                db.Tracks.Remove(track);
                db.SaveChanges();
            }
        }
    }
}