using System;

namespace CocktailWizard.Services.DtoEntities
{
    public class BarCommentDto
    {
        public Guid Id { get; set; }
        public Guid BarId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } 
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
