using System;
using System.Collections.Generic;
using System.Text;

namespace Q103458
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public List<PostTag> PostTags { get; set; }
    }
}
