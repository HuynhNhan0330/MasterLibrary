
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
            Role = ROLE.Customer;
        }

        public CustomerDTO(int id, string name, string email, string user, string password)
        {
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
    }
}
