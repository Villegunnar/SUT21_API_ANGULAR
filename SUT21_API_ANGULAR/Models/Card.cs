using System.ComponentModel.DataAnnotations;

namespace SUT21_API_ANGULAR.Models
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public string HolderName { get; set; }
        public string CardNumber { get; set; }
        public int ExpirMonth { get; set; }
        public int ExpirYear { get; set; }
        public int CVC { get; set; }

    }
}
