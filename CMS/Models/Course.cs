using System;
using System.Collections.Generic;

#nullable disable

namespace CMS.Models
{
    public partial class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
    }
}
