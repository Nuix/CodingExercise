const express = require('express')
const { ListUserInvestments, GetUserInvestmentDetail } = require('../http/controllers/UsersController')

/**
 * ExpressJS Router
 *
 * @param {Express} app
 * @param {object|null} opts
 */
module.exports = function (app, opts) {
  app.get('/api/users/:userId/investments', ListUserInvestments)
  app.get('/api/users/:userId/investments/:investmentId', GetUserInvestmentDetail)
}

