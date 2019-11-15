using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Data.DtoEntities
{
    public class BarRatingDto
    {
        public double Value { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid BarId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
