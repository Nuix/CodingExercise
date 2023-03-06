'use strict';

/**
 * Transforms an Investment record
 *
 * @param {any} record
 * @returns {{name, id, current_value: (*|string)}}
 */
module.exports = (record) => {
  return {
      id: record.id,
      name: record.name,
      current_value: record.current_value
  }
}
