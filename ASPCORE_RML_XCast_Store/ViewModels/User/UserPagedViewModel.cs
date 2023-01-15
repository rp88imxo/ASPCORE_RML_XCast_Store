using RMLXCast.Core.Domain.Orders;

namespace RMLXCast.Web.ViewModels.User
{
    public class UserPagedViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OrdersCount { get; set; }

        public string Roles { get; set; }

        public DateTime RegistrationDateUtc { get; set; }
    }
}
