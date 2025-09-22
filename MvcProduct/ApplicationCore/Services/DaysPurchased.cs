using MvcProduct.ApplicationCore.Interfaces;

namespace MvcProduct.ApplicationCore.Services
{
    public class DaysPurchased : IDaysPurchased
    { 
        public int CalculateDays (DateTime purchaseDate)
        {
            //Measures the distance by days.
            return (DateTime.Now - purchaseDate).Days;
        }
    }
}
