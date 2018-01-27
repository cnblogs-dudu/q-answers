using System;
using System.Collections.Generic;
using System.Text;

namespace Q103458
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
