using System;
using System.Collections.Generic;
using Api.Models.Mongo;

namespace Api.Models.Commands
{
    public class GetPasswordResult
    {
        public List<PasswordView> Passwords { get; set; }
    }

    public class PasswordView
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}