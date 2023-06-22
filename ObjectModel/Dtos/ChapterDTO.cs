using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectModel.Dtos
{
    public class ChapterDTO
    {
        public int ChapterId { get; set; }

        public int View { get; set; }

        public int Index { get; set; }

        public DateTime CreateAt { get; set; }

        public int StoryId { get; set; }
    }
}
