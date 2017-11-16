using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public class SellItem
    {
        public long? SellItemId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The quantity should be bigger than {1}")]
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal UnitPrice { get; set; }
        public long? ProductId { get; set; }
        public Product Product { get; set; }
        public long? SellId { get; set; }
        public Sell Sell { get; set; }
    }
}