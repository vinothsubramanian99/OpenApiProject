using OpenApiProject1.Models;
using OpenApiProject1.MiddleWareExceptionHandeling;

namespace OpenApiProject1.Validators
{
    public class ModelValidators
    {
       //  private readonly ILogger<ModelValidators> _logger;
       // public ModelValidators(ILogger<ModelValidators> logger){
       //     _logger=logger;
       // }
        public string LoandetailsValidator(LoanDetail Loan)
        {

            if (Loan.LoanNumber == "")
            {
                throw new ValidationException("Loan Number should not be empty");
            }
            else
            {

                return "ok";
            }
        }
    }

}