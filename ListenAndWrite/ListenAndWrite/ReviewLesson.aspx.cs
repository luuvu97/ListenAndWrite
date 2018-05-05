using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using  ListenAndWrite.ModelIdentify;
using ListenAndWrite.Logic;
using System.Web.UI.DataVisualization.Charting;

namespace ListenAndWrite
{
    public partial class ReviewLesson : System.Web.UI.Page
    {
        //public TestType testType { get; set; }
        public Lesson lesson { get; set; }
        public List<Score> scores { get; set; }
        public List<ScoreNode> scoreNode { get; set; }
        public List<TrackNode> pointTrackFullMode { get; set; }
        public List<TrackNode> pointTrackNewMode { get; set; }
        public ApplicationUser member { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetData();
            ChartAction.showChart(MemberAction.GetLastDatePoint(this.member.Id, TestType.FullMode, 5), Chart1);
            ChartAction.showChart(MemberAction.GetLastDatePoint(this.member.Id, TestType.NewMode, 5), Chart2);
        }

        public void GetData()
        {
            if(HttpContext.Current.User.Identity.IsAuthenticated == false){
                Response.Redirect("~/Account/Login");
            }
            else
            {
                this.member = ModelControl.GetMemberByUserName(HttpContext.Current.User.Identity.Name);
            }

            if (!String.IsNullOrEmpty(Request.QueryString["LessonID"]))
            {
                int lessonID = Int32.Parse(Request.QueryString["LessonID"]);
                this.lesson = ModelControl.GetLessionByID(lessonID);
            }
            else
            {
                Response.Redirect("~/");
            }

            //this.testType = TestType.FullMode;
            this.scores = MemberAction.GetScore(this.member.Id, this.lesson.LessonID);

            //for TrackNode
            Double maxPointFullMode = 100 + (this.lesson.Level - 1) * 10;
            Double maxTrackPointFullMode = maxPointFullMode / this.lesson.Tracks.Count;
            Double maxPointNewMode = 200 + (this.lesson.Level - 1) * 10;
            Double maxTrackPointNewMode = maxPointNewMode / this.lesson.Tracks.Count;
            //full mode
            Score s = this.GetScoreTestType(TestType.FullMode);
            if (s != null)
            {
                this.pointTrackFullMode = ModelControl.GenTrackNode(TestAction.parsePoint(s.MaxScore), maxTrackPointFullMode);
            }
            else
            {
                this.pointTrackFullMode = null;
            }
            //New Mode
            s = this.GetScoreTestType(TestType.NewMode);
            if (s != null)
            {
                this.pointTrackNewMode = ModelControl.GenTrackNode(TestAction.parsePoint(s.MaxScore), maxTrackPointNewMode);
            }
            else
            {
                this.pointTrackNewMode = null;
            }
            //for ScoreNode
            this.scoreNode = ModelControl.GenScoreNode(this.scores, maxPointFullMode, maxPointNewMode);
        }

        public Score GetScoreTestType(TestType type)
        {
            foreach (Score s in this.scores)
            {
                if (s.TestType == type)
                {
                    return s;
                }
            }
            return null;
        }

        public ListenAndWrite.ModelIdentify.Lesson _ViewLessionDetail_GetItem()
        {
            return this.lesson;
        }

        public List<ListenAndWrite.ModelIdentify.TrackNode> _ViewTrackFullMode_GetData()
        {
            return this.pointTrackFullMode;
        }

        public List<ListenAndWrite.ModelIdentify.TrackNode> _ViewTrackNewMode_GetData()
        {
            return this.pointTrackNewMode;
        }

        public List<ListenAndWrite.ModelIdentify.ScoreNode> _ViewBriefScore_GetData()
        {
            return this.scoreNode;
        }
    }
}