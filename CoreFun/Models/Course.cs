using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFun.Models
{
    public class Course
    {
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        //[Key, Column("CourseID", Order = 1)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
