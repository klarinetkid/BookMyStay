using BookMyStay.Api.Common;
using BookMyStay.DataServices;

namespace BookMyStay.Api.Models
{
    public class BaseViewModel
    {
        internal ApplicationDbContext dbContext = Helper.GetDbContext();
    }
}
