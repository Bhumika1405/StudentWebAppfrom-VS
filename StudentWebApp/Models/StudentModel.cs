using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StudentWebApp.Models
{
    public class StudentModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
       // [Required]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DisplayName("Enroll Date")]
        public DateTime EnrollDate { get; set; }
        [Required]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        
    }
}