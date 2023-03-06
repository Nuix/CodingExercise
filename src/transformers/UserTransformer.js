'use strict';
const InvestmentTransformer = require('./InvestmentTransformer')

/**
 * Transforms a user record
 *
 * @param {any} record
 * @returns {{name, created_at: (*|Date), id, investments: Promise<{name, id, current_value: *}>, email: *}}
 */
module.exports = (record) => {
    return record
          .UserInvestments
          .map(record => InvestmentTransformer(record.Investment))
}
