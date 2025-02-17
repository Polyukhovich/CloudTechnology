using Electronick.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Electronick.Models
{
    public class Device
    {// ID 
        public int Id { get; set; }
        //назва 
        public string Name { get; set; }

        // Ціна
        public int Price { get; set; }
        //дата виходу
        public DateTime Date { get; set; }
        public int ParameterId { get; set; }
        public  Parameter? Parameter { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public Device() { }
       public Device(DeviceViewModel deviceViewModel)
        {
            Id = deviceViewModel.Id;
            Name = deviceViewModel.Name;
            Price = deviceViewModel.Price;
            Date = deviceViewModel.Date;
            ParameterId = deviceViewModel.ParameterId;
            CategoryId = deviceViewModel.CategoryId;

        }
    }
}
