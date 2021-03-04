using System.Collections.ObjectModel;

namespace omnia_blazor_demo.Shared.DTO
{
    public class OrderDto
    {
        public string _code { get; set; }
        public string _serie { get; set; }
        public int? _number { get; set; }
        public string _date { get; set; }
        public string client { get; set; }
        public string salesPerson { get; set; }
        public decimal? totalWithVat { get; set; }
        public decimal? totalWithoutVat { get; set; }
        public decimal? vatTotal { get; set; }
        public ObservableCollection<OrderLineDto> lines { get; set; }
    }
}