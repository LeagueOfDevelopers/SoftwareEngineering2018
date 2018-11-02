using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoDSprintApi.Models
{
    public class AdministratorModel 
    {
        public AdministratorModel()
        {
            Id = Guid.Empty;
            NickName = "admin";
        }

        public Guid Id { get; }

        public string NickName { get; }
    }
}
