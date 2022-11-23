using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
<<<<<<< HEAD
            Role = ROLE.Customer;
=======
            IDROLE = ROLE.Customer;
>>>>>>> hoangminh
        }

        public CustomerDTO(int id, string name, string email, string user, string password)
        {
<<<<<<< HEAD
            Id = id;
            Name = name;
            Email = email;
            User = user;
            Password = password;
            Role = ROLE.Customer;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
=======
            MAKH = id;
            TENKH = name;
            EMAIL = email;
            USERNAME = user;
            USERPASSWORD = password;
            IDROLE = ROLE.Customer;
        }

        public int MAKH { get; set; }
        public string TENKH { get; set; }
        public string EMAIL { get; set; }
        public string USERNAME { get; set; }
        public string USERPASSWORD { get; set; }
        public string IDROLE { get; set; }
>>>>>>> hoangminh
    }
}
