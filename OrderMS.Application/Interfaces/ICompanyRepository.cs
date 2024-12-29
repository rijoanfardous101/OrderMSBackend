using OrderMS.Application.DTOs.CompanyDTOs;
using OrderMS.Application.DTOs.UserDTOs;
using OrderMS.Domain.Entities;

namespace OrderMS.Application.Interfaces
{
    public interface ICompanyRepository
    {
        /// <summary>
        /// Takes company name and email and returns company id.
        /// </summary>
        /// <param name="signupReqDTO"></param>
        /// <returns></returns>
        public Task<Guid> CreateCompanyAsync(SignupReqDTO signupReqDTO);

        public Task<Company?> GetCompanyById(Guid cId);

        public Task<Company?> UpdateCompanyInfoAsync(ChangeCompanyInfoReqDTO reqDTO, Guid companyId);

    }
}
