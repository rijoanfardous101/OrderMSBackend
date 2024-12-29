using System;
using OrderMS.Application.DTOs;
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
            };

            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();

            return company.CompanyId;
        }
    }
}
