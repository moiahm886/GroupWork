using GroupWork.Models;

namespace GroupWork.DAL
{
    public interface CompanyInterface
    {
        Task<List<CompanyModel>> GetCompanyListByCountryAsync(int Id);
        Task<List<CompanyModel>> GetCompany();
        Task AddCompany(CompanyModel company);
        Task<CompanyModel> FindCompanyByID(int? ID);
        void RemoveCompany(CompanyModel company);
        Task savechanges();
        Task<List<BranchModel>> FindBranches(int ID);
        void RemoveBranches(List<BranchModel> branch);
        Task<List<BranchModel>> GetBranches();
        Task<CompanyModel> FindBranchThroughCompany(int ID);
        Task AddBranch(BranchModel branch);
        Task <BranchModel> FindBranchesById(int ID);
        Task RemoveBranch(BranchModel branch);

    }
}
