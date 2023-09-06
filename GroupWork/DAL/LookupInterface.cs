using GroupWork.Models;

namespace GroupWork.DAL
{
    public interface LookupInterface
    {
        Task AddAsync(LookupModel model);
        Task <LookupModel>FindByCompanyIdAsync(int  companyId);
        Task DeleteAsync(LookupModel model);
    }
}
