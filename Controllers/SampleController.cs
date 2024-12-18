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
using System.Text.Json.Serialization;
using System.Text.Json;
using OpenApiProject1.SingletonService;


//using OpenApiProject1.Models;

namespace OpenApiProject1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {

        private readonly MySingletonService _singletonService;
        public readonly ModelValidators _objModelValidators;
        public SampleController(MySingletonService singletonService)
        {
            //Console.WriteLine("Controller");
            _objModelValidators = new ModelValidators();
            _singletonService = singletonService;
        }


        [HttpGet("Get")]
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
            string jsonString = JsonSerializer.Serialize(DataTableToList(d.Tables[0]));
            //Console.WriteLine(jsonString); // Log this JSON string
          //  _singletonService.LogInformation($"Output Data {jsonString}");
            _singletonService.LogMessage($"Output Data {jsonString}");
            
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

        [HttpPost("Post")]
        public ActionResult<LoanDetail> PostTModel([FromBody] LoanDetail loan)
        {
            // ModelValidators vd = new ModelValidators();
            string jsonString = JsonSerializer.Serialize(loan);
           // _logger.LogInformation($"Input Data {jsonString}");
                    _singletonService.LogMessage($"Input Data {jsonString}");
            string s = _objModelValidators.LoandetailsValidator(loan);
              BusinessGet b = new BusinessGet();
               
            if (s == "ok")
            {
               int Result = b.BuinessInsert( loan);
               if (Result > 0){
                    return Ok ("Successfully Inserted");
               }
               else{
                    return BadRequest("Data not get Inserted");
               }
            }
            else
            {
                return NotFound();
            }


        }

        static List<Dictionary<string, object>> DataTableToList(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }
            return list;
        }

        [HttpPut("Update")]

        public async Task<IActionResult> Put(int id, [FromBody] LoanDetail loan)
        {

            if (id == 0) { return NotFound(); }
            else
            {
                //  await.task.wait(1000);
                BusinessGet b = new BusinessGet();
                int Result = b.BuinessPut(id, loan);
                if (Result != 0)
                {
                    return Ok("SuccessFully updated");
                }
                else
                {
                    return BadRequest();
                }


            }

        }


        [HttpDelete("Delete")]

        public async Task<IActionResult> Delete(int id)
        {

            //try{

            BusinessGet b = new BusinessGet();
            int Result = b.BuinessDelete(id);

            if (Result > 0)
            {
                return Ok("SuccessFully Deleted");
            }
            else
            {
                return BadRequest();
            }


        }
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


