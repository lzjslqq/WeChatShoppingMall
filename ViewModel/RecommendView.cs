using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class RecommendView
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string CaseName { get; set; }
        public DateTime AddTime { get; set; }
        public string CustomerName { get; set; }
    }
}