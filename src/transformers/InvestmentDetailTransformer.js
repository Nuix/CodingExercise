'use strict';

/**
 * Transforms a user record
 *
 * @param {any} record
 * @returns {Promise<{name, created_at: *, id, investments: Promise<{name: *, id: *, current_value: *}>, email: *}>}
 */
module.exports = async (record) => {
  const now = Date.now()
  const diffInDays = then => Math.round((now-then) / (1000*60*60*24))
  const longTerm = diffInDays(record.created_at) >= 365
  const netGainLoss = record.Investment.current_value - (record.purchase_value * record.shares)
  return {
    id: record.Investment.id,
    name: record.Investment.name,
    cost_basis_per_share: "$"+record.purchase_value,
    shares: record.shares,
    current_value: "$"+record.Investment.current_value,
    term: longTerm ? "Long" : "Short",
    total_gain_or_loss: netGainLoss
  }
}
