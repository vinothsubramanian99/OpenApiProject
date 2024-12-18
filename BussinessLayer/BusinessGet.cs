
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using OpenApiProject1.Models;
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

            public int BuinessPut(int id,LoanDetail loan)
        {
            int dt ;

            SampleDB db = new SampleDB();
            dt =db.DBPut(id,loan);
     
            return dt;
        }

             public int BuinessDelete(int id)
        {
            int dt ;

            SampleDB db = new SampleDB();
            dt =db.DBDelete(id);
     
            return dt;
        }

        public int BuinessInsert(LoanDetail loan)
        {
            int dt ;

            SampleDB db = new SampleDB();
            dt =db.DBInsert(loan);
     
            return dt;
        }

    }

    //public interface IDataAccess
    //{
    //}
}