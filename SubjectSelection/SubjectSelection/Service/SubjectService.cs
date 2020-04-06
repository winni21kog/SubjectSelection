using log4net;
using SubjectSelection.Models;
using SubjectSelection.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SubjectSelection.Service
{
    public class SubjectService
    {
        private SubjectSelectionEntities1 db = new SubjectSelectionEntities1();
        static ILog logger = LogManager.GetLogger("Web");

        /// <summary>
        /// 取得所有課程
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubjectViewModel> GetSubjects()
        {
            var result = db.Subject.AsEnumerable().Select(s => new SubjectViewModel
            {
                Id = s.Id,
                SubjectId = s.SubjectId,
                Name = s.Name,
                Credit = s.Credit,
                Location = s.Location,
                teacherName = s.teacherName
            });

            return result;
        }

        /// <summary>
        /// 取得特定課程
        /// </summary>
        /// <param name="id">課程id</param>
        /// <returns></returns>
        public SubjectViewModel GetSubject(int id)
        {
            var result = db.Subject.AsEnumerable().Where(x => x.Id == id).Select(s => new SubjectViewModel
            {
                Id = s.Id,
                SubjectId = s.SubjectId,
                Name = s.Name,
                Credit = s.Credit,
                Location = s.Location,
                teacherName = s.teacherName
            }).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// 新增課程
        /// </summary>
        /// <param name="subject">課程資料</param>
        public void AddSubject(SubjectViewModel subject)
        {
            var result = new Subject
            {
                SubjectId = GetSubjectId(),
                Name = subject.Name,
                Credit = subject.Credit,
                Location = subject.Location,
                teacherName = subject.teacherName
            };

            db.Subject.Add(result);
            db.SaveChanges();
            logger.Info($"新增課程 SudjectId:{result.SubjectId},Name:{result.Name},Credit:{result.Credit},Location:{result.Location},teacherName:{result.teacherName}");
        }

        /// <summary>
        /// 取得課程流水號
        /// </summary>
        /// <returns></returns>
        private string GetSubjectId()
        {
            var subjectId = string.Empty;

            if (db.Subject.Select(s => s.SubjectId).Count() != 0)
            {
                var oId = db.Subject.OrderByDescending(s => s.SubjectId).Select(s => s.SubjectId).FirstOrDefault();
                var nId = Convert.ToInt32(oId.Substring(1)) + 1;
                subjectId = "C" + nId.ToString().PadLeft(3, '0');
            }
            else
            {
                subjectId = "C001";
            }

            return subjectId;
        }

        /// <summary>
        /// 更新課程
        /// </summary>
        /// <param name="student">課程資料</param>
        public void UpdataSubject(SubjectViewModel subject)
        {
            // 取得原始資料
            var originSId = db.Subject.Where(s => s.Id == subject.Id).Select(s => s.SubjectId).FirstOrDefault();
            //更新Model
            var result = new Subject
            {
                Id = subject.Id,
                SubjectId = originSId,
                Name = subject.Name,
                Credit = subject.Credit,
                Location = subject.Location,
                teacherName = subject.teacherName
            };

            db.Entry(result).State = EntityState.Modified;
            db.SaveChanges();
            logger.Info($"修改課程資料 Id:{result.Id} SudjectId:{result.SubjectId},Name:{result.Name},Credit:{result.Credit},Location:{result.Location},teacherName:{result.teacherName}");
        }

        /// <summary>
        /// 檢查課程有無學生選擇
        /// </summary>
        /// <param name="id">課程Id</param>
        /// <returns></returns>
        public bool CheckSelection(int id)
        {
            return db.Selection.Where(s => s.SubjectId == id).Any();
        }

        /// <summary>
        /// 刪除課程
        /// </summary>
        /// <param name="id">課程id</param>
        public void DeleteSubject(int id)
        {
            Subject subject = db.Subject.Find(id);
            if (subject != null)
            {
                db.Subject.Remove(subject);
                db.SaveChanges();
                logger.Info($"刪除課程 Id:{subject.Id} SudjectId:{subject.SubjectId},Name:{subject.Name},Credit:{subject.Credit},Location:{subject.Location},teacherName:{subject.teacherName}");
            }
            else
            {
                logger.Error($"找不到課程 Id:{subject.Id}");
            }
        }
    }

}