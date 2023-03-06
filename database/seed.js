const { PrismaClient } = require('@prisma/client')
const { faker } = require('@faker-js/faker')
const prisma = new PrismaClient()

/**
 * Generates a random number
 *
 * @param {number} MAX
 * @returns {number}
 * @constructor
 */
const RANDOM_NUM_GEN = MAX => Math.floor(Math.random() * (MAX ?? 254 - 5 + 1) + 5)

/**
 * Seeds some user data with investments
 *
 * @returns {Promise<void>}
 */
async function main() {
  let n = 1
  const investments = RANDOM_NUM_GEN(254)
  while(n < investments) {
    await prisma.investment.create({
      data: {
        name: faker.company.name(),
        current_value: faker.commerce.price(),
        created_at: faker.date.past(5),
      }
    })
    n++
  }
  const users = RANDOM_NUM_GEN(254)
  const numberOfInvestments = RANDOM_NUM_GEN(16)
  let randomInvestments = []
  n = 1
  while(n < numberOfInvestments) {
    const investmentsCount = await prisma.investment.count();
    const skip = Math.floor(Math.random() * investmentsCount);
    const randomInvestment = await prisma.investment.findMany({
      take: 1,
      skip: skip,
      orderBy: {
        id: 'desc',
      },
    })
    randomInvestments.push({
      id: randomInvestment[0].id,
    })
    n++
  }
  while(n < users) {
    await prisma.user.create({
      data: {
        email: faker.internet.email(),
        name: faker.name.fullName(),
        UserInvestments: {
          create: randomInvestments.map(investment => {
            return {
              Investment: {
                connect: { id: investment.id }
              },
              shares: RANDOM_NUM_GEN(200),
              purchase_value: faker.commerce.price(),
              created_at: faker.date.past(5)
            }
          })
        }
      }
    })
    n++
  }
}

main()
  .then(async () => {
    await prisma.$disconnect()
  })
  .catch(async (e) => {
    console.error(e)
    await prisma.$disconnect()
    process.exit(1)
  })
