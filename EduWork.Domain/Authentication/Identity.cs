using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Domain.Authentication
{
    public class Identity : IIdentity
    {
        private const string NAME_CLAIM = "name";
        private const string EMAIL_CLAIM = "preferred_username";
        private const string OID_CLAIM = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        private readonly ClaimsPrincipal _principal;

        public Identity(IHttpContextAccessor httpContextAccesor)
        {
            _principal = httpContextAccesor.HttpContext!.User;
        }
        public string? DisplayName 
        { 
            get{
                return _principal.Claims.FirstOrDefault(_ => _.Type == NAME_CLAIM)?.Value;
            } 
        }
        public string? Email { 
            get{
                return _principal.Claims.FirstOrDefault(_ => _.Type == EMAIL_CLAIM)?.Value;
            } 
        }
        public Guid? ObjectId {
            get
            {
                var result = Guid.TryParse(_principal.Claims.FirstOrDefault(_ => _.Type == OID_CLAIM)?.Value, out var oid);
                return result ? oid : null;
            }
        }
    }
}
