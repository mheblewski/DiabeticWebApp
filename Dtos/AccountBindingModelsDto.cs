using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class AddExternalLoginBindingModelDto
    {
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModelDto
    {

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModelDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterExternalBindingModelDto
    {
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModelDto
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModelDto
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
