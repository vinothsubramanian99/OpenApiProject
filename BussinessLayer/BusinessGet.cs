
using System.Data;
using Microsoft.AspNetCore.Mvc;
using OpenApiProject1.SampleDB1;
namespace OpenApiProject1.BusinessLayer
{
    public class BusinessGet
    {
        public System.Data.DataSet GetTModels()
        {
            DataSet dt =new DataSet();
            Console.WriteLine("hii Im in business");
            SampleDB db = new SampleDB();
            dt =db.GetDetails();
            Console.WriteLine(dt.Tables.Count);
            return dt;
        }
    }

    //public interface IDataAccess
    //{
    //}
}