using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Kal_Be_KKL.Models.DAL
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBServices()
        {

        }

        public SqlConnection connect(String conString)
        {
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        public int Insert_Requests(Request req)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(req);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildInsertCommand(Request req)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}')",
                 req.Id, req.Request_Date.ToString("yyyy-MM-dd"), req.Request_Status);
            String prefix = "INSERT INTO kkl_Request " + "([Id], [Request_Date], [Request_Status])";
            command = prefix + sb.ToString();

            return command;
        }

        public int Delete_Request(string id, int month)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = "Delete from kkl_Request where Id = '" + id + "' AND MONTH(Request_Date) = "+month;

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }




        public Employee LogIn(string id, string passowrd)
        {
            SqlConnection con = null;
            Employee e = new Employee();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from kkl_Employee where Id = '" + id + "' and Password = '" + passowrd + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    e.Id = (string)(dr["Id"]);
                    e.First_Name = (string)dr["First_Name"];
                    e.Last_Name = (string)dr["Last_Name"];
                    e.Gender = (string)dr["Gender"];
                    e.Birth_Date = Convert.ToDateTime(dr["Birth_Date"]);
                    e.Phone_Number = (string)dr["Phone_Number"];
                    e.Mail = (string)dr["Mail"];
                    e.Password = (string)dr["Password"];
                    e.Deleted = Convert.ToBoolean(dr["Deleted"]);
                }
                return e;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int Insert_Employee(Employee emp)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(emp);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(Employee emp)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}','{7}')",
                emp.Id, emp.First_Name, emp.Last_Name, emp.Gender, emp.Birth_Date.ToString("yyyy-MM-dd"), emp.Phone_Number, emp.Mail, emp.Password);
            String prefix = "INSERT INTO kkl_Employee " + "([Id],[First_Name], [Last_Name], [Gender], [Birth_Date], [Phone_Number], [Mail], [Password])";
            command = prefix + sb.ToString();

            return command;
        }
        
            public List<Area> Read_Area()
        {
            SqlConnection con = null;
            List<Area> areaList = new List<Area>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from kkl_Area";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Area area = new Area();
                    area.Region_Id = Convert.ToInt32(dr["Region_Id"]);
                    area.Area_Id = Convert.ToInt32(dr["Area_Id"]);
                    area.Area_Name = (string)dr["Area_Name"];
                    area.Patrol_Id = (string)dr["Patrolman_Id"];
                    area.Manager_Id = (string)dr["Manager_Id"];
                    area.Forester_Id = (string)dr["Forester_Id"];
                    areaList.Add(area);
                }
                return areaList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        } 
        public List<Course> Read_Courses()
        {
            SqlConnection con = null;
            List<Course> courseList = new List<Course>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from kkl_Course";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Course course = new Course();
                    course.Course_Id = Convert.ToInt32(dr["Course_Id"]);
                    course.Course_Name = (string)dr["Course_Name"];
                    course.Description = (string)dr["Description"];
                    course.Validity = Convert.ToInt32(dr["Validity"]);

                    courseList.Add(course);
                }
                return courseList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int insert_course_of_duty(DateTime Receipt_Course_Date, int Course_Id, string Id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(Receipt_Course_Date, Course_Id, Id);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(DateTime Receipt_Course_Date, int Course_Id, string Id)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}')",
                Receipt_Course_Date.ToString("yyyy-MM-dd"), Course_Id, Id);
            String prefix = "INSERT INTO kkl_Courses_of_Duty " + "([Receipt_Course_Date],[Course_Id], [Id])";
            command = prefix + sb.ToString();

            return command;
        }

        public List<ShiftRequirement> Read_Requirements()
        {
            SqlConnection con = null;
            List<ShiftRequirement> shift_Requirement_List = new List<ShiftRequirement>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from kkl_Shift_Requirements";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    ShiftRequirement shift_Requirement = new ShiftRequirement();
                    shift_Requirement.Requirement_Id = Convert.ToInt32(dr["Requirement_Id"]);
                    shift_Requirement.Requirement_Name = (string)dr["Requirement_Name"];
                    shift_Requirement_List.Add(shift_Requirement);
                }
                return shift_Requirement_List;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<Block> Read_Blocks()
        {
            SqlConnection con = null;
            List<Block> block_List = new List<Block>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from kkl_Block";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Block block = new Block();
                    block.Area_Id = Convert.ToInt32(dr["Area_Id"]);
                    block.Block_Id = Convert.ToInt32(dr["Block_Id"]);
                    block.Block_Name = (string)dr["Block_Name"];
                    block_List.Add(block);
                }
                return block_List;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<BlockShiftRequirementWithName> Read_PermanentRequirement(int blockId)
        {
            SqlConnection con = null;
            List<BlockShiftRequirementWithName> permanentRequirements_List = new List<BlockShiftRequirementWithName>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @"select Requirement_Name,kp.Quantity,Comments
                                      from kkl_Permanent_Requirements_To_Block_In_a_shift kp join kkl_Shift_Requirements ks on kp.Requirement_Id = ks.Requirement_Id
                                         where kp.Block_Id = " + blockId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    BlockShiftRequirementWithName permanentRequirement = new BlockShiftRequirementWithName();
                    permanentRequirement.Requirement_Name = (string)dr["Requirement_Name"];
                    permanentRequirement.Quantity = Convert.ToInt32(dr["Quantity"]);
                    permanentRequirement.Comments = dr["Comments"] != DBNull.Value ? (string)dr["Comments"] : default; ;
                    permanentRequirements_List.Add(permanentRequirement);
                }
                return permanentRequirements_List;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<BlockShiftRequirementWithName> Read_SpecialRequirement(int blockId,DateTime shiftDate)
        {
            SqlConnection con = null;
            List<BlockShiftRequirementWithName> specialRequirements_List = new List<BlockShiftRequirementWithName>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @" select Requirement_Name,sr.Quantity,Comments
                                      from kkl_Special_requirements_for_a_day_in_a_shift_of_a_block sr join kkl_Shift_Requirements ks on sr.Requirement_Id = ks.Requirement_Id
                                      where sr.Block_Id = "+blockId+" and sr.Shift_Date= '" + shiftDate.ToString("yyyy-MM-dd")+"'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    BlockShiftRequirementWithName specialRequirement = new BlockShiftRequirementWithName();
                    specialRequirement.Requirement_Name = (string)dr["Requirement_Name"];
                    specialRequirement.Quantity = Convert.ToInt32(dr["Quantity"]);
                    specialRequirement.Comments = dr["Comments"] != DBNull.Value ? (string)dr["Comments"] : default; ;
                    specialRequirements_List.Add(specialRequirement);
                }
                return specialRequirements_List;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int InsertSpecialRequirement(SpecialRequirement specialRequirement)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildSpecialRequirementInsertCommand(specialRequirement);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private String BuildSpecialRequirementInsertCommand(SpecialRequirement sr)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}')",
                 sr.Block_Id, sr.Shift_Date.ToString("yyyy-MM-dd"), sr.Requirement_Id, sr.Quantity, sr.Comments);
            String prefix = "INSERT INTO kkl_Special_requirements_for_a_day_in_a_shift_of_a_block " + "([Block_Id], [Shift_Date], [Requirement_Id], [Quantity], [Comments])";
            command = prefix + sb.ToString();

            return command;
        }

    }
}