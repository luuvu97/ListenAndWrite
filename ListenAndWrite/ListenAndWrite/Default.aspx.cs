using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListenAndWrite.ModelIdentify;
using System.Web.UI.DataVisualization.Charting;
using ListenAndWrite.Logic;

namespace ListenAndWrite
{
    public enum QuickAccess
    {
        Newest,
        PopularAudio,
        AudioOfWeek,
        QuickChoose,
    }

    public partial class _Default : Page
    {
        public int PAGE_SIZE = 20;
        public const int MAX_LEVEL = 20;
        public const string QUERY_SEARCH_CATEGORY_ID = "CategoryID";
        public const string QUERY_SEARCH_ACCESS = "Access";
        public const string QUERY_SEARCH_TEXT = "Search";

        public ApplicationUser member { get; set; }
        private string searchText { get; set; }
        private int searchLevelFrom { get; set; }
        private int searchLevelTo { get; set; }
        private int searchCategoryID { get; set; }
        private QuickAccess quickAccess { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetMember();
            if (this.member != null)
            {
                //ChartAction.showChart(MemberAction.GetLastDatePoint(this.member.Id, 3), Chart1);
            }
        }

        public void GetMember()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                this.member = ModelControl.GetMemberByUserName(HttpContext.Current.User.Identity.Name);
                //Response.Write(this.member.Id);
            }
        }

        public void getQueryString()
        {
            initSearchData();

            //for searchAccess
            if (!String.IsNullOrEmpty(Request.QueryString[QUERY_SEARCH_ACCESS]))
            {
                this.parseQuickAccess();
                return;
            }

            //for searchCategoryID
            if (!String.IsNullOrEmpty(Request.QueryString[QUERY_SEARCH_CATEGORY_ID]))
            {
                this.searchCategoryID = Convert.ToInt32(Request.QueryString[QUERY_SEARCH_CATEGORY_ID]);
            }
            else
            {
                this.searchCategoryID = -1;
            }

            //for searchText
            if (!String.IsNullOrEmpty(Request.QueryString[QUERY_SEARCH_TEXT]))
            {
                this.searchText = Request.QueryString[QUERY_SEARCH_TEXT];
            }
            else
            {
                this.searchText = null;
            }
        }

        public void parseQuickAccess()
        {
            //Default value is newest
            string str = Request.QueryString[QUERY_SEARCH_ACCESS];
            if (str == "QuickChoose")
            {
                this.quickAccess = QuickAccess.QuickChoose;
            }
            else if (str == "PopularAudio")
            {
                this.quickAccess = QuickAccess.PopularAudio;
            }
            else if (str == "AudioOfWeek")
            {
                this.quickAccess = QuickAccess.AudioOfWeek;
            }
            else
            {
                this.quickAccess = QuickAccess.Newest;
            }
        }

        public IQueryable<ListenAndWrite.ModelIdentify.Lesson> _ViewAudios_GetData()
        {
            this.getQueryString();
            var _db = new ModelsContent();

            //compute with search category id
            if (searchCategoryID != -1)
            {
                return (from lc in _db.LessonCateogies
                          from l in _db.Lessons
                          where l.LessonID == lc.LessonID && lc.CategoryID == this.searchCategoryID && l.Tracks.Count > 0
                          select l).OrderByDescending(l => l.ViewCount);
            }

            if (!String.IsNullOrEmpty(this.searchText))
            {
                return _db.Lessons.Where(l => l.Title.Contains(this.searchText)).OrderByDescending(l => l.ViewCount);
            }

            if (this.quickAccess == QuickAccess.QuickChoose)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    this.GetMember();
                    this.searchLevelFrom = this.member.ListeningLevel;
                    this.searchLevelTo = MAX_LEVEL;

                    List<int> alreadyListen = _db.Scores.Where(
                            s => s.MemberID == this.member.Id
                        ).Select( s => s.LessonID).ToList();

                    return _db.Lessons.Where(
                        l => l.Level >= this.searchLevelFrom && l.Level <= this.searchLevelTo && !alreadyListen.Contains(l.LessonID) && l.Tracks.Count > 0
                        ).OrderBy(l => l.Level).ThenByDescending(l => l.ViewCount);
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }

            if (this.quickAccess == QuickAccess.AudioOfWeek)
            {
                DateTime prevTwoWeek = DateTime.Today.Subtract(TimeSpan.FromDays(14));
                return _db.Lessons.Where(
                    l => l.UploadedTime >= prevTwoWeek && l.Tracks.Count > 0
                    ).OrderByDescending(l => l.ViewCount);
            }

            if (this.quickAccess == QuickAccess.PopularAudio)
            {
                return _db.Lessons.OrderByDescending(l => l.ViewCount);
            }

            //default: newest
            return _db.Lessons.Where(l => l.Tracks.Count > 0).OrderByDescending(l => l.UploadedTime).ThenByDescending(l => l.LessonID);
        }

        protected void _DataPager_PreRender(object sender, EventArgs e)
        {
            _DataPager.PageSize = PAGE_SIZE;
        }

        private void initSearchData()
        {
            this.searchCategoryID = -1;
            this.searchText = null;
            this.searchLevelFrom = 0;
            this.searchLevelTo = MAX_LEVEL;
        }
    }
}