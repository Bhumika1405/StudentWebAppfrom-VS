using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using StudentWebApp.Models;

namespace StudentWebApp.Controllers
{
    public class StudentController : Controller
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
        SqlCommand SQLCmd;
        SqlDataAdapter Adp;
        DataTable DtMstr;

        [HttpGet]
        public ActionResult AllStudentList()
        {
            List<StudentModel> studentlist = null;
            //SQLCmd = new SqlCommand("sp_StudentGET", Conn);
            //SQLCmd.CommandType = CommandType.StoredProcedure;
            Adp = new SqlDataAdapter("sp_StudentGET", Conn);
            Adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            Adp.SelectCommand.Parameters.AddWithValue("@Flag", "AllStudentGET");
            DtMstr = new DataTable();
            Adp.Fill(DtMstr);
            studentlist = new List<StudentModel>();
            for(int i=0;i< DtMstr.Rows.Count; i++)
            {
                StudentModel Objstudent = new StudentModel();
                Objstudent.ID = Convert.ToInt32(DtMstr.Rows[i]["ID"].ToString());
                Objstudent.FirstName = Convert.ToString(DtMstr.Rows[i]["FirstName"]);
                Objstudent.LastName = Convert.ToString(DtMstr.Rows[i]["LastName"]);
                Objstudent.EnrollDate = Convert.ToDateTime(DtMstr.Rows[i]["EnrollDate"]);
                Objstudent.EmailAddress = Convert.ToString(DtMstr.Rows[i]["EmailAddress"]);
                studentlist.Add(Objstudent);
            }
            return View(studentlist);
           // return Json(new { data = studentlist }, JsonRequestBehavior.AllowGet);
           
        }

        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public string AddStudent(StudentModel studentData)
        {
            string result = " ";
            try
            {
                SQLCmd = new SqlCommand("sp_StudentAddEdit",Conn);
                SQLCmd.CommandType = CommandType.StoredProcedure;
               // SQLCmd.Transaction = SQLCmd.Connection.BeginTransaction();
                //studentData.ID = 0;
                //SQLCmd.Parameters.AddWithValue("@ID", studentData.ID);
                SQLCmd.Parameters.Add("@ID", SqlDbType.Int).Value = studentData.ID;
                SQLCmd.Parameters.Add("@FirstName",SqlDbType.NVarChar,50).Value= studentData.FirstName;
                SQLCmd.Parameters.Add("@LastName",SqlDbType.NVarChar,50).Value = studentData.LastName;
                SQLCmd.Parameters.Add("@EnrollDate", SqlDbType.Date).Value = studentData.EnrollDate;
                SQLCmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = studentData.EmailAddress;
                SQLCmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50).Value = string.Empty;
                SQLCmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                Conn.Open();
                SQLCmd.ExecuteNonQuery();
                result = Convert.ToString(SQLCmd.Parameters["@Message"].Value);
                Conn.Close();
                return result;
            }
            catch (Exception)
            {

                return result = " ";
            }
            finally { 
                Conn.Close();
            }   
        }
        [HttpGet]
        public ActionResult ViewStudentdata(int id)
        {
            try
            {
                StudentModel objStudent = new StudentModel();
                if(id>0)
                {
                    Adp = new SqlDataAdapter("sp_StudentGET", Conn);
                    Adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Adp.SelectCommand.Parameters.AddWithValue("@Flag", "StudentGETByID");
                    Adp.SelectCommand.Parameters.AddWithValue("@ID", id);
                    DtMstr = new DataTable();
                    Adp.Fill(DtMstr);
                    if(DtMstr.Rows.Count>0)
                    {
                        objStudent.ID = id;
                        objStudent.FirstName = Convert.ToString(DtMstr.Rows[0]["FirstName"]);
                        objStudent.LastName = Convert.ToString(DtMstr.Rows[0]["LastName"]);
                        objStudent.EnrollDate = (Convert.ToDateTime(DtMstr.Rows[0]["EnrollDate"]));
                        objStudent.EmailAddress = Convert.ToString(DtMstr.Rows[0]["EmailAddress"]);
                    }
                    return View(objStudent);
                }
                else
                {
                    return RedirectToAction("AllStudentList");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            try
            {
                StudentModel ObjStudent = new StudentModel();
                if (id>0)
                {
                    Adp = new SqlDataAdapter("sp_StudentGET", Conn);
                    Adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Adp.SelectCommand.Parameters.AddWithValue("@Flag", "StudentGETByID");
                    Adp.SelectCommand.Parameters.AddWithValue("@ID", id);
                    DtMstr = new DataTable();
                    Adp.Fill(DtMstr);
                    if(DtMstr.Rows.Count>0) 
                    {
                        
                        ObjStudent.ID = id;
                        ObjStudent.FirstName = Convert.ToString(DtMstr.Rows[0]["FirstName"]);
                        ObjStudent.LastName = Convert.ToString(DtMstr.Rows[0]["LastName"]);
                        ObjStudent.EnrollDate = Convert.ToDateTime(string.Format("{0:dd-MMM-yyyy}", DtMstr.Rows[0]["EnrollDate"]));
                        ObjStudent.EmailAddress = Convert.ToString(DtMstr.Rows[0]["EmailAddress"]);

                    }
                    return View(ObjStudent);
                }
                else
                {
                    return RedirectToAction("AllStudentList");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult EditStudent(int id,StudentModel studentdata)
        {
            string result = " ";
            try
            {
                SQLCmd = new SqlCommand();
                SQLCmd.Connection = Conn;
                SQLCmd.CommandText = "sp_StudentAddEdit";
                SQLCmd.CommandType = CommandType.StoredProcedure;
                //SQLCmd.Transaction = SQLCmd.Connection.BeginTransaction();
                SQLCmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                SQLCmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = studentdata.FirstName;
                SQLCmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = studentdata.LastName;
                SQLCmd.Parameters.Add("@EnrollDate", SqlDbType.Date).Value = studentdata.EnrollDate;
                SQLCmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar,50).Value = studentdata.EmailAddress;
                SQLCmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50).Value = string.Empty;
                SQLCmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                Conn.Open();
                SQLCmd.ExecuteNonQuery();
                result = Convert.ToString(SQLCmd.Parameters["@Message"].Value);
                Conn.Close();
                //if (result.Equals("TRUE", StringComparison.CurrentCultureIgnoreCase))
                //{
                //    SQLCmd.Transaction.Commit();
                //}
                //else
                //{
                //    SQLCmd.Transaction.Rollback();
                //}
                return RedirectToAction("AllStudentList");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (SQLCmd.Connection != null) SQLCmd.Dispose();
            }
        }

        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            try
            {
                if(id>0)
                {
                    StudentModel ObjStudent = new StudentModel();
                    Adp = new SqlDataAdapter("sp_StudentGET", Conn);
                    Adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Adp.SelectCommand.Parameters.AddWithValue("@Flag", "StudentGETByID");
                    Adp.SelectCommand.Parameters.AddWithValue("@ID", id);
                    DtMstr = new DataTable();
                    Adp.Fill(DtMstr);
                    if (DtMstr.Rows.Count > 0)
                    {
                        ObjStudent.ID = id;
                        ObjStudent.FirstName = Convert.ToString(DtMstr.Rows[0]["FirstName"]);
                        ObjStudent.LastName = Convert.ToString(DtMstr.Rows[0]["LastName"]);
                        ObjStudent.EnrollDate = Convert.ToDateTime(string.Format("{0:dd-MMM-yyyy}", DtMstr.Rows[0]["EnrollDate"]));
                        ObjStudent.EmailAddress = Convert.ToString(DtMstr.Rows[0]["EmailAddress"]);
                    }
                    return View(ObjStudent);
                }
                else
                {
                    return RedirectToAction("AllStudentList");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPost]
        public ActionResult DeleteStudent(StudentModel studentdata)
        {
            string result = " ";
            try
            {
                if (studentdata != null)
                {
                    SQLCmd = new SqlCommand("sp_StudentDelete", Conn);
                    SQLCmd.CommandType = CommandType.StoredProcedure;
                    SQLCmd.Parameters.Add("@ID",SqlDbType.Int).Value = studentdata.ID;
                    SQLCmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50).Value = string.Empty;
                    SQLCmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    Conn.Open();
                    SQLCmd.ExecuteNonQuery();
                    result = Convert.ToString(SQLCmd.Parameters["@Message"].Value);
                    Conn.Close();
                    return RedirectToAction("AllStudentList");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}