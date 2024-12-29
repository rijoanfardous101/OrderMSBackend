using System;
using OrderMS.Application.DTOs.CompanyDTOs;
using OrderMS.Application.DTOs.UserDTOs;
using OrderMS.Application.Interfaces;
using OrderMS.Domain.Entities;
using OrderMS.Persistence.DatabaseContext;

namespace OrderMS.Persistence.Services
{
    public class CompanyService : ICompanyRepository
    {
        private readonly OrderMSDbContext _dbContext;

        public CompanyService(OrderMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateCompanyAsync(SignupReqDTO signupReqDTO)
        {
            var company = new Company()
            {
                CompanyId = Guid.NewGuid(),
                CompanyName = signupReqDTO.CompanyName,
                Email = signupReqDTO.EmailAddress,
                CreatedAt = DateTime.UtcNow,
            };

            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();

            return company.CompanyId;
        }

        public async Task<Company?> GetCompanyById(Guid cId)
        {
            return await _dbContext.Companies.FindAsync(cId);
        }

        public async Task<Company?> UpdateCompanyInfoAsync(ChangeCompanyInfoReqDTO reqDTO, Guid companyId)
        {
            var company = await GetCompanyById(companyId);

            if (company == null)
                return null;

            company.CompanyAddress = reqDTO.Address;
            company.CompanyDescription = reqDTO.Description;

            await _dbContext.SaveChangesAsync();

            return company;
        }
    }
}
