using Ardalis.Specification;
using BaseProject.Models.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Specification
{
    public class RefreshTokenSpec : Specification<RefreshToken>, ISingleResultSpecification
    {
        public RefreshTokenSpec(string token)
        {
            Query.Where(item => item.Token == token);
        }
    }
}
