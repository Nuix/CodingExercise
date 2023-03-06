require('dotenv').config()

require('../src/app')({
  port: process.env.APP_PORT,
  host: process.env.APP_HOST,
})
