using Dapper;
using log4net;
using SubjectSelection.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SubjectSelection.Service
{
    public class SubjectSelectionService
    {
        private ConnectionService connectionService = new ConnectionService();
        static ILog logger = LogManager.GetLogger("Web");

        /// <summary>
        /// 取得所有選課
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubjectSelectionViewModel> GetSelections()
        {
            using (var connection = connectionService.CreateConnection())
            {
                connection.Open();
                var query = @";with SelectionCTE as (
                        select st.Id, st.StudentId, sb.Name
                        from Selection se
                        join Student st on st.Id = se.StudentId
                        join Subject sb on sb.Id = se.SubjectId)
                        select DISTINCT Id,StudentId,
                        (select Name+'、' from SelectionCTE s2
                        where s1.StudentId = s2.StudentId
                        for xml path('')
                        ) as SubjectNames
                        from SelectionCTE s1";

                var result = connection.Query<SubjectSelectionViewModel>(query);
                // 去除最後一個、
                foreach (var r in result)
                {
                    r.SubjectNames = r.SubjectNames.Substring(0, r.SubjectNames.Length - 1);
                }

                return result;
            }
        }

        /// <summary>
        /// 取得特定學生的課程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int[] GetSelectionFromStudent(int id)
        {
            using (var connection = connectionService.CreateConnection())
            {
                connection.Open();
                var query = @"select SubjectId from Selection where StudentId=@id";

                var result = connection.Query<int>(query, new { id }).ToArray();

                return result;
            }
        }

        /// <summary>
        /// 新增選課
        /// </summary>
        /// <param name="sId">學生Id</param>
        /// <param name="subjects">選擇的課程Id</param>
        public bool AddSelection(int sId, int[] subjects)
        {
            using (var connection = connectionService.CreateConnection())
            {
                connection.Open();

                using(var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query = @"INSERT INTO Selection (StudentId,SubjectId) VALUES (@sId, @subject)";
                        for (int i = 0; i < subjects.Length; i++)
                        {
                            connection.Execute(query, new { sId, subject = subjects[i] },transaction);
                        }

                        transaction.Commit();
                        logger.Info($"學生Id:${sId} 新增課程成功");
                        return true;
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        logger.Error($"學生Id:${sId} 新增課程失敗 Exception: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 更新選課資料
        /// </summary>
        /// <param name="sId">學生Id</param>
        /// <param name="subjects">選擇的課程Id</param>
        public bool UpdataSelection(int sId, int[] subjects)
        {
            using (var connection = connectionService.CreateConnection())
            {
                connection.Open();

                // 確認有無該學生Id的選課資料，有資料才刪除
                var count = connection.Query<int>(@"select COUNT(StudentId) from Selection where StudentId=@sId",
                    new { sId }).FirstOrDefault();
                if (count > 0)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 刪除該學生選課
                            var deleteQuery = @"delete from Selection where StudentId = @sId";
                            connection.Execute(deleteQuery, new { sId }, transaction);

                            if (subjects != null)
                            {
                                // 有選課則更新該學生選課
                                var insertQuery = @"INSERT INTO Selection (StudentId,SubjectId) VALUES (@sId, @subject)";
                                for (int i = 0; i < subjects.Length; i++)
                                {
                                    connection.Execute(insertQuery, new { sId, subject = subjects[i] }, transaction);
                                }
                            }

                            transaction.Commit();
                            logger.Info($"學生Id:${sId} 更新課程成功");
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            logger.Error($"學生Id:${sId} 更新課程失敗 Exception: {ex.Message}");
                            return false;
                        }
                    }
                }
                else
                {
                    logger.Error($"找不到學生Id:{sId} 更新課程失敗!");
                    return false;
                }
            }
        }

        /// <summary>
        /// 刪除學生選課資料
        /// </summary>
        /// <param name="sId">學生Id</param>
        public void DeleteSelection(int sId)
        {
            using (var connection = connectionService.CreateConnection())
            {
                connection.Open();

                // 確認有無該學生Id的選課資料，有資料才刪除
                var count = connection.Query<int>(@"select COUNT(StudentId) from Selection where StudentId=@sId", 
                    new { sId }).FirstOrDefault();
                if (count > 0)
                {
                    var query = @"delete from Selection where StudentId = @sId";
                    connection.Execute(query, new { sId });
                    logger.Info($"學生Id:${sId} 刪除課程成功!");
                }
                else
                {
                    logger.Error($"找不到學生Id:{sId} 刪除課程失敗!");
                }
            }
        }
    }
}