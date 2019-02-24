using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.Data.Model
{
    public class Article : ModelBase
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        // Relations
        public virtual User User { get; set; }
    }
}
