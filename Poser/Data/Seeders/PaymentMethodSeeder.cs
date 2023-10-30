using Poser.EF;
using Poser.Core.Models.Orders;

namespace Poser.Data.Seeders
{
    public class PaymentMethodSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod { Name = "Cash" , IsActive = true},
                new PaymentMethod { Name = "Credit" , IsActive = true},
                new PaymentMethod { Name = "Tamara" , IsActive = true},
                new PaymentMethod { Name = "Tabby" , IsActive = true},
            };

            foreach (var paymentMethod in paymentMethods)
            {
                context.PaymentMethods.Add(paymentMethod);
            }
            context.SaveChanges();
        }
    }
}