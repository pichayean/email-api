using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable
namespace email_api.Database;
public partial class AuthenticationHistory
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public string Email { get; set; }
    public string SignInDate { get; set; }
}