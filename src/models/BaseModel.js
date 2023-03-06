const { PrismaClient, Prisma } = require('@prisma/client')

const queryLogger = {
  log: [
    {
      emit: 'event',
      level: 'query',
    },
    {
      emit: 'stdout',
      level: 'error',
    },
    {
      emit: 'stdout',
      level: 'info',
    },
    {
      emit: 'stdout',
      level: 'warn',
    },
  ],
}

const prisma = new PrismaClient(
  process.env.QUERY_LOGGER === 'true' ? queryLogger : {},
)

if (process.env.QUERY_LOGGER === 'true') {
  prisma.$on('query', e => {
    console.log('Query: ' + e.query)
    console.log('Params: ' + e.params)
    console.log('Duration : ' + e.duration + 'ms')
  })
}

module.exports = {
  client: prisma,
  Prisma: Prisma,
}
