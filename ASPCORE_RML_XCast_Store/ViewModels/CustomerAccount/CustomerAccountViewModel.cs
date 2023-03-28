namespace RMLXCast.Web.ViewModels.CustomerAccount
{
    public class CustomerAccountViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public CustomerOrdersViewModel CustomerOrdersViewModel { get; set; }

        public CustomerChangePasswordViewModel CustomerChangePasswordViewModel { get; set; }

    }
}
