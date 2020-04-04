using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SubjectSelection.Models;
using SubjectSelection.Service;
using SubjectSelection.ViewModel;

namespace SubjectSelection.Controllers
{
    public class StudentsController : Controller
    {
        private StudentService studentService = new StudentService();

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
            var students = studentService.GetStudents();

            return Json(new ResponseData()
            {
                Status = true,
                Data = students
            },JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 顯示特定學生資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetStudent(int id)
        {
            var student = studentService.GetStudents(id);

            return Json(new ResponseData()
            {
                Status = true,
                Data = student
            }, JsonRequestBehavior.AllowGet);
        }
        // GET: Students/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Student.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        /// <summary>
        /// 新增學生資料
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                studentService.AddStudent(student);

                var students = studentService.GetStudents();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = students
                });
            }

            return View(student);
        }

        //// GET: Students/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Student.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        // POST: Students/Edit/5

        /// <summary>
        /// 修改學生資料
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                studentService.UpdataStudent(student);

                var students = studentService.GetStudents();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = students
                });
            }
            return View(student);
        }

        /// <summary>
        /// 刪除學生資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            studentService.DeleteStudent(id);

            var students = studentService.GetStudents();
            return Json(new ResponseData()
            {
                Status = true,
                Data = students
            });
        }
    }
}
