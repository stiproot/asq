using System;

namespace asqapi.Models
{
    public class AuthenticationModel
    {
        public string Username{ get; set; }
        public string Password{ get; set; }

        public void Validate()
        {
            if(string.IsNullOrWhiteSpace(this.Username) || string.IsNullOrWhiteSpace(this.Password))
            {
                throw new Exception("Authentication model invalid");
            }
        }
    }
}