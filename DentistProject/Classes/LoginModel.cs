using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz.")]
        [Display(Name = "Kullanıcı Adı")]
        public string txtUserName { get; set; }

        [Required(ErrorMessage = "Lütfen parolanızı giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string txtPassword { get; set; }
    }
}