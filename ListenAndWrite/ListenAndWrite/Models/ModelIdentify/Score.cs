using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ListenAndWrite.ModelIdentify
{
    public class Score
    {
        [Key, ForeignKey("Member"), MaxLength(128), Column(Order=0)]
        public string MemberID { get; set; }

        [Key, ForeignKey("Lesson"), Column(Order=1)]
        public int LessonID { get; set; }

        [Key, Column(Order=2)]
        public TestType TestType { get; set; }

        public DateTime NearestDateWithBestScore { get; set; }

        public String MaxScore { get; set; }

        public virtual ApplicationUser Member { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}