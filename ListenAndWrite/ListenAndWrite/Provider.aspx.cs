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
    public partial class Provider : System.Web.UI.Page
    {
        public int PAGE_SIZE = 20;
        public ApplicationUser provider { get; set; }
        public IQueryable<Lesson> lessons { get; set; }

        public void GetData()
        {
            //memeber
            if (!String.IsNullOrWhiteSpace(Request.QueryString["Provider"]))
            {
                String providerName = Request.QueryString["Provider"];
                this.provider = Logic.ModelControl.GetMemberByUserName(providerName);
                if (providerName == null)
                {
                    Response.Redirect("~/");
                }
                //lesson list
                var db = new ModelsContent();
                this.lessons = db.Lessons.Where(l => l.Provider.UserName == this.provider.UserName && l.Tracks.Count >  0).OrderByDescending(l => l.UploadedTime).ThenByDescending(l => l.LessonID);
            }
            else
            {
                Response.Redirect("~/");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetData();
        }

        public IQueryable<ListenAndWrite.ModelIdentify.Lesson> _ViewAudios_GetData()
        {
            return this.lessons;
        }

        protected void _DataPager_PreRender(object sender, EventArgs e)
        {
            _DataPager.PageSize = PAGE_SIZE;
        }
    }
}