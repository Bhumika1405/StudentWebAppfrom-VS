using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentWebApp.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudentWebApp.Controllers
{
    public class CourseController : Controller
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        SqlCommand SQLCmd;
        SqlDataAdapter Adp;
        DataTable DtMstr;

        [HttpGet]
        public ActionResult AllCourseGET()
        {
            Adp = new SqlDataAdapter("sp_CourseGET", Conn);
            Adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            Adp.SelectCommand.Parameters.AddWithValue("@Flag", "AllCourseGET");
            DtMstr = new DataTable();
            Adp.Fill(DtMstr);
            List<CourseClass> Courselst = new List<CourseClass>();
            for(int i= 0; i < DtMstr.Rows.Count;i++)
            {
                CourseClass ObjCourse = new CourseClass();
                ObjCourse.ID = Convert.ToInt32(DtMstr.Rows[i]["ID"]);
                ObjCourse.CourseName = Convert.ToString(DtMstr.Rows[i]["CourseName"]);
                ObjCourse.studentName = Convert.ToString(DtMstr.Rows[i]["StudentName"]);
                ObjCourse.CourseFee = Convert.ToDecimal(DtMstr.Rows[i]["CourseFee"]);
                Courselst.Add(ObjCourse);
            }
            return View(Courselst);
        }

        [HttpGet]
        public ActionResult ADDCourse()
        {
            List<StudentModel> studentLst = new List<StudentModel>();
            SQLCmd = new SqlCommand("sp_CourseGET", Conn);
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.Parameters.AddWithValue("@Flag", "AllStudentGET");
            Adp = new SqlDataAdapter(SQLCmd);
            DtMstr = new DataTable();
            Adp.Fill(DtMstr);
            foreach(DataRow dr in DtMstr.Rows)
            {
                studentLst.Add(
                    new StudentModel
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        FirstName = Convert.ToString(dr["FirstName"])
                    });
            }
            ViewBag.studentdata = studentLst;
            return View();
        }

        [HttpPost]
        public ActionResult ADDCourse(CourseModel coursedata)
        {
             string result = " ";
            try
            {
                if(coursedata!=null)
                {
                    SQLCmd = new SqlCommand("sp_CourseAddEdit", Conn);
                    SQLCmd.CommandType = CommandType.StoredProcedure;
                    SQLCmd.Parameters.Add("@ID", SqlDbType.Int).Value = coursedata.ID;
                    SQLCmd.Parameters.Add("@CourseName", SqlDbType.NVarChar, 50).Value = coursedata.CourseName;
                    SQLCmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = coursedata.StudentID;
                    SQLCmd.Parameters.Add("@CourseFee", SqlDbType.Decimal).Value = coursedata.CourseFee;
                    SQLCmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50).Value = string.Empty;
                    SQLCmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    Conn.Open();
                    SQLCmd.ExecuteNonQuery();
                    result = Convert.ToString(SQLCmd.Parameters["@Message"].Value);
                    Conn.Close();
                    return RedirectToAction("AllCourseGET");
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
        [HttpGet]
        public ActionResult ViewCourseDetails(int id)
        {
            try
            {
                Adp = new SqlDataAdapter("sp_CourseGET",Conn);
                Adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                Adp.SelectCommand.Parameters.AddWithValue("@Flag", "GETCourseBYID");
                Adp.SelectCommand.Parameters.AddWithValue("@ID",id);
                DtMstr = new DataTable();
                Adp.Fill(DtMstr);
                CourseClass ObjCourse = new CourseClass();
                if(DtMstr.Rows.Count>0)
                {
                    ObjCourse.ID = Convert.ToInt32(DtMstr.Rows[0]["ID"]);
                    ObjCourse.CourseName = Convert.ToString(DtMstr.Rows[0]["CourseName"]);
                    ObjCourse.studentName = Convert.ToString(DtMstr.Rows[0]["studentName"]);
                    ObjCourse.CourseFee = Convert.ToDecimal(DtMstr.Rows[0]["CourseFee"]);
                }
                return View(ObjCourse);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            #region Bind Drop Down
            List<StudentModel> studentLst = new List<StudentModel>();
            SQLCmd = new SqlCommand("sp_CourseGET", Conn);
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.Parameters.AddWithValue("@Flag", "AllStudentGET");
            Adp = new SqlDataAdapter(SQLCmd);
            DtMstr = new DataTable();
            Adp.Fill(DtMstr);
            foreach (DataRow dr in DtMstr.Rows)
            {
                studentLst.Add(
                    new StudentModel
                    {
                        ID= Convert.ToInt32(dr["ID"]),
                        FirstName = Convert.ToString(dr["FirstName"])
                    });
            }
            ViewBag.studentdata = studentLst;
            #endregion
            CourseClass objcourse = new CourseClass();
            Adp = new SqlDataAdapter("sp_CourseGET", Conn);
            Adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            Adp.SelectCommand.Parameters.AddWithValue("@Flag", "GETCourseBYID");
            Adp.SelectCommand.Parameters.AddWithValue("@ID", id);
            DtMstr = new DataTable();
            Adp.Fill(DtMstr);
            if(DtMstr.Rows.Count>0)
            {
                objcourse.ID = id;
                objcourse.CourseName = Convert.ToString(DtMstr.Rows[0]["CourseName"]);
                objcourse.StudentID = Convert.ToInt32(DtMstr.Rows[0]["StudentID"]);
                objcourse.CourseFee = Convert.ToDecimal(DtMstr.Rows[0]["CourseFee"]);
            }
            return View(objcourse);
        }

        [HttpPost]
        public ActionResult EditCourse(CourseClass objcourse)
        {
            try
            {
                SQLCmd = new SqlCommand("sp_CourseAddEdit",Conn);
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.Parameters.Add("@ID", SqlDbType.Int).Value = objcourse.ID;
                SQLCmd.Parameters.Add("@CourseName", SqlDbType.NVarChar, 50).Value = objcourse.CourseName;
                SQLCmd.Parameters.Add("@StudentID",SqlDbType.Int).Value = objcourse.StudentID;
                SQLCmd.Parameters.Add("@CourseFee",SqlDbType.NVarChar,50).Value = objcourse.CourseFee;
                SQLCmd.Parameters.Add("@Message",SqlDbType.NVarChar,50).Value = string.Empty;
                SQLCmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                Conn.Open();
                SQLCmd.ExecuteNonQuery();
                Conn.Close();
                return RedirectToAction("AllCourseGET");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            try
            {
                Adp = new SqlDataAdapter("sp_CourseGET", Conn);
                Adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                Adp.SelectCommand.Parameters.AddWithValue("@Flag", "GETCourseBYID");
                Adp.SelectCommand.Parameters.AddWithValue("@ID", id);
                DtMstr = new DataTable();
                Adp.Fill(DtMstr);
                CourseClass ObjCourse = new CourseClass();
                if(DtMstr.Rows.Count>0)
                {
                    ObjCourse.ID = id;
                    ObjCourse.CourseName = Convert.ToString(DtMstr.Rows[0]["CourseName"]);
                    ObjCourse.studentName = Convert.ToString(DtMstr.Rows[0]["StudentName"]);
                    ObjCourse.CourseFee = Convert.ToDecimal(DtMstr.Rows[0]["CourseFee"]);
                }
                return View(ObjCourse);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult DeleteCourse(CourseClass coursedata)
        {
            SQLCmd = new SqlCommand("sp_CourseDelete", Conn);
            SQLCmd.CommandType = CommandType.StoredProcedure;
            SQLCmd.Parameters.Add("@ID",SqlDbType.Int).Value = coursedata.ID;
            SQLCmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50).Value = string.Empty;
            Conn.Open();
            SQLCmd.ExecuteNonQuery();
            return RedirectToAction("AllCourseGET");
        }
    }
}