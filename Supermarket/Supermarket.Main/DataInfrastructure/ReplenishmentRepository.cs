using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Supermarket.Core.Models;
using Supermarket.Core.Repositories;

namespace Supermarket.Main.DataInfrastructure
{
    public class ReplenishmentRepository : IReplenishmentRepository
    {
        private readonly SupermarketDB _context = new SupermarketDB();


        public IEnumerable<Product> GetAvailableProducts()
        {
            var result = _context.Products.Where(p => p.IsActive == true).AsEnumerable();
            return result;
        }

        public bool EnoughMoneyInCashDeskForPayment(decimal amountToPay)
        {
            decimal currentAmount = _context.CashDesk.Single().AvailableAmount;
            bool result = currentAmount.CompareTo(amountToPay) >= 0;
            return result;
        }

        public decimal GetAvailableMoneyAmount()
        {
            decimal result = _context.CashDesk.Single().AvailableAmount;
            return result;
        }


        public void MakeReplenishment(IEnumerable<ReplenishmentDetail> replenishments)
        {
            decimal amountToPay = replenishments.Sum(x => new Decimal(x.Amount) * x.PricePerUnit);
            this.PayFromCashDesk(amountToPay);
            this.MakeReplenishments(replenishments);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region PrivateHelpers
        private void PayFromCashDesk(decimal amountToPay)
        {
            _context.CashDesk.Single().AvailableAmount -= amountToPay;
        }

        private void MakeReplenishments(IEnumerable<ReplenishmentDetail> replenishments)
        {
            Replenishment newReplenishment = new Replenishment()
            {
                DateAndTime = DateTime.Now,
                TotalAmountPaid = replenishments.Sum(r => new Decimal(r.Amount) * r.PricePerUnit),
            };
            _context.Replenishments.Add(newReplenishment);

            //Check for a product availability info for today
            ProductAvailability todayAvailabilityInfo = _context.ProductAvailabilities.ToList().SingleOrDefault(x => x.Date.Date.Equals(DateTime.Now.Date));
            if (todayAvailabilityInfo == null)
            {
                todayAvailabilityInfo = AddBaseAvailabilityForToday();
            }

            WriteReplenishmentDetailsToDB(replenishments, newReplenishment, todayAvailabilityInfo);
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

        private void WriteReplenishmentDetailsToDB(IEnumerable<ReplenishmentDetail> replenishments, Replenishment newReplenishment, ProductAvailability todayAvailabilityInfo)
        {
            foreach (var replenish in replenishments)
            {
                var product = _context.Products.SingleOrDefault(x => x.Id == replenish.ProductId);
                if (product == null || product.IsActive == false)
                {
                    throw new InvalidOperationException("You can't replenish deleted product " + product.Name);
                }
                replenish.Replenishment = newReplenishment;
                _context.ReplenishmentDetails.Add(replenish);

                HandleProductAvailability(replenish, todayAvailabilityInfo);
            }
        }

        private void HandleProductAvailability(ReplenishmentDetail replenishment, ProductAvailability todayAvailabilityInfo)
        {

            var stockInfo = todayAvailabilityInfo.ProductInfos.SingleOrDefault(x => x.ProductId == replenishment.ProductId);
            if (stockInfo == null)
            {
                //We've replenished a new product
                todayAvailabilityInfo.ProductInfos.Add(new ProductAvailabilityDetail()
                {
                    ProductId = replenishment.ProductId,
                    Amount = replenishment.Amount,
                    ProductAvailabilityId = todayAvailabilityInfo.Id
                });
            }
            else
            {
                stockInfo.Amount += replenishment.Amount;
            }

        }
        #endregion
    }
}