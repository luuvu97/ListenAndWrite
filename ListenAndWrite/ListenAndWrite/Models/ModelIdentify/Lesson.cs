using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ListenAndWrite.ModelIdentify
{
    public class Lesson
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
        public int LessonID { get; set; }

        [ForeignKey("Provider"), ScaffoldColumn(false), MaxLength(128)]
        public string ProviderID { get; set; }

        [Required, MaxLength(200), Display(Name = "Title")]
        public string Title { get; set; }

        [Required, DefaultValue((int) 1), Display(Name = "Level")]
        public int Level { get; set; }

        public Double Length { get; set; }

        [Required, Display(Name="Uploaded Time")]
        public DateTime UploadedTime { get; set; }

        [DefaultValue(0)]
        [Required, Display(Name="View Count")]
        public int ViewCount { get; set; }

        [StringLength(2), Display(Name="Language")]
        public string Language { get; set; }

        [MaxLength(2000), Display(Name = "Description")]
        public string Description { get; set; }

        public virtual List<Track> Tracks { get; set; }
        public virtual List<LessonCategory> LessonCategory { get; set; }
        public virtual ApplicationUser Provider { get; set; }
    }
}