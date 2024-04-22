using ClubWebsite.Models.Common;
using ClubWebsite.Models.Payment;

namespace ClubWebsite.Models.Join
{
    public class MemberExecutePaymentModel
    {
        public MemberViewModel MemberViewModel { get; set; }
        public BkashExecutePaymentReturnModel BkashExecutePaymentReturnModel { get; set; }
    }
}
