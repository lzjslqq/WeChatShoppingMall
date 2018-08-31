using DapperExtension.Core;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RecommendRepo : BaseRepo<Recommend>
    {
        public RecommendRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public bool Cancel(long caseId)
        {
            var sql = @"delete from dbo.Recommend where caseId = @caseId ";
            return DbManage.Execute(sql, new { caseId }) > 0;
        }
    }
}