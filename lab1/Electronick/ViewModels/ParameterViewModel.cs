using Electronick.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Electronick.ViewModels
{
    public class ParameterViewModel
    {
        public int Id { get; set; }
        [Remote("IsNameParameterAvailable", "Home", ErrorMessage = "Компанія з такою назвою вже існує")]
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string NameCompany { get; set; }// назва виробника
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 200 символів")]
        public string CategoryCompany { get; set; }//категогорія пристрою
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(900, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 400 символів")]
        public string Description { get; set; } // Опис компанії 
        public ParameterViewModel() { }
        public ParameterViewModel(Parameter parameter)
        {   Id = parameter.Id;
            NameCompany = parameter.NameCompany;
            CategoryCompany = parameter.CategoryCompany;
            Description = parameter.Description;
        }
        }                                   
}
