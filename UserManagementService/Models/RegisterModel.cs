namespace UserManagementService.Models
{
#nullable disable
    public class RegisterModel : LoginModel
    {
        public string ConfirmPassword { get; set; }
    }
}
