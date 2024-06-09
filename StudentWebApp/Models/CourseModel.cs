using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StudentWebApp.Models
{
    public class CourseModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Course Name")]
        public string CourseName { get; set; }
        [Required]
        [DisplayName("Student")]
        public int StudentID { get; set; }
        [Required]
        [DisplayName("Course Fees")]
        public decimal CourseFee { get; set; }

        

       // public List<StudentModel> StudentLst { get; set; }
    }

    public class CourseClass : CourseModel 
    {
        public string studentName { get; set; }
    }
}