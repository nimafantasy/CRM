using Web.Framework.Mvc;

namespace Models.Home
{
    public partial class DashboardModel : BaseNopModel
    {
        public bool IsLoggedInAsVendor { get; set; }
    }
}