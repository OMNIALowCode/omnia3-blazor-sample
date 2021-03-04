namespace omnia_blazor_demo.Shared.DTO
{
    public class OrderLineDto : OmniaEntityDto
    {
        public string product {get;set;}
        public int? quantity {get;set;}
        public decimal? unitPrice {get;set;}
        public string vatTax {get;set;}
        public decimal? value {get;set;}
        public decimal? vatValue {get;set;}
        public decimal? totalWithVat {get;set;}
    }
}