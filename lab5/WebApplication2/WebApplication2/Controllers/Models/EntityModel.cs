using System.Collections.Generic;

namespace WebApplication2.Models
{
    // Модель для навчального аналізу текстів
    public class EntityModel
    {
        public string InputText { get; set; }
        public List<EntityResult> Entities { get; set; }
    }

    // Результат розпізнавання сутності
    public class EntityResult
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public double ConfidenceScore { get; set; }
        public string Description { get; set; }
    }

}
