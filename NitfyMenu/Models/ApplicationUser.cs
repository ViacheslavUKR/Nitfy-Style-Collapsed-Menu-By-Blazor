using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

//changed CreateIdentityAsync/DefaultAuthenticationTypes delete in NetCore 3.1

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public string GoogleAuthenticatorSecretKey { get; set; }
        public string IPAddress { get; set; }

    }
}