using System;

namespace Microsoft.eShopWeb.Infrastructure.Identity
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }
    }
}
