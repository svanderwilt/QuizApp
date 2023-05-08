using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class TestSummaryDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public DateTime TestStartTime { get; set; }
        public DateTime? TestEndTime { get; set; }
        public int Score { get; set; }
        public int AnsweredNo { get; set; }
        public bool Completed { get; set; }
    }
}