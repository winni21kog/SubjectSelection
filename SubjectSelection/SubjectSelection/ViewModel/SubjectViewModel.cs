using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SubjectSelection.ViewModel
{
    public class SubjectViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 課號
        /// </summary>
        public string SubjectId { get; set; }
        /// <summary>
        /// 課名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 學分
        /// </summary>
        public int Credit { get; set; }
        /// <summary>
        /// 上課地點
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 講師名字
        /// </summary>
        public string teacherName { get; set; }
    }
}