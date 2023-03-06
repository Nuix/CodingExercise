const { client } = require('./BaseModel')

/**
 * Fetches a user record with a list of investments
 *
 * @param {number} userId
 * @returns {Promise<void>}
 * @constructor
 */
async function GetUserWithInvestments(userId) {
  return client.user.findUnique({
    where: {
      id: userId
    },
    include: {
      UserInvestments: {
        select: {
          investment_id: true,
          Investment: {
            select: {
              id: true,
              name: true,
            }
          }
        }
      }
    }
  })
}

/**
 * Fetches a users' investment detail record
 *
 * @param {number} userId
 * @param {number} investmentId
 * @returns {Promise<void>}
 * @constructor
 */
async function GetUserInvestmentDetail(userId, investmentId) {
  return client.userInvestments.findUnique({
    where: {
      user_id_investment_id: {
        user_id: userId,
        investment_id: investmentId
      },
    },
    include: {
      Investment: true
    }
  })
}

module.exports = {
  GetUserWithInvestments,
  GetUserInvestmentDetail
}
