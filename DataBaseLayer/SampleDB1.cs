
using System.Data;
using OpenApiProject1.DBCon;

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
    }
}