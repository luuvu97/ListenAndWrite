using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListenAndWrite.ModelIdentify
{
    public class LessonCategory
    {
        [Key, Column(Order=0), ForeignKey("Lesson")]
        public int LessonID { get; set; }

        [Key, Column(Order=1), ForeignKey("Category")]
        public int CategoryID { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual Category Category { get; set; }
    }
}