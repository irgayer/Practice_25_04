using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_25_04
{
    public class Comment : Entity
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime PublishTime { get; set; }
        public Guid IdNew { get; set; }

        public override string ToString()
        {
            return $"{PublishTime}\n" +
                   $"{Author}: " +
                   $"{Text}";
        }
    }
}
