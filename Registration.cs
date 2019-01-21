using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegisterMVC.Models
{
    public class Registration
    {
        [Key]
        public int userId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("[a-zA-Z ]{6,10}", ErrorMessage = "can have Alphabets and space of 6 to 10 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("[a-zA-Z ]{6,10}", ErrorMessage = "can have Alphabets and space of 6 to 10 characters.")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression(@"[\w-\.]+@([\w-]+\.)+[\w-]+", ErrorMessage = "Enter valid E-mail Id")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Address")]
        [RegularExpression("[a-zA-Z0-1 ]{1,30}", ErrorMessage = "accepts alphabets, numbers and space")]
        public string Address { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Password")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$", ErrorMessage = "Minimum 8 characters atleast 1 Alphabet, 1 Number and 1 Special Character")]
        public string Password { get; set; }

        public int PageCount { get; set; }

        public int PageNumber { get; set; }

        public List<Registration> list{get;set;}
    }
}