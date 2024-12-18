
using System.Data;
using OpenApiProject1.DBCon;
using OpenApiProject1.Models;

namespace OpenApiProject1.SampleDB1
{
    public class SampleDB
    {
        

        
        public DataSet GetDetails()
        {
          //_logger.LogInformation("DB called");
            string query = "select * from loandetail";
            DBConnection db = new DBConnection();
            DataSet dt = db.GetConnection(query);
            return dt;


        }


     public int DBPut(int id,LoanDetail loan)
        {
          //_logger.LogInformation("DB called");
            string query = $"update loandetail set loannumber='{loan.LoanNumber}' where lid={id} ";
            DBConnection db = new DBConnection();
           int t = db.ExecuteUpdate(query);
            return t;


        }

          public int DBDelete(int id)
        {
          //_logger.LogInformation("DB called");
            string query = $"delete from  loandetail  where lid={id} ";
            DBConnection db = new DBConnection();
           int t = db.ExecuteUpdate(query);
            return t;


        }

         public int DBInsert(LoanDetail loan)
        {
          //_logger.LogInformation("DB called");
            string query = $"Insert into loandetail (Loannumber,username)values ('{loan.LoanNumber}','{loan.UserName}') ";
            DBConnection db = new DBConnection();
           int t = db.ExecuteUpdate(query);
            return t;


        }
    }
}