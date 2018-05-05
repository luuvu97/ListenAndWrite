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
    public partial class Listening : System.Web.UI.Page
    {
        public TestAction testAction { get; set; }
        public ApplicationUser member { get; set; }
        public Lesson lesson { get; set; }
        public TestType testType { get; set; }
        public List<Score> scores { get; set; }
        public bool isContinue { get; set; }

        public Listening()
        {
            //this.testAction = null;
            //this.member = null;
            //this.lesson = null;
            //this.testType = TestType.Unset;
            //this.scores = null;
            //this.isContinue = false;
        }

        public void GetData()
        {
            //lesson
            if (Request.QueryString["LessonID"] != null)
            {
                int lessonID = Convert.ToInt32(Request.QueryString["LessonID"]);
                this.lesson = ModelControl.GetLessionByID(lessonID);
            }
            else
            {
                Response.Redirect("~/");
            }

            //memeber
            if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("~/Account/Login");
            }
            this.member = Logic.ModelControl.GetMemberByUserName(HttpContext.Current.User.Identity.Name);

            //score
            this.scores = MemberAction.GetScore(this.member.Id, this.lesson.LessonID);
            this.testAction = new TestAction(this.member, this.lesson);
            //test action
            if (!String.IsNullOrWhiteSpace(txtTestType.Value))
            {
                this.testType = ModelControl.GetTestType(txtTestType.Value);
                //this.testType = ModelControl.GetTestType(Session["testType"].ToString());
                this.testAction.SetTestType(this.testType);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetData();
            this.GetScript();
            this.GetAudioPath();
        }

        public void GetScript()
        {
            var listScripts = this.testAction.GetScripts();
            String str = "";
            for (int i = 0; i < listScripts.Count; i++)
            {
                str += listScripts[i];
                if (i < listScripts.Count - 1)
                {
                    str += ";";
                }
            }
            txtScripts.Value = str;
        }

        public void GetAudioPath()
        {
            var listAudioPath = this.testAction.GetAudioPath();
            String str = "";
            for (int i = 0; i < listAudioPath.Count; i++)
            {
                str += listAudioPath[i];
                if (i < listAudioPath.Count - 1)
                {
                    str += ";";
                }
            }
            txtAudioPaths.Value = str;
        }

        public string ConvertListToArrayJS(List<string> list, String varName)
        {
            String script = "";
            script = "<script>";
            script += "var " + varName + " = new Array();";
            foreach (string str in list)
            {
                script += varName + ".push('" + str + "');";
            }
            script += "</script>";
            return script;
        }

        public void SetClientScript()
        {
            ClientScriptManager cs = Page.ClientScript;
            String scriptText = "";
            scriptText = "var fireEventElement = new FireEventElement('" + txtPoint.ClientID + "', '" + txtCurrentPart.ClientID + "','" + btnForEventFireUp.ClientID + "');";
            cs.RegisterClientScriptBlock(this.GetType(), "fireEvent", scriptText, true);

        }

        protected void btnForEventFireUp_Click(object sender, EventArgs e)
        {
            String point = txtPoint.Value;
            this.testAction.UpdateTest(point);
        }

        protected void btnTestTypeFullMode_Click(object sender, EventArgs e)
        {
            this.testType = TestType.FullMode;
            //Session["testType"] = ModelControl.TestTypeToString(this.testType);
            txtTestType.Value = ModelControl.TestTypeToString(this.testType);
            this.isContinue = true;
        }

        protected void btnTestTypeNewMode_Click(object sender, EventArgs e)
        {
            this.testType = TestType.NewMode;
            //Session["testType"] = ModelControl.TestTypeToString(this.testType);
            txtTestType.Value = ModelControl.TestTypeToString(this.testType);
            this.isContinue = true;
        }

        public ListenAndWrite.ModelIdentify.Lesson _ViewLessionDetail_GetItem()
        {
            return this.lesson;
        }

        protected void btnListenAgain_Click(object sender, EventArgs e)
        {
            this.isContinue = true;
        }
    }
}