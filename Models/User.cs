using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public List<Wedding> Weddings { get; set; }

        public User ()
        {
            Weddings = new List<Wedding> ();
        }
    }
}