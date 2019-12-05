using System;

namespace Microsoft.eShopWeb.Infrastructure.Identity
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public bool EmailConfirmed { get; set; }
        
        public bool TwoFactorEnabled { get; set; }
        
        public string PasswordHash { get; set; }
    }
}
