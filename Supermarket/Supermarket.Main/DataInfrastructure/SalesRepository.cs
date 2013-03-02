using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supermarket.Core.Repositories;
using Supermarket.Core.Models;

namespace Supermarket.Main.DataInfrastructure
{
    public class SalesRepository : ISalesRepository
    {
        private readonly SupermarketDB _context = new SupermarketDB();

        public IEnumerable<Product> GetAvailableProducts()
        {
            var availabilityInfo = _context.ProductAvailabilities
                .AsNoTracking()
                .OrderByDescending(x => x.Date)
                .FirstOrDefault();
            if (availabilityInfo == null)
            {
                return null;
            }

            var result = availabilityInfo.ProductInfos.Where(x => x.Product.IsActive == true && x.Amount > 0).Select(x => x.Product);
            return result;
        }

        public void MakeSale(IEnumerable<SaleDetail> saleDetails)
        {
            //Remove availabilities and revert if needed
            //Add money
            //Add sale
            //Add sale details
            decimal amountToReceive = saleDetails.Sum(x => x.PricePerUnit * new Decimal(x.Amount));
            _context.CashDesk.Single().AvailableAmount += amountToReceive;
            this.MakeSales(saleDetails, amountToReceive);
        }

        private void MakeSales(IEnumerable<SaleDetail> saleDetails, decimal totalPayed)
        {
            Sale newSale = GenerateNewSale(totalPayed);
            ProductAvailability todaysAvailability = CheckAvailabilities();
            UpdateAvailabilities(saleDetails, newSale, todaysAvailability);
        }

        private ProductAvailability CheckAvailabilities()
        {
            ProductAvailability todayAvailabilityInfo = _context.ProductAvailabilities.ToList().SingleOrDefault(x => x.Date.Date.Equals(DateTime.Now.Date));
            if (todayAvailabilityInfo == null)
            {
                todayAvailabilityInfo = AddBaseAvailabilityForToday();
            }
            return todayAvailabilityInfo;
        }

        private ProductAvailability AddBaseAvailabilityForToday()
        {
            //Add availability info for today
            ProductAvailability todayAvailabilityInfo = new ProductAvailability() { Date = DateTime.Now.Date, ProductInfos = new HashSet<ProductAvailabilityDetail>() };
            ProductAvailability lastInfo = _context.ProductAvailabilities
                .OrderByDescending(x => x.Date)
                .FirstOrDefault(x => x.Date.CompareTo(todayAvailabilityInfo.Date) < 0);
            if (lastInfo != null)
            {
                //We have data from previous availabilities, copy it over
                foreach (var oldDetail in lastInfo.ProductInfos)
                {
                    todayAvailabilityInfo.ProductInfos.Add(new ProductAvailabilityDetail()
                    {
                        Amount = oldDetail.Amount,
                        ProductId = oldDetail.ProductId,
                        ProductAvailability = todayAvailabilityInfo
                    });
                }
            }
            _context.ProductAvailabilities.Add(todayAvailabilityInfo);
            return todayAvailabilityInfo;
        }

        private void UpdateAvailabilities(IEnumerable<SaleDetail> saleDetails, Sale newSale, ProductAvailability todaysAvailability)
        {
            foreach (var saleDetail in saleDetails)
            {
                var product = _context.Products.SingleOrDefault(x => x.Id == saleDetail.ProductId);
                if (product == null || product.IsActive == false)
                {
                    throw new InvalidOperationException("You can't sell deleted product " + product.Name);
                }
                saleDetail.Sale = newSale;
                _context.SaleDetails.Add(saleDetail);

                var stockInfo = todaysAvailability.ProductInfos.Single(x => x.ProductId == saleDetail.ProductId);
                stockInfo.Amount -= saleDetail.Amount;
            }
        }

        private Sale GenerateNewSale(decimal totalPayed)
        {
            Sale newSale = new Sale()
            {
                DateAndTime = DateTime.Now,
                TotalAmountPaid = totalPayed
            };
            _context.Sales.Add(newSale);
            return newSale;
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        public decimal GetStorePriceFor(int productId)
        {
            var product = _context.Products.AsNoTracking().SingleOrDefault(x => x.Id == productId && x.IsActive == true);
            if (product == null)
            {
                throw new InvalidOperationException("Invalid product selected, it isn't available in the store!");
            }
            return product.Price;
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}