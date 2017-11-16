using System.ComponentModel.DataAnnotations;

namespace Front.Models.ViewModel
{
    public class SellItemVM : SellItem
    {
        [DataType(DataType.Currency)]
        public decimal Total { get { return this.Quantity * this.UnitPrice; } }
    }
}