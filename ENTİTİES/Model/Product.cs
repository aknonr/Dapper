namespace Entities.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Season { get; set; }
        public string Department { get; set; }
        public int ProductTypeId { get; set; }
        public int Brand { get; set; }
        public string Description { get; set; }
        public bool IsTransferred { get; set; }
        public DateTime TransferDate { get; set; }
        public string ProductId { get; set; }

    }
}
