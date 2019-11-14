using CocktailWizard.Data.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Services.Tests
{
    public static class TestUtilities
    {
        public static DbContextOptions<CWContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<CWContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
