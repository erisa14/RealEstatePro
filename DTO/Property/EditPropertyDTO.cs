using Helpers.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Property
{
    public class EditPropertyDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter the category!")]
        public CategoryEnum.CategoryName CategoryType { get; set; }

        [Required(ErrorMessage = "Please enter the location!")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the price!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the square area!")]
        public int SquareArea { get; set; }

        [Required(ErrorMessage = "Please enter the number of floors!")]
        public int NumberOfFloors { get; set; }

        [Required(ErrorMessage = "Please enter a decription!")]
        public string Description { get; set; } = null!;

        //public int Status { get; set; }

        //  [Required(ErrorMessage = "Please upload at least 5 photos!")]
        // public Guid PhotoId { get; set; }
    }
}
