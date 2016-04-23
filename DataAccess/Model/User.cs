using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Class;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class User : IEntity
    {
        public int Id { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "E-mail je povinný")]
        public string Email { get; set; }

        [DisplayName("Heslo")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        private string clearPassword;
        [NotMapped]
        [DisplayName("Heslo")]
        [DataType(DataType.Password)]
        public string ClearPassword
        {
            set
            {
                Password = CryptHelper.Crypt(value);
                clearPassword = value;
            }
            get { return clearPassword; }
        }

        [DisplayName("Heslo znovu")]
        [DataType(DataType.Password)]
        [Compare("ClearPassword", ErrorMessage = "Hesla se musí shodovat")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        
        [DisplayName("Jméno")]
        public string Name { get; set; }

        [DisplayName("Příjmení")]
        public string Surname { get; set; }
        
        public DateTime Created { get; set; }

        public DateTime? LastLogin { get; set; }

        public int TotalLogins { get; set; }

        public virtual List<Role> Roles { get; set; } 
    }
}
