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
    public class SubjectController : Controller
    {
        private SubjectService subjectService = new SubjectService();
        static ILog logger = LogManager.GetLogger("Web");

        public ActionResult Index()
        {
            var subject = new SubjectViewModel();

            return View(subject);
        }

        /// <summary>
        /// 顯示所有課程清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSubjectList()
        {
            try
            {
                var subjects = subjectService.GetSubjects();

                return Json(new ResponseData()
                {
                    Status = true,
                    Data = subjects
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error($"取得所有課程失敗 Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 顯示特定課程資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSubject(int id)
        {
            try
            {
                var subject = subjectService.GetSubject(id);

                return Json(new ResponseData()
                {
                    Status = true,
                    Data = subject
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error($"取得Id:{id}的課程失敗 Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 新增課程資料
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(SubjectViewModel subject)
        {
            try
            {
                subjectService.AddSubject(subject);

                var subjects = subjectService.GetSubjects();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = subjects
                });
            }
            catch (Exception ex)
            {
                logger.Error($"新增課程失敗 SudjectId:{subject.SubjectId},Name:{subject.Name},Credit:{subject.Credit},Location:{subject.Location},teacherName:{subject.teacherName}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// 修改課程資料
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(SubjectViewModel subject)
        {
            try
            {
                subjectService.UpdataSubject(subject);

                var subjects = subjectService.GetSubjects();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = subjects
                });
            }
            catch (Exception ex)
            {
                logger.Error($"修改課程失敗 Id:{subject.Id} SudjectId:{subject.SubjectId},Name:{subject.Name},Credit:{subject.Credit},Location:{subject.Location},teacherName:{subject.teacherName}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                });
            }

        }

        /// <summary>
        /// 刪除課程資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                subjectService.DeleteSubject(id);

                var subjects = subjectService.GetSubjects();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = subjects
                });
            }
            catch (Exception ex)
            {
                logger.Error($"刪除課程失敗 Id:{id} Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                });
            }

        }
    }
}
