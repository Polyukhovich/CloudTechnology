using Electronick.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Electronick.Models
{
    public class Category

    {
        public int Id { get; set; }
        [Remote("IsNameCategoryAvailable", "Home", ErrorMessage = "Модель з такою назвою вже існує")]
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string NameCategory { get; set; }//Категорія
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(400, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 400 символів")]
        public string spacifications { get; set; }//технічні характеристики 

        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string waranty { get; set; } //гарантія 

        public ICollection<Device>? Devices { get; set; }
        public Category() { }
        public Category(CategoryViewModel categoryViewModel)
        {
            Id = categoryViewModel.Id;
            NameCategory = categoryViewModel.NameCategory;
            spacifications = categoryViewModel.spacifications;
            waranty = categoryViewModel.waranty;
        }
    }
}
