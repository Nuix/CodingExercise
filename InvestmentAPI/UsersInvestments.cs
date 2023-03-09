using System;
using System.Collections.Generic;

namespace InvestmentAPI
{
    public class UsersInvestments: IDisposable
    {
        private bool disposedValue;

        public Guid UserId { get; set; }
        public List<InvestmentItem> Investments { get; set; } = new List<InvestmentItem>();
        public UsersInvestments() { }
        public UsersInvestments(Guid userId, List<InvestmentItem> investments)
        {
            UserId = userId;
            Investments = investments;
        }

        #region [IDisposable implementation]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Investments != null)
                    {
                        Investments.Clear();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion [IDisposable implementation]
    }
}
