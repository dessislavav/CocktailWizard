using System;

namespace CocktailWizard.Data.DtoEntities
{
    public class BarCommentDto
    {
        public Guid BarId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Body { get; set; }
    }
}
