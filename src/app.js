'use strict'
const express = require('express')
const httpErrors = require('http-errors')
const useragent = require('express-useragent')
const bodyParser = require('body-parser')
require('dotenv').config()

/**
 * Application entrypoint
 *
 * @param {object|null} options
 * @param {function|null} cb
 */
module.exports = function main(options, cb) {
  const ready = cb || function () {}
  const opts = Object.assign({}, options)

  let server
  let serverStarted = false
  let serverClosing = false

  /**
   * General server-side process error reporting
   *
   * @param {any} err
   */
  const unhandledError = (err) => {
    if (serverClosing) {
      return
    }
    serverClosing = true

    // Report an error to the console
    console.error(err)

    if (serverStarted) {
      server.close(function () {
        process.exit(1)
      })
    }
  }
  process.on('uncaughtException', unhandledError)
  process.on('unhandledRejection', unhandledError)

  const app = express()

  app.use(bodyParser.urlencoded({ extended: false }))

  app.use(useragent.express())

  // Handle CORS and API access
  app.use((req, res, next) => {
    res.header('Access-Control-Allow-Origin', '*')
    res.header(
      'Access-Control-Allow-Headers',
      'Origin, X-Requested-With, Content-Type, Accept, User-Agent',
    )
    res.header(
      'Access-Control-Allow-Methods',
      'GET, POST, DELETE, PUT, PATCH, OPTIONS',
    )
    req.ERRORS = require('./config/Errors')
    req.SERVICES = require('./config/Services')
    next()
  })

  // Set routes to return API data instead of views
  app.use(express.json())

  // Routes
  require('./routes')(app, opts)

  /**
   * General 404 Handler
   */
  app.use(function fourOhFourHandler(req, res, next) {
    next(httpErrors(404, `Route not found: ${req.url}`))
  })

  /**
   * Report Internal Server Error
   */
  app.use(function fiveHundredHandler(err, req, res, next) {
    res.status(err.status || 500).json({
      messages: [
        {
          code: err.code || 'InternalServerError',
          message: err.message,
        },
      ],
    })
  })

  // Removes x-powered-by header
  app.disable('x-powered-by')

  server = app.listen(opts.port, opts.host,  (err) => {
    if (err) {
      return ready(err, app, server)
    }
    if (serverClosing) {
      return ready(new Error('Server was closed before it could start'))
    }
    serverStarted = true
    const addr = server.address()
    console.info(
      `Started server at ${opts.host || addr.host || 'localhost'}:${addr.port}`,
    )

    ready(err, app, server)
  })
}
