using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_25_04
{
    public class New : Entity
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string MainText { get; set; }
        public string DetailText { get; set; }
        public DateTime PublishTime { get; set; }
        public List<Comment> Comments { get; set; }

        public override string ToString()
        {
            return $"Автор  : {Author}\n" +
                   $"Пишет  : {Title}\n" +
                   $"\t{MainText}\n" +
                   $"\t\t{DetailText}\n" +
                   $"{PublishTime}";
        }
    }
}
