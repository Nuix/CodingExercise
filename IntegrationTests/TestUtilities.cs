using System.Data;

namespace IntegrationTests
{
    internal class TestUtilities : IDisposable
    {
        IDbConnection _testConnection;
        private bool disposedValue;
        private const string sp_InsertUser = "[dbo].[InsertUser]";
        private const string sp_InsertInvestment = "[dbo].[InsertInvestment]";
        private const string sp_UpsertInvetmentRecord = "[dbo].[UpsertInvestmentRecord]";
        private const string sp_DeleteInvestmentRecord = "[dbo].[DeleteInvestmentRecord]";
        private const string sp_DeleteInvestment = "[dbo].[DeleteInvestment]";
        private const string sp_DeleteUser = "[dbo].[DeleteUser]";

        private const string param_UserGuid = "@userGuid";
        private const string param_InvestmentGuid = "@investmentGuid";
        private const string param_InvestmentName = "@investmentName";
        private const string param_DatePurchased = "@datePurchased";
        private const string param_CostBasisPerShare = "@costBasisPerShare";
        private const string param_NumberOfShares = "@numberOfShares";
        private const string param_CurrentPrice = "@currentPrice";

        public TestUtilities(IDbConnection testConnection)
        {
            _testConnection = testConnection;
            _testConnection.Open();
        }

        #region [Parameter Creation]
        public void CreateParameterUserGuid(IDbCommand command, Guid userGuid)
        {
            var paramUserGuid = command.CreateParameter();
            paramUserGuid.ParameterName = param_UserGuid;
            paramUserGuid.DbType = DbType.Guid;
            paramUserGuid.Value = userGuid;
            command.Parameters.Add(paramUserGuid);
        }

        public void CreateParameterInvestmentGuid(IDbCommand command, Guid investmentGuid)
        {
            var paramInvestmentGuid = command.CreateParameter();
            paramInvestmentGuid.ParameterName = param_InvestmentGuid;
            paramInvestmentGuid.DbType = DbType.Guid;
            paramInvestmentGuid.Value = investmentGuid;
            command.Parameters.Add(paramInvestmentGuid);
        }

        public void CreateParameterInvestmentName(IDbCommand command, string investmentName)
        {
            var paramInvestmentName = command.CreateParameter();
            paramInvestmentName.ParameterName = param_InvestmentName;
            paramInvestmentName.DbType = DbType.String;
            int maxLength = investmentName.Length > 10 ? 10 : investmentName.Length;
            paramInvestmentName.Value = investmentName.Substring(0, maxLength);
            command.Parameters.Add(paramInvestmentName);
        }

        public void CreateParameterPurchaseDate(IDbCommand command, DateTime purchaseDate)
        {
            var paramPurchaseDate = command.CreateParameter();
            paramPurchaseDate.ParameterName = param_DatePurchased;
            paramPurchaseDate.DbType = DbType.DateTime;
            paramPurchaseDate.Value = purchaseDate.ToUniversalTime();
            command.Parameters.Add(paramPurchaseDate);
        }

        public void CreateParameterCostBasisPerShare(IDbCommand command, Decimal costBasisPerShare)
        {
            var paramCostBasisPerShare = command.CreateParameter();
            paramCostBasisPerShare.ParameterName = param_CostBasisPerShare;
            paramCostBasisPerShare.DbType = DbType.Decimal;
            paramCostBasisPerShare.Value = costBasisPerShare;
            command.Parameters.Add(paramCostBasisPerShare);
        }

        public void CreateParameterNuberOfShares(IDbCommand command, long numberOfShares)
        {
            var paramNumberOfShares = command.CreateParameter();
            paramNumberOfShares.ParameterName = param_NumberOfShares;
            paramNumberOfShares.DbType = DbType.Int64;
            paramNumberOfShares.Value = numberOfShares;
            command.Parameters.Add(paramNumberOfShares);
        }

        public void CreateParameterCurrentPrice(IDbCommand command, Decimal currentPrice)
        {
            var paramCurrentPrice = command.CreateParameter();
            paramCurrentPrice.ParameterName = param_CurrentPrice;
            paramCurrentPrice.DbType = DbType.Decimal;
            paramCurrentPrice.Value = currentPrice;
            command.Parameters.Add(paramCurrentPrice);
        }

        #endregion [Parameter Creation]

        #region [Insert Operations]
        public int InsertUser(Guid userGuid)
        {
            int result = 0;
            using (var command = _testConnection.CreateCommand())
            {
                command.CommandText = sp_InsertUser;
                command.CommandType = CommandType.StoredProcedure;
                CreateParameterUserGuid(command, userGuid);
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public int InsertInvestment(Guid userGuid,  Guid investmentGuid, string investmentName)
        {
            int result = 0;
            using (var command = _testConnection.CreateCommand())
            {
                command.CommandText = sp_InsertInvestment;
                command.CommandType = CommandType.StoredProcedure;
                CreateParameterUserGuid(command, userGuid);
                CreateParameterInvestmentGuid(command, investmentGuid);
                CreateParameterInvestmentName(command, investmentName);
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public int UpsertInvestmentRecord(Guid investmentGuid, DateTime purchaseDate, decimal costBasisPerShare, 
            long numberOfShares, decimal currentPrice)
        {
            int result = 0;
            using (var command = _testConnection.CreateCommand())
            {
                command.CommandText = sp_UpsertInvetmentRecord;
                command.CommandType = CommandType.StoredProcedure;
                CreateParameterInvestmentGuid(command, investmentGuid);
                CreateParameterPurchaseDate(command, purchaseDate);
                CreateParameterCostBasisPerShare(command, costBasisPerShare);
                CreateParameterNuberOfShares(command, numberOfShares);
                CreateParameterCurrentPrice(command, currentPrice);
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        #endregion [Insert Operations]

        #region [Delete Operations]
        public int DeleteInvestmentRecord(Guid investmentGuid)
        {
            int result = 0;
            using (var command = _testConnection.CreateCommand())
            {
                command.CommandText = sp_DeleteInvestmentRecord;
                command.CommandType = CommandType.StoredProcedure;
                CreateParameterInvestmentGuid(command, investmentGuid);
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public int DeleteInvestment(Guid investmentGuid)
        {
            int result = 0;
            using (var command = _testConnection.CreateCommand())
            {
                command.CommandText = sp_DeleteInvestment;
                command.CommandType = CommandType.StoredProcedure;
                CreateParameterInvestmentGuid(command, investmentGuid);
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public int DeleteUser(Guid userGuid)
        {
            int result = 0;
            using (var command = _testConnection.CreateCommand())
            {
                command.CommandText = sp_DeleteUser;
                command.CommandType = CommandType.StoredProcedure;
                CreateParameterUserGuid(command, userGuid);
                result = command.ExecuteNonQuery();
            }
            return result;
        }
        #endregion [Delete Operations]

        #region [IDisposable Implementation]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_testConnection != null)
                    {
                        _testConnection.Dispose();
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
        #endregion [IDisposable Implementation]
    }
}
