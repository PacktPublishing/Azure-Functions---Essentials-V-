#r "System.Data"
#r "System.Configuration"
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
public static async Task<HttpResponseMessage>
Run(HttpRequestMessage req, TraceWriter log)
{
dynamic data = await req.Content.ReadAsAsync<object>();
string firstname, lastname, email, devicelist;
firstname = data.firstname;
lastname = data.lastname;
email = data.email;
devicelist = data.devicelist;
SqlConnection con =null;
try
{
string query = "INSERT INTO EmployeeInfo (firstname,lastname, email, devicelist) " + "VALUES (@firstname,@lastname, @email, @devicelist) ";
con = new SqlConnection(ConfigurationManager.ConnectionStrings
["MyConnectionString"].ConnectionString);
SqlCommand cmd = new SqlCommand(query, con);
cmd.Parameters.Add("@firstname", SqlDbType.VarChar,50).Value = firstname;
cmd.Parameters.Add("@lastname", SqlDbType.VarChar, 50).Value = lastname;
cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
cmd.Parameters.Add("@devicelist", SqlDbType.VarChar).Value = devicelist;
con.Open();
cmd.ExecuteNonQuery();
}
catch(Exception ex)
{
log.Info(ex.Message);
}
finally
{
if(con!=null)
{
con.Close();
}
}
return req.CreateResponse(HttpStatusCode.OK, "Hello ");
}
