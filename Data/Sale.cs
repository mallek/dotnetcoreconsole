using System;

namespace SkunkCalc.Data
{
    public class Sale
    {
         public int RecordKey { get; set; }
        public string CompanyNumber { get; set; }
        public string RecordStatus { get; set; }
        public string DTPrimaryEmployeeNumber { get; set; }
        public string DTSecondaryEmployeeNumber { get; set; }
        public string VIN { get; set; }
        public string StockNumber { get; set; }
        public DateTime? SalesDate { get; set; }
        public string SaleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool IsNew { get; set; }
        public char VehicleType { get; set; }
        public decimal CommissionGross { get; set; }
        public decimal FnIGross { get; set; }
        public DateTime DateCapped { get; set; }
        public string CustomerName { get; set; }
        public decimal VehicleCost { get; set; }
        public decimal HouseGross { get; set; }
        public string RecordType { get; set; }
        public decimal DealerIncentive { get; set; }
        public bool HasServiceContract { get; set; }
        public bool IsCPO { get; set; } = false;
		public string CompanyDisplayName { get; set; }
    }
}