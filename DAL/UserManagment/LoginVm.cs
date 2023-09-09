using System.ComponentModel.DataAnnotations;

namespace CookieReaders.Models
{
    public class LoginVm { 
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        
        public string Error { get {
                return GetError();
            
            }  }

        protected virtual string GetError() {
            string error = string.Empty;
                if(string.IsNullOrWhiteSpace(EmailAddress)) {
                error += "EmailAddress can not be empty"+Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                error += "Password can not be empty" + Environment.NewLine; ;
            }
            return error;
        }
    }
}