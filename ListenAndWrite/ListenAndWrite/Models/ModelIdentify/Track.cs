using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListenAndWrite.ModelIdentify
{
    public class Track
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), ScaffoldColumn(false)]
        public int TrackID { get; set; }

        public int LessonID { get; set; }

        [Required, MaxLength(400), ScaffoldColumn(false)]
        public string AudioPath { get; set; }

        [Required, MaxLength(400), ScaffoldColumn(false)]
        public string ScriptText { get; set; }
    }
}