using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class EntityModel
    {
        public string InputText { get; set; }
        public List<EntityResult> Entities { get; set; }
    }

    public class EntityResult
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public double ConfidenceScore { get; set; }
    }
}
