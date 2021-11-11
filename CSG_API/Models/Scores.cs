using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSG_API.Models
{
    public class Scores
    {
        [Key]
        public int userid { get; set; }
        public int score { get; set; }
        public string username { get; set; }
    }
}
