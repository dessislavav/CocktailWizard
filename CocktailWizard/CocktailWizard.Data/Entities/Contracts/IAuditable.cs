using System;

namespace CocktailWizard.Data.Entities.Contracts
{
    public interface IAuditable
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
    }
}
