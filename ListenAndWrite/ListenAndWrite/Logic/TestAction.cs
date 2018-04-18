using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListenAndWrite.ModelIdentify;

namespace ListenAndWrite.Logic
{
    public class TestAction
    {
        public ApplicationUser member { get; set; }

        public Lesson lesson { get; set; }

        public Test testing { get; set; }

        public TestType testType { get; set; }

        public int currentPart { get; set; }

        public string point { get; set; }   //a string represent point for all track:ex 12,15,20,0

        public Score score { get; set; }

        public List<string> GetScripts()
        {
            return this.lesson.Tracks.Select(lp => lp.ScriptText).ToList();
        }

        public List<string> GetAudioPath()
        {
            return this.lesson.Tracks.Select(lp => lp.AudioPath).ToList();
        }

        public TestAction(ApplicationUser member, Lesson lesson)
        {
            this.member = member;
            this.lesson = lesson;
        }

        public void SetTestType(TestType testType)
        {
            this.testType = testType;
        }

        public void UpdateScore(List<Double> point)
        {
            List<Score> ls = MemberAction.GetScore(this.member.Id, this.lesson.LessonID);
            if (ls.Count == 0)
            {
                ModelControl.IncreaseViewCount(this.lesson.LessonID);
            }

            Score s = MemberAction.GetScore(this.member.Id, this.lesson.LessonID, this.testType);
            if (s == null)
            {
                Score newScore = new Score
                {
                    MemberID = this.member.Id,
                    LessonID = this.lesson.LessonID,
                    NearestDateWithBestScore = DateTime.Today,
                    MaxScore = this.PointToString(point),
                    TestType = this.testType,
                };
                var db = new ModelsContent();
                db.Scores.Add(newScore);
                db.SaveChanges();
            }
            else
            {
                //Need type IQueryable. Not type Score
                //And the connection is not closed
                var db = new ModelsContent();
                var tmp = db.Scores.Where(
                    sc => sc.MemberID == this.member.Id && sc.LessonID == this.lesson.LessonID && sc.TestType == this.testType
                    ).FirstOrDefault();
                tmp.MaxScore = this.PointToString(point);
                tmp.NearestDateWithBestScore = DateTime.Today;
                db.SaveChanges();
            }
        }

        public String PointToString(List<Double> point)
        {
            String ret = "";
            for (int i = 0; i < point.Count - 1; i++)
            {
                ret += point[i] + ",";
            }
            ret += point[point.Count - 1];
            return ret;
        }

        public void UpdateTest(String point)
        {
            bool isUpdate = false;
            this.point = point;
            List<Double> maxScore = null;
            List<Double> curScore = parsePoint(this.point);
            List<Double> prevMaxScore = null;
            this.score = MemberAction.GetScore(this.member.Id, this.lesson.LessonID, this.testType);
            if (this.score != null)
            {
                maxScore = parsePoint(this.score.MaxScore);
                prevMaxScore = parsePoint(this.score.MaxScore);
                for (int i = 0; i < maxScore.Count; i++)
                {
                    if (curScore[i] > maxScore[i])
                    {
                        isUpdate = true;
                        maxScore[i] = curScore[i];
                    }
                }
            }
            else
            {
                isUpdate = true;
                maxScore = curScore;
            }
            if (isUpdate == true)
            {
                this.UpdateScore(maxScore);
                if (prevMaxScore == null)
                {
                    MemberAction.AddPoint(this.member.UserName, CalculateTotalScore(maxScore));
                }
                else
                {
                    MemberAction.AddPoint(this.member.UserName, CalculateTotalScore(maxScore) - CalculateTotalScore(prevMaxScore));
                }
            }
        }

        public static double CalculateTotalScore(String point)
        {
            //point is a string represent point for all track:ex 12,15,20,0
            double ret = 0;
            String[] arr = point.Split(",".ToCharArray());
            for (int i = 0; i < arr.Length; i++)
            {
                ret += Double.Parse(arr[i]);
            }
            return ret;
        }

        public static double CalculateTotalScore(List<Double> list)
        {
            //point is a string represent point for all track:ex 12,15,20,0
            double ret = 0;
            for (int i = 0; i < list.Count; i++)
            {
                ret += list[i];
            }
            return ret;
        }

        public static List<double> parsePoint(String point)
        {
            List<double> ret = new List<double>();
            String[] arr = point.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                ret.Add(Double.Parse(arr[i]));
            }
            return ret;
        }
    }
}