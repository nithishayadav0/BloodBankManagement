namespace BloodBankManagement.Model
{
    public class BloodBank
    {
        public int Id { get; set; }
        public  string DonorName { get; set; }

        public  int Age { get; set; }

        public  string BloodType { get; set; }

        public  long ContactInfo { get; set; }
        public  string Quantity { get; set; }   
        public DateTime CollectionDate { get; set; }
        public  DateTime ExpirationDate { get; set; }

        public  string Status { get; set; }


    }
}
