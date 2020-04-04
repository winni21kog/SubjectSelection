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
        private SubjectSelectionEntities db = new SubjectSelectionEntities();

        /// <summary>
        /// 取得所有學生資料
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
        /// 取得特定學生資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentViewModel GetStudents(int id)
        {
            //var result = db.Student.Where(x => x.Id == id).FirstOrDefault();
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

        public void AddStudent(StudentViewModel student)
        {
            var result = new Student
            {
                StudentId = student.StudentId,
                Name = student.Name,
                Birthday = Convert.ToDateTime(student.BirthdayStr),
                Email = student.Email
            };

            db.Student.Add(result);
            db.SaveChanges();
        }

        public void UpdataStudent(Student student)
        {
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteStudent(int id)
        {
            Student student = db.Student.Find(id);
            db.Student.Remove(student);
            db.SaveChanges();
        }
    }
}