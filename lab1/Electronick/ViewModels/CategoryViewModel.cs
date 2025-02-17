using Electronick.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Electronick.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Remote("IsNameCategoryAvailable", "Home",ErrorMessage = "Модель з такою назвою вже існує")]
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string NameCategory { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(400, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 400 символів")]
        public string spacifications { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string waranty { get; set; }

        public CategoryViewModel() { }

        public CategoryViewModel(Category category)
        {
            Id = category.Id;
            NameCategory = category.NameCategory;
            spacifications = category.spacifications;
            waranty = category.waranty;
        }
    }
}
