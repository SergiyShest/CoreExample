using System.ComponentModel.DataAnnotations;

namespace CookieReaders.Models
{
    public class RegisterVm : LoginVm
    {
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }

        protected override string GetError()
        {
            string error = base.GetError();
            if (!string.IsNullOrWhiteSpace(Password) &&  Password != ConfirmPassword)
            {
                error += "Password and ConfirmPassword not Equal" + Environment.NewLine;
            }
            
            return error;
        }
    }
}