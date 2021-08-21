using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryWebApplication.DatabaseContexts;
using InventoryWebApplication.Models;
using Microsoft.Extensions.Logging;

namespace InventoryWebApplication.Services
{
    public class PaymentMethodsService : NameUniqueDatabaseService<PaymentMethod>
    {
        protected override void SetValues(PaymentMethod target, PaymentMethod values)
        {
            target.Name = values.Name;
            target.ProfitMarginPercentage = values.ProfitMarginPercentage;
        }

        public async Task Set(ICollection<PaymentMethod> newMethods)
        {
            foreach (PaymentMethod newMethod in newMethods)
            {
                await Add(newMethod);
                await UpdateByName(newMethod);
            }
            
            foreach (PaymentMethod method in ItemSet)
            {
                string lowerName = method.Name.ToLower();
                if (newMethods.All(o => o.Name.ToLower() != lowerName))
                {
                    await DeleteByName(method.Name);
                }
            }

            await DatabaseContext.SaveChangesAsync();
        }

        public PaymentMethodsService(DatabaseContext databaseContext, ILogger<DatabaseService<PaymentMethod>> logger) :
            base(databaseContext.PaymentMethods, databaseContext, logger) { }
    }
}