using System;

namespace SimpleAction.Common.Auth
{
    public interface IJwtHandler
    {
         MyJsonWebToken Create (Guid userId);
    }
}