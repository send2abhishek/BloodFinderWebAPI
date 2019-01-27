using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodTraceWebApi.Models
{
    public class BloodUser
    {

        public int Id { get; set; }

        [Required, StringLength(10)]
        public string UserName { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email in not valid")]
        public string Email { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }

        
        public string Country { get; set; }

        [Required, StringLength(2)]
        public string BloodGroup { get; set; }
        public string Imagepath { get; set; }

        public int Date { get; set; }


        //holds byte of array
        //we don't want the column of ImageArray in our database and 
        //that's why I am using NotMapped Attribute on top of this property

        [NotMapped]
        public byte[] ImageArray { get; set; }
    }
}