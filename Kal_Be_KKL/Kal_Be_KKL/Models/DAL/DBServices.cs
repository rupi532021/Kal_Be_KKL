using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
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

            String cStr = "Delete from kkl_Request where Id = '" + id + "' AND MONTH(Request_Date) = " + month;

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

        public List<Employee> GetAllEmployee()
        {
            SqlConnection con = null;

            List<Employee> empList = new List<Employee>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from kkl_Employee";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Employee e = new Employee();
                    e.Id = (string)(dr["Id"]);
                    e.First_Name = (string)dr["First_Name"];
                    e.Last_Name = (string)dr["Last_Name"];
                    e.Gender = (string)dr["Gender"];
                    e.Birth_Date = Convert.ToDateTime(dr["Birth_Date"]);
                    e.Phone_Number = (string)dr["Phone_Number"];
                    e.Mail = (string)dr["Mail"];
                    e.Password = (string)dr["Password"];
                    e.Deleted = Convert.ToBoolean(dr["Deleted"]);
                    empList.Add(e);
                }
                return empList;
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




        public List<Employee> GetAllPossibleEmployees(DateTime exdate, int myJob, int area_id)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;
            List<Employee> emplist = new List<Employee>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                }
                catch (Exception)
                {

                    throw;
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "sp_Change";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Area_Id", area_id);
                cmd.Parameters.AddWithValue("@Requirement_Id", area_id);
                cmd.Parameters.AddWithValue("@Request_Date", exdate);


                // get a reader
                try
                {
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                }
                catch (Exception)
                {

                    throw;
                }


                while (dr.Read())
                {
                    Employee emp = new Employee();
                    // Read till the end of the data into a row
                    emp.Id = (string)(dr["Id"]);
                    emp.First_Name = (string)dr["First_Name"];
                    emp.Last_Name = (string)dr["Last_Name"];
                    emplist.Add(emp);
                }
                return emplist;
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

        public int GetJobId(string jobname)
        {
            SqlConnection con = null;
            object dbJobId = new object();
            int JobId = -1;
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT Requirement_Id FROM kkl_Shift_Requirements WHERE Requirement_Name = @Requirement_Name";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                cmd.Parameters.AddWithValue("@Requirement_Name", jobname);

                // get a reader
                dbJobId = cmd.ExecuteScalar(); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                if (dbJobId == null || !int.TryParse(dbJobId.ToString(), out JobId))
                {
                    return 0;
                }

                return JobId;
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
            catch (SqlException ex)
            {

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

        public int Insert_Worker_In_Area(Worker_In_Area WIA)
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

            String cStr = BuildInsertCommand(WIA);      // helper method to build the insert string

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
        private String BuildInsertCommand(Worker_In_Area WIA)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}')",
                WIA.Id, WIA.Area_Id, WIA.Job_Id, WIA.Job_Start_Date.ToString("yyyy-MM-dd"));
            String prefix = "INSERT INTO kkl_Worker_In_Area " + "([Id],[Area_Id], [Job_Id], [Job_Start_Date])";
            command = prefix + sb.ToString();

            return command;
        }

        public int Update_SpecialRequirement(SpecialRequirement specialRequirement)
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

            String cStr = BuildSpecialRequirementUpdateCommand(specialRequirement);      // helper method to build the update string

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
        private String BuildSpecialRequirementUpdateCommand(SpecialRequirement sr)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("SET[Block_Id] ={0}, [Shift_Date]='{1}', Requirement_Id ={2}, Quantity ={3}, Comments ='{4}'",
                 sr.Block_Id, sr.Shift_Date.ToString("yyyy-MM-dd"), sr.Requirement_Id, sr.Quantity, sr.Comments);
            String prefix = "UPDATE kkl_Special_requirements_for_a_day_in_a_shift_of_a_block ";
            command = prefix + sb.ToString() + " WHERE Block_Id =  " + sr.Block_Id + "and Shift_Date = '" + sr.Shift_Date.ToString("yyyy-MM-dd") + "' and Requirement_Id = " + sr.Requirement_Id;
            return command;

        }

        public int Insert_Worker_In_Region(Worker_In_Region WIR)
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

            String cStr = BuildInsertCommand(WIR);      // helper method to build the insert string

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
        private String BuildInsertCommand(Worker_In_Region WIR)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}')",
                WIR.Id, WIR.Region_Id, WIR.Job_Id, WIR.Job_Start_Date.ToString("yyyy-MM-dd"));
            String prefix = "INSERT INTO kkl_Worker_In_Region " + "([Id],[Region_Id], [Job_Id], [Job_Start_Date])";
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
        public List<Courses_Of_Duty> GetAllCoursesOfDuty(string id)
        {
            SqlConnection con = null;
            List<Courses_Of_Duty> CoursesOfDuty = new List<Courses_Of_Duty>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from kkl_Courses_of_Duty where Id = " + id + "AND Is_Deleted = " + 0;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Courses_Of_Duty course_of_duty = new Courses_Of_Duty();
                    course_of_duty.Course_Id = Convert.ToInt32(dr["Course_Id"]);
                    course_of_duty.Id = (string)dr["Id"];
                    course_of_duty.Receipt_Course_Date = Convert.ToDateTime(dr["Receipt_Course_Date"]);

                    CoursesOfDuty.Add(course_of_duty);
                }
                return CoursesOfDuty;
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
        public bool Delete_Course_Of_Duty(Courses_Of_Duty cod)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                //String selectSTR = "select * from kkl_Courses_of_Duty where Id = " + cod.Id + "AND Is_Deleted = " + 0 + "AND Course_Id = ";



                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Id", cod.Id);
                cmd.Parameters.AddWithValue("@Receipt_Course_Date", cod.Receipt_Course_Date);
                cmd.Parameters.AddWithValue("@Course_Id", cod.Course_Id);
                String selectSTR = "UPDATE kkl_Courses_of_Duty SET Is_Deleted = 1 WHERE Id = @Id AND Is_Deleted = 0 AND Course_Id = @Course_Id AND Receipt_Course_Date = @Receipt_Course_Date";
                cmd.CommandText = selectSTR;
                // get a reader
                cmd.ExecuteNonQuery();
                return true;
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

        public bool insert_course_of_duty(Courses_Of_Duty[] cod)
        {

            SqlConnection con;
            SqlCommand cmd = new SqlCommand();

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd.Connection = con;
            cmd.CommandText = "sp_Add_Courses_To_DB";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Receipt_Course_Date", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Course_Id", SqlDbType.Int);
            cmd.Parameters.Add("@RetVal", SqlDbType.Int);
            cmd.Parameters["@RetVal"].Direction = ParameterDirection.ReturnValue;
            for (int i = 0; i < cod.Length; i++)
            {
                cmd.Parameters["@Id"].Value = cod[i].Id;
                cmd.Parameters["@Receipt_Course_Date"].Value = cod[i].Receipt_Course_Date.ToString("yyyy-MM-dd");
                cmd.Parameters["@Course_Id"].Value = cod[i].Course_Id;
                try
                {
                    cmd.ExecuteNonQuery();
                    int RetVal = (int)cmd.Parameters["@RetVal"].Value;

                }
                catch (Exception ex)
                {
                    // write to log
                    con.Close();
                    return false;

                }
            }

            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            return true;
        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
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

        public List<BlockShiftRequirementWithName> Read_SpecialRequirement(int blockId, DateTime shiftDate)
        {
            SqlConnection con = null;
            List<BlockShiftRequirementWithName> specialRequirements_List = new List<BlockShiftRequirementWithName>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @" select Requirement_Name,sr.Quantity,Comments, sr.Requirement_Id
                                      from kkl_Special_requirements_for_a_day_in_a_shift_of_a_block sr join kkl_Shift_Requirements ks on sr.Requirement_Id = ks.Requirement_Id
                                      where sr.Block_Id = " + blockId + " and sr.Shift_Date= '" + shiftDate.ToString("yyyy-MM-dd") + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    BlockShiftRequirementWithName specialRequirement = new BlockShiftRequirementWithName();
                    specialRequirement.Requirement_Name = (string)dr["Requirement_Name"];
                    specialRequirement.Quantity = Convert.ToInt32(dr["Quantity"]);
                    specialRequirement.Requirement_Id = Convert.ToInt32(dr["Requirement_Id"]);
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

        public int InsertDayInShift(Day_In_Shift dayInShift)
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

            String cStr = BuildInsertCommand(dayInShift);      // helper method to build the insert string

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
        private String BuildInsertCommand(Day_In_Shift dayInShift)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}','{4}')",
                dayInShift.Id, dayInShift.Block_Id, dayInShift.Shift_Date.ToString("yyyy-MM-dd"), dayInShift.Requirement_Id, dayInShift.Iteration_Number);
            String prefix = "INSERT INTO kkl_Day_In_Shift " + "([Id],[Block_Id], [Shift_Date],[Requirement_Id],[Iteration_Number])";
            command = prefix + sb.ToString();

            return command;
        }

        public List<Day_In_Shift> ReadDayInShift()
        {
            SqlConnection con = null;
            List<Day_In_Shift> DayInShiftList = new List<Day_In_Shift>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from kkl_Day_In_Shift";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Day_In_Shift dayInShift = new Day_In_Shift();
                    dayInShift.Id = (string)dr["Id"];
                    dayInShift.Block_Id = Convert.ToInt32(dr["Block_Id"]);
                    dayInShift.Shift_Date = Convert.ToDateTime(dr["Shift_Date"]);
                    dayInShift.Requirement_Id = Convert.ToInt32(dr["Requirement_Id"]);
                    DayInShiftList.Add(dayInShift);
                }
                return DayInShiftList;
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

        public List<RequirementForSpecificShift> ReadPermantReqListForBlock(int blockId)
        {
            SqlConnection con = null;
            List<RequirementForSpecificShift> permantReqListForBlock = new List<RequirementForSpecificShift>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @"select pr.Requirement_Id,pr.Quantity,sr.Requirement_Name
                                    from kkl_Permanent_Requirements_To_Block_In_a_shift pr inner join
                                    kkl_Shift_Requirements sr on pr.Requirement_Id=sr.Requirement_Id
                                    where pr.Block_Id=" + blockId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    RequirementForSpecificShift req = new RequirementForSpecificShift();
                    req.Requirement_Id = Convert.ToInt32(dr["Requirement_Id"]);
                    req.Requirement_Name = (string)dr["Requirement_Name"];
                    req.Quantity = Convert.ToInt32(dr["Quantity"]);
                    permantReqListForBlock.Add(req);
                }
                return permantReqListForBlock;
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

        public int If_SpecialRequirement_Is_Exist(SpecialRequirement specialRequirement)
        {
            SqlConnection con = null;
            RequirementForSpecificShift req = new RequirementForSpecificShift();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @"Select* from kkl_Special_requirements_for_a_day_in_a_shift_of_a_block where[Block_Id] = '" + specialRequirement.Block_Id + "' and[Shift_Date] = " +
                " '" + specialRequirement.Shift_Date.ToString("yyyy-MM-dd") + "' and [Requirement_Id] = '" + specialRequirement.Requirement_Id + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    req.Requirement_Id = Convert.ToInt32(dr["Requirement_Id"]);
                    req.Quantity = Convert.ToInt32(dr["Quantity"]);
                }
                if (req.Requirement_Id != 0)
                    return req.Quantity;
                return 0;
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


        public int Delete_SpecialRequirement(int blockId, DateTime Shift_Date, int Requirement_Id)
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

            String cStr = "Delete from kkl_Special_requirements_for_a_day_in_a_shift_of_a_block where [Block_Id] = '" + blockId + "' and [Shift_Date] =" +
                " '" + Shift_Date.ToString("yyyy-MM-dd") + "' and [Requirement_Id] = '" + Requirement_Id + "'";      // helper method to build the insert string

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


        public List<RequirementForSpecificShift> ReadSpeciaelReqListForBlock(int blockId, DateTime Shift_Date)
        {
            SqlConnection con = null;
            List<RequirementForSpecificShift> speciaelReqListForBlock = new List<RequirementForSpecificShift>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @"select srd.Requirement_Id,srd.Quantity,sr.Requirement_Name
                                    from kkl_Special_requirements_for_a_day_in_a_shift_of_a_block srd inner join
                                    kkl_Shift_Requirements sr on srd.Requirement_Id = sr.Requirement_Id
                                    where srd.Block_Id=" + blockId + @" and
                                    srd.Shift_Date='" + Shift_Date.ToString("yyyy-MM-dd") + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    RequirementForSpecificShift req = new RequirementForSpecificShift();
                    req.Requirement_Id = Convert.ToInt32(dr["Requirement_Id"]);
                    req.Requirement_Name = (string)dr["Requirement_Name"];
                    req.Quantity = Convert.ToInt32(dr["Quantity"]);
                    speciaelReqListForBlock.Add(req);
                }
                return speciaelReqListForBlock;
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

        public Employee FindMatchEmployee(int Area_Id, string Request_Status, DateTime Request_Date, int Requirement_Id, int iteration_Number)
        {

            SqlConnection con = null;
            SqlDataReader dr = null;
            Employee emp = new Employee();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                }
                catch (Exception)
                {

                    throw;
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "sp_smartAssign";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Area_Id", Area_Id);
                cmd.Parameters.AddWithValue("@Request_Status", Request_Status);
                cmd.Parameters.AddWithValue("@Request_Date", Request_Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Requirement_Id", Requirement_Id);
                cmd.Parameters.AddWithValue("@Iteration_Number", iteration_Number);

                // get a reader
                try
                {
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                }
                catch (Exception)
                {

                    throw;
                }


                while (dr.Read())
                {
                    // Read till the end of the data into a row
                    emp.Id = (string)(dr["Id"]);
                    emp.First_Name = (string)dr["First_Name"];
                    emp.Last_Name = (string)dr["Last_Name"];
                }
                return emp;
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


        public int InsertEmployeeToShift(string id, int blockId, DateTime shift_Date, int Requirement_Id, int iteration_Number)
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

            String cStr = BuildInsertCommand(id, blockId, shift_Date, Requirement_Id, iteration_Number);      // helper method to build the insert string

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

        private String BuildInsertCommand(string id, int blockId, DateTime shift_Date, int Requirement_Id, int iteration_Number)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}','{4}')",
                 id, blockId, shift_Date.ToString("yyyy-MM-dd"), Requirement_Id, iteration_Number);
            String prefix = "INSERT INTO kkl_Day_In_Shift " + "([Id], [Block_Id], [Shift_Date], [Requirement_Id],[Iteration_Number])";
            command = prefix + sb.ToString();

            return command;
        }

        public List<int> Read_BlocksOfArea(int areaId)
        {
            SqlConnection con = null;
            List<int> blockIds = new List<int>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @"select Block_Id
                                     from kkl_Block
                                     where Area_Id=" + areaId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    blockIds.Add(Convert.ToInt32(dr["Block_Id"]));
                }
                return blockIds;
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


        public List<Block> Read_Blocks_With_Area_Id(int Area_Id)
        {
            SqlConnection con = null;
            List<Block> blocklist = new List<Block>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @"select *
                                     from kkl_Block
                                     where Area_Id=" + Area_Id;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Block b = new Block();
                    b.Block_Id = Convert.ToInt32(dr["Block_Id"]);
                    b.Area_Id = Area_Id;
                    b.Block_Name = (string)dr["Block_Name"];
                    blocklist.Add(b);
                }
                return blocklist;
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
        public List<Area> GetAreasOfRegion(int region_id)
        {
            SqlConnection con = null;
            List<Area> area_list = new List<Area>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @"select *
                                     from kkl_Area
                                     where Region_Id=" + region_id;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Area a = new Area();
                    a.Area_Id = Convert.ToInt32(dr["Area_Id"]);
                    a.Area_Name = (string)dr["Area_Name"];
                    a.Manager_Id = (string)dr["Manager_Id"];
                    a.Patrol_Id = (string)dr["Patrolman_Id"];
                    a.Forester_Id = (string)dr["Forester_Id"];

                    area_list.Add(a);
                }
                return area_list;
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

        public Area Read_Area_By_Emp_Id(string id)
        {
            SqlConnection con = null;
            Area area = new Area();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @"SELECT a.*
                                     FROM kkl_Worker_In_Area wia inner join kkl_Area a on wia.Area_Id=a.Area_Id
                                     where id='" + id + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    area.Region_Id = Convert.ToInt32(dr["Region_Id"]);
                    area.Area_Id = Convert.ToInt32(dr["Area_Id"]);
                    area.Area_Name = (string)dr["Area_Name"];
                    area.Patrol_Id = (string)dr["Patrolman_Id"];
                    area.Manager_Id = (string)dr["Manager_Id"];
                    area.Forester_Id = (string)dr["Forester_Id"];
                }
                return area;
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

        public List<DutyInShift> ReadDutiesInShift(string date, int areaId)
        {
            SqlConnection con = null;
            List<DutyInShift> dutyInShifts = new List<DutyInShift>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @" select e.First_Name,e.Last_Name,sr.Requirement_Name,b.Block_Name,e.Phone_Number
                                        from kkl_Day_In_Shift dis inner join
                                        kkl_Employee e on e.Id=dis.Id inner join
                                        kkl_Block b on dis.Block_Id=b.Block_Id inner join
                                        kkl_Shift_Requirements sr on dis.Requirement_Id=sr.Requirement_Id inner join
                                        kkl_Area a on b.Area_Id=a.Area_Id
                                        where Shift_Date='" + date + "' and a.Area_Id=" + areaId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    DutyInShift dis = new DutyInShift();
                    dis.First_Name = (string)dr["First_Name"];
                    dis.Last_Name = (string)dr["Last_Name"];
                    dis.Requirement_Name = (string)dr["Requirement_Name"];
                    dis.Block_Name = (string)dr["Block_Name"];
                    dis.Phone = (string)dr["Phone_Number"];
                    dutyInShifts.Add(dis);
                }
                return dutyInShifts;
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

        public int DeleteExistAssign(int areaId, string startDate, string endDate)
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

            String cStr = @"DELETE FROM kkl_Day_In_Shift
                            WHERE Shift_Date between '" + startDate + "' and '" + endDate + "' and Block_Id in (select Block_Id from kkl_Block where Area_Id =" + areaId + ")";

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

        public int DeleteAllAssignsExceptBest(int areaId, string startDate, string endDate, int iterationNumber)
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

            String cStr = @"DELETE FROM kkl_Day_In_Shift
                            WHERE Iteration_Number <> " + iterationNumber + " and Shift_Date between '" + startDate + "' and '" + endDate + "' and Block_Id in (select Block_Id from kkl_Block where Area_Id =" + areaId + ")";

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

        public Worker_In_Area getAreaAndJob(string id)
        {
            SqlConnection con = null;
            Worker_In_Area wia = new Worker_In_Area();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select * from kkl_Worker_In_Area where id='{id}'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    wia.Id = (string)dr["Id"];
                    wia.Area_Id = Convert.ToInt32(dr["Area_Id"]);
                    wia.Job_Id = Convert.ToInt32(dr["Job_Id"]);
                    wia.Job_Start_Date = Convert.ToDateTime(dr["Job_Start_Date"]);
                }
                return wia;
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
        public int Edit_Employee(Employee emp)
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

            String cStr = @"UPDATE kkl_Employee
 SET First_Name ='" + emp.First_Name + "', Last_Name = '" + emp.Last_Name + "' , Gender = '" + emp.Gender + "'  , Birth_Date ='" + emp.Birth_Date.ToString("yyyy-MM-dd") + "' , Phone_Number = '" + emp.Phone_Number + "' , Mail = '" + emp.Mail + "',Password = '" + emp.Password + "' WHERE Id = '" + emp.Id + "'";      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (SqlException ex)
            {

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
        public Worker_In_Region getRegionAndJob(string id)
        {
            SqlConnection con = null;
            Worker_In_Region wir = new Worker_In_Region();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select * from kkl_Worker_In_Region where id='{id}'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    wir.Id = (string)dr["Id"];
                    wir.Region_Id = Convert.ToInt32(dr["Region_Id"]);
                    wir.Job_Id = Convert.ToInt32(dr["Job_Id"]);
                    wir.Job_Start_Date = Convert.ToDateTime(dr["Job_Start_Date"]);
                }
                return wir;
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

        public List<double> GetScoreHelper(int area_Id, int month, int iterationNumber)
        {

            SqlConnection con = null;
            SqlDataReader dr = null;
            List<double> ans = new List<double>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                }
                catch (Exception)
                {

                    throw;
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "sp_Score_Helper";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Area_Id", area_Id);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Iteration_Number", iterationNumber);

                // get a reader
                try
                {
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                }
                catch (Exception)
                {

                    throw;
                }


                while (dr.Read())
                {
                    // Read till the end of the data into a row
                    ans.Add(Convert.ToDouble(dr["satisfaction"]));
                    ans.Add(Convert.ToDouble(dr["fairness"]));
                }
                return ans;
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

        public int insertSubstitutionRequest(Substitution_Request sReq)
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

            String cStr = BuildInsertCommand(sReq);      // helper method to build the insert string

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

        private String BuildInsertCommand(Substitution_Request sReq)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}')",
                 sReq.Id_From, sReq.Id_To, sReq.Request_date.ToString("yyyy-MM-dd"), sReq.Status);
            String prefix = "INSERT INTO kkl_Substitution_Request " + "([Id_From], [Id_To], [Request_Date], [Status])";
            command = prefix + sb.ToString();

            return command;
        }

        public List<Substitution_Request> Read_Area_Substitution_Request(int areaId)
        {
            SqlConnection con = null;
            List<Substitution_Request> sReqList = new List<Substitution_Request>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @"select sr.Request_Number,e.First_Name + ' '+ e.Last_Name as 'Name_From',e.Id as'Id_From',
                                    e1.First_Name + ' '+ e1.Last_Name as 'Name_To',e1.Id as'Id_To',sr.Request_Date,srs.Requirement_Name
                                    from kkl_Substitution_Request sr
                                    left join kkl_Employee e on e.Id=sr.Id_From
                                    left join kkl_Employee e1 on e1.Id=sr.Id_To
                                    left join kkl_Day_In_Shift din on din.Id=e.Id
                                    inner join kkl_Shift_Requirements srs on din.Requirement_Id=srs.Requirement_Id
                                    where din.Shift_Date=sr.Request_Date and sr.Status=0
                                    and din.Block_Id in (select Block_Id from kkl_Block where Area_Id = "+areaId+") and sr.Request_Date >= '"+DateTime.Now.ToString("yyyy-MM-dd")+ @"'
                                    order by sr.Request_Date";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Substitution_Request sReq = new Substitution_Request();
                    sReq.Request_Number= Convert.ToInt32(dr["Request_Number"]);
                    sReq.Id_From = (string)(dr["Id_From"]);
                    sReq.Name_From = (string)(dr["Name_From"]);
                    sReq.Id_To = (string)(dr["Id_To"]);
                    sReq.Name_To = (string)(dr["Name_To"]);
                    sReq.Request_date = Convert.ToDateTime(dr["Request_date"]);
                    sReq.Requirement_Name = (string)(dr["Requirement_Name"]);
                    sReqList.Add(sReq);
                }
                return sReqList;
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
        
        public List<Substitution_Request> Read_Region_Substitution_Request(int regionId)
        {
            SqlConnection con = null;
            List<Substitution_Request> sReqList = new List<Substitution_Request>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = @"select sr.Request_Number,e.First_Name + ' '+ e.Last_Name as 'Name_From',e.Id as'Id_From',
                                    e1.First_Name + ' '+ e1.Last_Name as 'Name_To',e1.Id as'Id_To',sr.Request_Date,srs.Requirement_Name
                                    from kkl_Substitution_Request sr
                                    left join kkl_Employee e on e.Id=sr.Id_From
                                    left join kkl_Employee e1 on e1.Id=sr.Id_To
                                    left join kkl_Day_In_Shift din on din.Id=e.Id
                                    inner join kkl_Shift_Requirements srs on din.Requirement_Id=srs.Requirement_Id
                                    where din.Shift_Date=sr.Request_Date and sr.Status=0
                                    and din.Block_Id in (select Block_Id  from kkl_Block where Area_Id in (select Area_Id from kkl_Area where Region_Id="+regionId+ ") and sr.Request_Date >= '" + DateTime.Now.ToString("yyyy-MM-dd")+"'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Substitution_Request sReq = new Substitution_Request();
                    sReq.Request_Number= Convert.ToInt32(dr["Request_Number"]);
                    sReq.Id_From = (string)(dr["Id_From"]);
                    sReq.Name_From = (string)(dr["Name_From"]);
                    sReq.Id_To = (string)(dr["Id_To"]);
                    sReq.Name_To = (string)(dr["Name_To"]);
                    sReq.Request_date = Convert.ToDateTime(dr["Request_date"]);
                    sReq.Requirement_Name = (string)(dr["Requirement_Name"]);
                    sReqList.Add(sReq);
                }
                return sReqList;
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

        public int UpdateStatus (int request_Number,int status)
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

            String cStr = @"UPDATE kkl_Substitution_Request
                            SET Status = "+status + @"
                            WHERE Request_Number = "+request_Number;

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
        
        public int UpdateSubstitution(string Id_From,string Id_To,DateTime Request_date)
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

            String cStr = @"UPDATE kkl_Day_In_Shift
                           SET id= "+Id_To +@"
                           WHERE Shift_Date='"+Request_date.ToString("yyyy-MM-dd") + "' and id= '" + Id_From +"'";

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

        public int Insert_Message (Message msg)
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

            String cStr = BuildInsertCommand(msg);      // helper method to build the insert string

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

        private String BuildInsertCommand(Message msg)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}')",
                 msg.Creator_Id, msg.Creation_Date.ToString("yyyy-MM-dd"), msg.Title.Replace("'","''"), msg.Content.Replace("'", "''"));
            String prefix = "INSERT INTO kkl_Message " + "([Creator_Id], [Creation_Date], [Title], [Content])";
            command = prefix + sb.ToString();

            return command;
        }

        public List<Message> Read_Messages()
        {
            SqlConnection con = null;
            List<Message> messages = new List<Message>();
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select * from kkl_Message order by Creation_Date desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Message msg = new Message();
                    msg.Message_Number = Convert.ToInt32(dr["Message_Number"]); 
                    msg.Creator_Id= (string)dr["Creator_Id"];
                    msg.Creation_Date= Convert.ToDateTime(dr["Creation_Date"]);
                    msg.Title= (string)dr["Title"];
                    msg.Content = (string)dr["Content"];
                    messages.Add(msg);
                }
                return messages;
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

    }


}