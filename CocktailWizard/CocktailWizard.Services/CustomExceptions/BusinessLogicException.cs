using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Services.CustomExceptions
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException()
        {

        }

        public BusinessLogicException(string message) 
            : base(message)
        {

        }

        public BusinessLogicException(string message, Exception inner) 
            : base(message, inner)
        {

        }
    }
}
