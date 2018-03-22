using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations.Resources;
using System.ComponentModel;

namespace ListenAndWrite.ModelIdentify
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
        public int CategoryID { get; set; }

        [Display(Name = "Category Name"), MaxLength(40)]
        public string CategoryName { get; set; }

        public virtual List<LessonCategory> LessonCategories { get; set; }
    }
}