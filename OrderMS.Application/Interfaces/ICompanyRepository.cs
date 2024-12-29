using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderMS.Application.DTOs;

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
    }
}
