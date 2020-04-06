using log4net;
using SubjectSelection.Models;
using SubjectSelection.Service;
using SubjectSelection.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SubjectSelection.Controllers
{
    public class SubjectSelectionController : Controller
    {
        private SubjectSelectionService selectionService = new SubjectSelectionService();
        static ILog logger = LogManager.GetLogger("Web");

        public ActionResult Index()
        {
            var subject = new SubjectSelectionViewModel();

            return View(subject);
        }

        /// <summary>
        /// 顯示所有選課清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSelectionList()
        {
            try
            {
                var selections = selectionService.GetSelections();

                return Json(new ResponseData()
                {
                    Status = true,
                    Data = selections
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error($"取得所有選課資料失敗 Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 取得特定學生課程
        /// </summary>
        /// <param name="id">學生Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSelection(int id)
        {
            try
            {
                var selections = selectionService.GetSelectionFromStudent(id);

                return Json(new ResponseData()
                {
                    Status = true,
                    Data = selections
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error($"取得特定學生課程失敗 Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 新增選課資料
        /// </summary>
        /// <param name="sId">學生Id</param>
        /// <param name="subjects">選擇的課程Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(int sId, int[] subjects)
        {
            var success = selectionService.AddSelection(sId, subjects);

            if (success)
            {
                var selections = selectionService.GetSelections();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = selections
                });
            }
            else
            {
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = "新增選課失敗"
                });
            }
        }

        /// <summary>
        /// 修改選課資料
        /// </summary>
        /// <param name="sId">學生Id</param>
        /// <param name="subjects">選擇的課程Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int sId, int[] subjects)
        {
            var success = selectionService.UpdataSelection(sId, subjects);

            if (success)
            {
                var selections = selectionService.GetSelections();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = selections
                });
            }
            else
            {
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = "更新失敗!"
                });
            }
        }

        /// <summary>
        /// 刪除選課資料
        /// </summary>
        /// <param name="sId">學生Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int sId)
        {
            try
            {
                selectionService.DeleteSelection(sId);

                var selections = selectionService.GetSelections();
                return Json(new ResponseData()
                {
                    Status = true,
                    Data = selections
                });
            }
            catch (Exception ex)
            {
                logger.Error($"刪除選課失敗 學生Id:{sId} Exception: {ex.Message}");
                return Json(new ResponseData()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }
    }
}