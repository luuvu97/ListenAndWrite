using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ListenAndWrite.ModelIdentify;
using ListenAndWrite.Logic;

namespace ListenAndWrite
{
    public partial class SubmitAudio : System.Web.UI.Page
    {
        public Lesson lesson { get; set;}
        public int lessonID { get; set; }
        public TrackNodeForSubmitAudio track { get; set; }

        public void GetData()
        {
            if (Session["lessonID"] != null)
            {
                this.lessonID = Int16.Parse(Session["lessonID"].ToString());
                this.lesson = ModelControl.GetLessionByID(this.lessonID);
                txtTitle.Text = this.lesson.Title;
                lblLenght.Text = this.lesson.Length.ToString();
                this.BindListTrack();
            }
            else
            {
                this.lesson = null;
            }
        }

        public void BindListTrack()
        {
            if (this.lesson != null)
            {
                var db = new ModelsContent();
                _ViewTrack.DataSource = db.Tracks.Where(t => t.LessonID == this.lessonID).ToList();
                _ViewTrack.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LogFile.PREPATH = Server.MapPath("~");
            this.GetData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.lesson != null)
            {
                int seq;
                if(this.lesson.Tracks == null){
                    seq = 0;
                }else{
                    seq = this.lesson.Tracks.Count;
                }
                this.track = new TrackNodeForSubmitAudio
                {
                    lessonID = this.lessonID,
                    lessonName = this.lesson.Title,
                    seqNumber = seq,
                    audioPath = audioPath.Value,
                    start = Double.Parse(playFrom.Text),
                    end = Double.Parse(playTo.Text),
                    script = ModelControl.Standardized( txtCurScript.Text),
                };
                this.track.TrimTrackAudio(Server.MapPath("~"));
                this.track.AddLessonTrack();
                this.GetData();
                _ViewTrack.DataBind();
            }
        }

        public List<ListenAndWrite.ModelIdentify.Track> _ViewTrack_GetData()
        {
            if (this.lesson != null)
            {
                return this.lesson.Tracks;
            }
            return null;
        }

        public List<ListenAndWrite.ModelIdentify.LessonCategory> _ViewCategory_GetData()
        {
            if (this.lesson != null)
            {
                return this.lesson.LessonCategory;
            }
            return null;
        }
    }
}