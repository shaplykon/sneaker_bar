﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sneaker_Bar.Models
{
    public class PurchaseRepository
    {
        private readonly PurchaseContext context;

        public PurchaseRepository(PurchaseContext _context)
        {
            context = _context;
        }

        public IQueryable<Purchase> GetPurchases()
        {
            return context.Purchases.OrderBy(x => x.Date);
        }

        public IQueryable<Purchase> GetPurchaseByUserId(int Id)
        {
            return context.Purchases.Where(x => x.UserId == Id);
        }

        public IQueryable<Purchase> GetPurchaseBySneakersId(int Id)
        {
            return context.Purchases.Where(x => x.SneakersId == Id);
        }

        public bool IsInPurchases(int userId, int sneakersId) {
            IQueryable<Purchase> purchase = context.Purchases.Where(x => x.SneakersId == sneakersId && x.UserId == userId);
            if (purchase.Count() == 0) return false;
            else return true;
        }

        public int SavePurchase(Purchase purchase)
        {
            if (purchase.PurchaseId == default)
            {
                context.Entry(purchase).State = EntityState.Added;
            }
            else
            {
                context.Entry(purchase).State = EntityState.Modified;
            }
            context.SaveChanges();
            return purchase.PurchaseId;
        }

        public void DeletePurchase(Purchase purchase)
        {
            context.Purchases.Remove(purchase);
            context.SaveChanges();
        }

        public void DeletePurchaseById(int userId, int sneakersId)
        {
            Purchase purchase = context.Purchases.Where(x => x.SneakersId == sneakersId && x.UserId == userId).SingleOrDefault();
            context.Purchases.Remove(purchase);
            context.SaveChanges();
        }
    }
}
