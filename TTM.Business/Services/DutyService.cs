using TTM.DataAccess;
using TTM.Domain;

namespace TTM.Business.Services
{
    public class DutyService : BaseService<DutyDto, Duty>
    {
        public DutyService(TTMContext ttmContext) : base(ttmContext)
        {
        }
    }
}
