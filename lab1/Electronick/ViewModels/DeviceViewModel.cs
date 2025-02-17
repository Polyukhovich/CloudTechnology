using Electronick.Models;
using System.ComponentModel.DataAnnotations;
namespace Electronick.ViewModels
{
    public class DeviceViewModel
    {
        public int Id { get; set; }
        public string? Parameter { get; set; }
        //назва 
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжина повинна бути від 3 до 30 символів")]
        public string Name { get; set; }

        // Ціна
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [Range(1, int.MaxValue, ErrorMessage = "Ціна не може бути від'ємною або нюльовою")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Це поле є обов'язковим")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int ParameterId { get; set; }
        public string? Description { get; set; }
        public string? CategoryCompany { get; set; }
        public string? NameCompany { get; set; }


        public int CategoryId { get; set; }
        public string? NameCategory { get; set; }
        public string? spacifications {  get; set; }
        public string? waranty { get; set; }

        public DeviceViewModel() { }
        public DeviceViewModel(Device device)
        {
            Id = device.Id;
            Name = device.Name;
            Price = device.Price;
            Date = device.Date;
            ParameterId = device.ParameterId;
            if (device.Parameter != null)
            {
                ParameterId = device.Parameter.Id;
               Description = device.Parameter.Description;
              CategoryCompany = device.Parameter.CategoryCompany;
              NameCompany = device.Parameter.NameCompany;

            }
            if(device.Category != null)
            {
                CategoryId = device.Category.Id;
                NameCategory = device.Category.NameCategory;
                spacifications = device.Category.spacifications;
                waranty = device.Category.waranty;


            }
        }
    }
}
