using Electronick.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Electronick.Models
{
    public class Parameter
    {
        public int Id { get; set; }
        public string NameCompany { get; set; }// назва виробника

        public string CategoryCompany { get; set; }//категогорія пристрою

        public string Description { get; set; } // Опис компанії 
                                                // Зовнішній ключ

        public ICollection<Device>? Devices { get; set; }

        public Parameter() { }
        public Parameter(ParameterViewModel parameterViewModel)

        {
            Id = parameterViewModel.Id;
            NameCompany = parameterViewModel.NameCompany;
            CategoryCompany = parameterViewModel.CategoryCompany;
            Description = parameterViewModel.Description;
        }
    }
}
