using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListenAndWrite.ModelIdentify;

namespace ListenAndWrite.Logic
{
    public class MemberAction
    {
        public static List<Score> GetLastScores(string userID, TestType testType, int numOfDate = 3)
        {
            var db = new ModelsContent();
            var listDate = db.Scores.Where(
                s => s.MemberID == userID
                ).OrderByDescending(s => s.NearestDateWithBestScore).Select(s => s.NearestDateWithBestScore).Distinct().Take(numOfDate).ToList();
            return db.Scores.Where(
                s => s.MemberID == userID && s.TestType == testType && listDate.Contains(s.NearestDateWithBestScore)
                ).ToList();
        }

        public static List<DatePoint> GetLastDatePoint(string userID, TestType testType, int numOfDate = 3)
        {
            List<DatePoint> list = new List<DatePoint>();
            List<Score> listScore = GetLastScores(userID, testType);
            for (int i = 0; i < listScore.Count; i++)
            {
                int level = listScore[i].Lesson.Level;
                int index = GetElementLevelIndex(list, level);
                Double score = TestAction.CalculateTotalScore(listScore[i].MaxScore);
                if (index == -1)
                {
                    list.Add(new DatePoint(listScore[i].NearestDateWithBestScore, score, level));
                }
                else
                {
                    list[index].point += score;
                }
            }
            return list;
        }

        public static int GetElementLevelIndex(List<DatePoint> list, int level)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].level == level)
                {
                    return i;
                }
            }
            return -1;
        }

        public static Score GetScore(string userID, int lessonID, TestType testType)
        {
            using (var _db = new ModelsContent())
            {
                return _db.Scores.Where(
                    s => s.MemberID == userID && s.LessonID == lessonID && s.TestType == testType
                    ).SingleOrDefault();
            }
        }

        public static List<Score> GetScore(string userID, int lessonID)
        {
            using (var db = new ModelsContent())
            {
                var ret = db.Scores.Where(
                    s => s.MemberID == userID && s.LessonID == lessonID
                    ).ToList();
                if (ret != null)
                {
                    return ret;
                }
            }
            return null;
        }

        public static void AddPoint(string username, double offset){
            var db = new ModelsContent();
            var member = db.Members.SingleOrDefault(
                m => m.UserName == username
                );
            member.Point += offset;
            db.SaveChanges();
        }
    }
}