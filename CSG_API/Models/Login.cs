using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CSG_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CSG_API.Models
{
    public class Login
    {
        [Key]
        public long userid { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
    }
}
