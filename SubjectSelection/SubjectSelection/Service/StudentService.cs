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
    public class StudentService
    {
        private SubjectSelectionEntities1 db = new SubjectSelectionEntities1();
        static ILog logger = LogManager.GetLogger("Web");

        /// <summary>
        /// 取得所有學生
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StudentViewModel> GetStudents()
        {
            var result = db.Student.AsEnumerable().Select(s => new StudentViewModel
            {
                Id = s.Id,
                StudentId = s.StudentId,
                Name = s.Name,
                Birthday = s.Birthday,
                BirthdayStr = s.Birthday.ToString("yyyy-MM-dd"),
                Email = s.Email
            });

            return result;
        }

        /// <summary>
        /// 取得特定學生
        /// </summary>
        /// <param name="id">學生id</param>
        /// <returns></returns>
        public StudentViewModel GetStudents(int id)
        {
            var result = db.Student.AsEnumerable().Where(x => x.Id == id).Select(s => new StudentViewModel
            {
                Id = s.Id,
                StudentId = s.StudentId,
                Name = s.Name,
                Birthday = s.Birthday,
                BirthdayStr = s.Birthday.ToString("yyyy-MM-dd"),
                Email = s.Email
            }).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// 新增學生
        /// </summary>
        /// <param name="student">學生資料</param>
        public void AddStudent(StudentViewModel student)
        {
            var result = new Student
            {
                StudentId = GetStudentId(),
                Name = student.Name,
                Birthday = Convert.ToDateTime(student.BirthdayStr),
                Email = student.Email
            };

            db.Student.Add(result);
            db.SaveChanges();
            logger.Info($"新增學生 StudentId:{result.StudentId},Name:{result.Name},Birthday:{result.Birthday},Email:{result.Email}");
        }

        /// <summary>
        /// 取得學號流水號
        /// </summary>
        /// <returns></returns>
        private string GetStudentId()
        {
            var studentId = string.Empty;

            if (db.Student.Select(s => s.StudentId).Count() != 0)
            {
                var oId = db.Student.OrderByDescending(s => s.StudentId).Select(s => s.StudentId).FirstOrDefault();
                var nId = Convert.ToInt32(oId.Substring(1)) + 1;
                studentId = "S" + nId.ToString().PadLeft(4, '0');
            }
            else
            {
                studentId = "S0001";
            }

            return studentId;
        }

        /// <summary>
        /// 更新學生
        /// </summary>
        /// <param name="student">學生資料</param>
        public void UpdataStudent(StudentViewModel student)
        {
            // 取得原始資料
            var originSId = db.Student.Where(s => s.Id == student.Id).Select(s=>s.StudentId).FirstOrDefault();
            //更新Model
            var result = new Student
            {
                Id = student.Id,
                StudentId = originSId,
                Name = student.Name,
                Birthday = Convert.ToDateTime(student.BirthdayStr),
                Email = student.Email
            };

            db.Entry(result).State = EntityState.Modified;
            db.SaveChanges();
            logger.Info($"修改學生資料 Id:{result.Id} StudentId:{result.StudentId},Name:{result.Name},Birthday:{result.Birthday},Email:{result.Email}");
        }

        /// <summary>
        /// 檢查學生有無選擇課程
        /// </summary>
        /// <param name="id">學生Id</param>
        /// <returns></returns>
        public bool CheckSelection(int id)
        {
            return db.Selection.Where(s => s.StudentId == id).Any();
        }

        /// <summary>
        /// 刪除學生
        /// </summary>
        /// <param name="id">學生id</param>
        public void DeleteStudent(int id)
        {
            Student student = db.Student.Find(id);
            if (student != null)
            {
                db.Student.Remove(student);
                db.SaveChanges();
                logger.Info($"刪除學生 Id:{student.Id} StudentId:{student.StudentId},Name:{student.Name},Birthday:{student.Birthday},Email:{student.Email}");
            }
            else
            {
                logger.Error($"找不到學生 Id:{student.Id}");
            }

        }
    }
}