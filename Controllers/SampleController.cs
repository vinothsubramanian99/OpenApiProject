using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenApiProject1.Models;
using OpenApiProject1.BusinessLayer;
using System.Collections;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using OpenApiProject1.Validators;


//using OpenApiProject1.Models;

namespace OpenApiProject1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {

        private readonly ILogger<SampleController> _logger;
        public readonly ModelValidators _objModelValidators;
        public SampleController(ILogger<SampleController> logger)
        {
            //Console.WriteLine("Controller");
           _objModelValidators = new ModelValidators();
            _logger = logger;
        }


        [HttpGet("")]
        public ActionResult<IEnumerable<LoanDetail>> GetTModels()
        {
          //  Console.WriteLine("Controller");
            //_logger.LogInformation("GetTModels called");
            BusinessGet b = new BusinessGet();
            //_logger.LogInformation("result received");
            DataSet d = b.GetTModels();
            Console.WriteLine("Controller got Output");
            Console.WriteLine(d.Tables.Count);
            //return Ok(d.Tables[0]);

            var jsonFriendlyResult = ConvertDataTableToDictionary(d.Tables[0]);
            return Ok(jsonFriendlyResult);

        }
        private List<Dictionary<string, object>> ConvertDataTableToDictionary(DataTable dataTable)
        {
            var result = new List<Dictionary<string, object>>();
            foreach (DataRow row in dataTable.Rows)
            {
                var rowDict = dataTable.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => row[col]);
                result.Add(rowDict);
            }
            return result;
        }
        //return Ok();

        [HttpPost("")]
        public ActionResult<LoanDetail> PostTModel([FromBody] LoanDetail loan)
        {
           // ModelValidators vd = new ModelValidators();
            string s = _objModelValidators.LoandetailsValidator(loan);
            if (s == "ok")
            {
                return Ok(loan);
            }
            else
            {
                return NotFound();
            }


        }


        /* [HttpGet("{id}")]
         public async Task<ActionResult<TModel>> GetTModelById(int id)
         {
             // TODO: Your code here
             await Task.Yield();

             return null;
         }


         [HttpPut("{id}")]
         public async Task<IActionResult> PutTModel(int id, TModel model)
         {
             // TODO: Your code here
             await Task.Yield();

             return NoContent();
         }

         [HttpDelete("{id}")]
         public async Task<ActionResult<TModel>> DeleteTModelById(int id)
         {
             // TODO: Your code here
             await Task.Yield();

             return null;
         }*/
    }
}
