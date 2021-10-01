using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSG_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CSG_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string pass { get; set; }
    }
}
