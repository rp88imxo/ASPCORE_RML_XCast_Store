namespace RMLXCast.Web.ViewModels.CustomerAccount
{
    public class CustomerAccountViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CustomerOrdersViewModel CustomerOrdersViewModel { get; set; }

    }
}
