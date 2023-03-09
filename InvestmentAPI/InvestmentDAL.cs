using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;

namespace InvestmentAPI
{
    internal class InvestmentDAL : IInvestmentDAL
    {
        private IDbConnection NuixDb { get; set; }
        private const string sp_GetInvestmentRecordFromInvestmentGuid = "[dbo].[GetInvestmentRecordFromInvestmentGuid]";
        private const string sp_GetInvestmentsFromUserGuid = "[dbo].[GetInvestmentsFromUserGuid]";
        private const string param_UserGuid = "@UserGuid";
        private const string param_InvestmentGuid = "@InvestmentGuid";
        private readonly ILogger _logger;

        public InvestmentDAL(IDbConnection conn, ILogger logger)
        {
            NuixDb = conn;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of investment items for a user guid.
        /// </summary>
        /// <param name="userId">Guid</param>
        /// <returns>List of InvestmentItem</returns>
        public List<InvestmentItem> GetInvestmentItemsByUserId(Guid userId)
        {
            List<InvestmentItem> investments = new List<InvestmentItem>();
            try
            {
                using (var command = NuixDb.CreateCommand())
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = param_UserGuid;
                    parameter.DbType = DbType.Guid;
                    parameter.Value = userId;
                    command.Parameters.Add(parameter);
                    command.CommandText = sp_GetInvestmentsFromUserGuid;
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader(CommandBehavior.Default))
                    {
                        while (reader.Read())
                        {
                            var item = new InvestmentItem();
                            item.Id = reader.GetGuid(0);
                            item.Name = reader.GetString(1).Trim();
                            investments.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
            }
            return investments;
        }

        /// <summary>
        /// Gets an Investment Record based on the guid of the investment.
        /// </summary>
        /// <param name="investmentId">Guid</param>
        /// <returns>InvestmentRecord</returns>
        public InvestmentRecord GetInvestmentRecordByInvestmentId(Guid investmentId)
        {
            InvestmentRecord record = null;
            try
            {
                using (var command = NuixDb.CreateCommand())
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = param_InvestmentGuid;
                    parameter.DbType = DbType.Guid;
                    parameter.Value = investmentId;
                    command.Parameters.Add(parameter);
                    command.CommandText = sp_GetInvestmentRecordFromInvestmentGuid;
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        reader.Read();
                        var purchaseDate = reader.GetDateTime(0);
                        var costBasisPerShare = reader.GetDecimal(1);
                        var numberOfShares = reader.GetInt64(2);
                        var currentPrice = reader.GetDecimal(3);
                        record = new InvestmentRecord(numberOfShares, costBasisPerShare, currentPrice, purchaseDate);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
            }
            return record;
        }
    }
}
