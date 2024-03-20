using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class History
    {
        public Guid AdministratorId { get; set; }
        public string Description { get; set; } = null!;
        public DateTimeOffset Date { get; set; }
        public int HistoryType { get; set; }
    }
}
