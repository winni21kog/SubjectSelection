using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SubjectSelection.ViewModel
{
    /// <summary>
    /// 學生ViewModel
    /// </summary>
    public class StudentViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 學號
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 生日字串
        /// </summary>
        public string BirthdayStr { get; set; }
        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }
    }
}