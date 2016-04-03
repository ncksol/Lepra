using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepra
{
    public class UserModel
    {
        public UserModel(User user)
        {
            Id = user.id;
            Username = user.login;
            Rank = user.rank;
            Gender = user.gender.ToLower() == "male" ? 0 : 1;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Rank { get; set; }
        public int Gender { get; set; }
    }
}
