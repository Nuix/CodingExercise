/**
 * Gets a list of investments given a user
 *
 * @param {Express.Request} req
 * @param {Express.Response} res
 * @constructor
 */
const ListUserInvestments = async (req, res) => {
  try {
    const response = await req
      .SERVICES
      .USER_SERVICE
      .ListUserInvestmentsService(
        parseInt(req.params.userId)
      )
    return res.json(response)
  } catch(e) {
    return res
      .status(400)
      .json(
        {"error": req.ERRORS.GENERAL_ERROR_MESSAGE})
  }
}

/**
 * Gets the detail of an investment given an investment
 *
 * @param {Express.Request} req
 * @param {Express.Response} res
 * @returns {Promise<*>}
 * @constructor
 */
const GetUserInvestmentDetail = async (req, res) => {
  try {
    const response = await req
      .SERVICES
      .USER_SERVICE
      .GetUserInvestmentDetail(
        parseInt(req.params.userId),
        parseInt(req.params.investmentId)
      )
    return res.json(response)
  } catch(e) {
    console.error(e)
    return res
      .status(400)
      .json(
        {"error": req.ERRORS.GENERAL_ERROR_MESSAGE})
  }
}

/**
 * InvestmentsController
 * @type {{ListUserInvestments: ((function(*, *): Promise<*|undefined>)|*), GetUserInvestmentDetail: ((function(*, *): Promise<*|undefined>)|*)}}
 */
module.exports = {
  GetUserInvestmentDetail,
  ListUserInvestments,
}
