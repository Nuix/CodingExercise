const UserModel = require('../../models/User')
const UserTransformer = require('../../transformers/UserTransformer')
const InvestmentDetailTransformer = require('../../transformers/InvestmentDetailTransformer')

/**
 * Service-layer to access a users' investments
 *
 * @param {number} userId
 * @returns {Promise<void>}
 * @constructor
 */
const ListUserInvestmentsService = async userId => {
  try {
    const result = await UserModel.GetUserWithInvestments(userId)
    return UserTransformer(result)
  } catch(e) {
    throw e
  }
}

/**
 * Service-layer to access a users' investment detail
 *
 * @param userId
 * @param investmentId
 * @returns {Promise<void>}
 * @constructor
 */
const GetUserInvestmentDetail = async (userId, investmentId) => {
  try {
    const result = await UserModel.GetUserInvestmentDetail(
      userId,
      investmentId
    )
    return InvestmentDetailTransformer(result)
  } catch(e) {
    throw e
  }
}

module.exports = {
  ListUserInvestmentsService,
  GetUserInvestmentDetail
}
