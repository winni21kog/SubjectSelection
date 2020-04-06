using SubjectSelection.Models;
using System.Collections.Generic;

namespace SubjectSelection.ViewModel
{
    public class SubjectSelectionViewModel
    {
        /// <summary>
        /// 學生Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 學號流水號
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 學生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 所選課程Id
        /// </summary>
        public IEnumerable<int> SubjectIds { get; set; }
        /// <summary>
        /// 所選課程名稱
        /// </summary>
        public string SubjectNames { get; set; }

        /// <summary>
        /// 學生
        /// </summary>
        public List<StudentViewModel> Students { get; set; }
        /// <summary>
        /// 課程
        /// </summary>
        public List<SubjectViewModel> Subjects { get; set; }
    }
}