using System;
using System.Collections.Generic;

namespace TransitSystem.Frontend.Services
{
    public class TransactionModel
    {
        public string Method { get; set; } = "Yape";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }

    public class TransactionService
    {
        public decimal CurrentBalance { get; private set; } = 42.50m;
        public List<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();

        public event Action OnChange;

        public void AddTransaction(string method, decimal amount)
        {
            Transactions.Insert(0, new TransactionModel { Method = method, Amount = amount, Date = DateTime.Now });
            CurrentBalance += amount;
            NotifyStateChanged();
        }

        public void UpdateBalance(decimal newBalance)
        {
            CurrentBalance = newBalance;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}