using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Services.DtoEntities
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsBanned { get; set; }
        public string ReasonForBan { get; set; }
    }
}
