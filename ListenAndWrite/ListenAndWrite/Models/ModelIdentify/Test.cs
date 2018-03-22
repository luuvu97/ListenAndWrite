using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListenAndWrite.ModelIdentify
{
    public class Test
    {
        [Key, ForeignKey("Member"), MaxLength(128), Column(Order = 0)]
        public string MemberID { get; set; }

        [ForeignKey("Lession"), Column(Order = 1)]
        public int LessonID { get; set; }

        public TestType TestType { get; set; }

        public int CurrentPart { get; set; }

        public string Point { get; set; }

        public virtual ApplicationUser Member { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}