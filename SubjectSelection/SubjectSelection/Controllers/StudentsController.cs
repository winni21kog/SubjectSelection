using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using log4net;
using SubjectSelection.Models;
using SubjectSelection.Service;
using SubjectSelection.ViewModel;

namespace SubjectSelection.Controllers
{
    public class StudentsController : Controller
    {
        private StudentService studentService = new StudentService();
        static ILog logger = LogManager.GetLogger("Web");

        public ActionResult Index()
        {
            var student = new StudentViewModel();
            
            return View(student);
        }

        /// <summary>
        /// 顯示所有學生清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetStudentList()
        {
            try
            {
                var students = studentService.GetStudents();

                return Json(new ResponseData()
                {
                    Status = true,
                    Data = students
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                logger.Error($"取得所有學生失敗 Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message=ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
            
        }

        /// <summary>
        /// 顯示特定學生資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetStudent(int id)
        {
            try
            {
                var student = studentService.GetStudents(id);

                return Json(new ResponseData()
                {
                    Status = true,
                    Data = student
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                logger.Error($"取得Id:{id}的學生失敗 Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
            
        }

        /// <summary>
        /// 新增學生資料
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(StudentViewModel student)
        {
            try
            {
                studentService.AddStudent(student);

                var students = studentService.GetStudents();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = students
                });
            }
            catch(Exception ex)
            {
                logger.Error($"新增學生失敗 StudentId:{student.StudentId},Name:{student.Name},Birthday:{student.BirthdayStr} Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// 修改學生資料
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(StudentViewModel student)
        {
            try
            {
                studentService.UpdataStudent(student);

                var students = studentService.GetStudents();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = students
                });
            }
            catch(Exception ex)
            {
                logger.Error($"修改學生失敗 Id:{student.Id} StudentId:{student.StudentId},Name:{student.Name},Birthday:{student.BirthdayStr} Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
            
        }

        /// <summary>
        /// 刪除學生資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                studentService.DeleteStudent(id);

                var students = studentService.GetStudents();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = students
                });
            }
            catch(Exception ex)
            {
                logger.Error($"刪除學生失敗 Id:{id} Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
            
        }
    }
}
