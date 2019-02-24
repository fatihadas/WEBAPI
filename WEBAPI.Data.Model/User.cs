using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.Data.Model
{
    public class User : ModelBase
    {
        public User()
        {
            this.Articles = new List<Article>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Fluently bir şekilde ilişkileri kullanabilmemiz için tanımlıyoruz.
        public virtual ICollection<Article> Articles { get; set; }
    }
}
