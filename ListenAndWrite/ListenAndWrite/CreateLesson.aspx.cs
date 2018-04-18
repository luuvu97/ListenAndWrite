using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListenAndWrite.ModelIdentify;
using ListenAndWrite.Logic;

namespace ListenAndWrite
{
    public partial class CreateLesson : System.Web.UI.Page
    {
        public Lesson lesson { get; set; }
        public Category category { get; set; }
        public int lessonID { get; set; }

        public void GetData()
        {
            if (Session["lessonID"] != null)
            {
                this.lessonID = Int32.Parse(Session["lessonID"].ToString());
                this.lesson = ModelControl.GetLessionByID(this.lessonID);
                txtTitle.Text = this.lesson.Title;
                lblNewLessonID.Text = this.lessonID.ToString();
                txtDescription.Text = this.lesson.Description;
                txtLevel.Text = this.lesson.Level.ToString();
            }
            else
            {
                this.lesson = null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LogFile.PREPATH = Server.MapPath("~");
            this.GetData();
            this.BindCategoryList();
        }

        protected void btnNewLesson_Click(object sender, EventArgs e)
        {
            this.lesson = new Lesson
            {
                ProviderID = "1",
                Title = txtTitle.Text,
                ViewCount = 0,
                UploadedTime = DateTime.Now,
                Length = 0,
                Level = Int32.Parse(txtLevel.Text),
                Language = "EN",
                Description = txtDescription.Text,
            };
            this.lessonID = ModelControl.AddLesson(this.lesson);
            LogFile.WriteAfterAddLesson(this.lessonID);
            this.GetData();
            HttpContext.Current.Session["lessonID"] = this.lessonID;
            Response.Redirect(Request.RawUrl);
        }

        public void BindCategoryList()
        {
            var db = new ModelsContent();
            drCategory.DataSource = db.Categories.OrderBy(l => l.CategoryName).ToList();
            drCategory.DataTextField = "CategoryName";
            drCategory.DataValueField = "CategoryID";
            drCategory.DataBind();
        }

        public List<ListenAndWrite.ModelIdentify.Track> _ViewTrack_GetData()
        {
            if (this.lesson != null)
            {
                return this.lesson.Tracks;
            }
            return null;
        }

        protected void btnNewCategory_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtNewCategory.Text))
            {
                var db = new ModelsContent();
                var tmp = db.Categories.SingleOrDefault(c => c.CategoryName.ToUpper() == txtNewCategory.Text.ToUpper());
                if (tmp == null)
                {
                    db.Categories.Add(new Category
                    {
                        CategoryName = txtNewCategory.Text,
                    });
                    db.SaveChanges();
                    var id = db.Categories.Max(c => c.CategoryID);
                    LogFile.WriteAfterAddCategory(id);
                    txtNewCategory.Text = "";
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void btnAddLessonCategory_Click(object sender, EventArgs e)
        {
            if (this.lesson != null)
            {
                String[] arr = txtCategory.Value.Split("+".ToCharArray());
                foreach (String str in arr)
                {
                    ModelControl.AddLessonCategory(this.lessonID, Int32.Parse(str));
                    LogFile.WriteAfterAddLessonCategory(this.lessonID, Int32.Parse(str));
                }
            }
            Response.Redirect(Request.RawUrl);
        }

        public List<ListenAndWrite.ModelIdentify.LessonCategory> _ViewCategory_GetData()
        {
            if (this.lesson != null)
            {
                return this.lesson.LessonCategory;
            }
            return null;
        }

        protected void btnMoveToSubmitAudio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SubmitAudio.aspx");
        }

        protected void btnGetExistLesson_Click(object sender, EventArgs e)
        {
            this.lessonID = Int32.Parse(lblNewLessonID.Text);
            HttpContext.Current.Session["lessonID"] = this.lessonID;
            Response.Redirect("~/CreateLesson.aspx");
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["lessonID"] = null;
            Response.Redirect("~/CreateLesson.aspx");
        }
    }
}