using Dapper.Contrib.Extensions;
using System;

using System;

using System.ComponentModel.DataAnnotations;

namespace Model
{
    [Table("[dbo].[Recommend]")]
    public class Recommend : ModelBase
    {
        public long CaseId { get; set; }

        public string Remark { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}