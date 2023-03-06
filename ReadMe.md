# Coding Exercise
> This repository holds coding exercises for candidates going through the hiring process.

You should have been assigned one of the coding exercises in this list.  More details can be found in the specific md file for that assignment.

Instructions: Fork this repository, do the assigned work, and submit a pull request for review.

[Investment Performance Web API](InvestmentPerformanceWebAPI.md#investment-performance-web-api)

# Implementation

-  NodeJS back-end
-  Prisma ORM

Set your environment variables in a root `.env` file, including the port 
and host and your database connection string.

To quickly bootstrap an ENV file run:

```
$ cp .env.example ./env
```



Example connection string:

`sqlserver://192.168.1.14:1433;database=test-database; TrustServerCertificate=true;User Id=sa;Password=supersecret`

## Install yarn globally

```
$ npm i -g yarn
```

Alternatively you can run the following commands with `npm`

## Install Dependencies

```
$ yarn install
```

Install local JS dependencies including Express and other packages

## Database Migration & Seeding
```
$ yarn prisma migrate dev --schema database/schema.prisma
```

Alternatively, migration files will be available to run manually to set up 
the database.  Running the prisma migration command will automatically seed 
the database after migrating.

To manually seed the database run `$ yarn prisma seed`.

## Start The Server

```
$ yarn start
```

Will start the NodeJS HTTP service.

```
$ yarn dev
```

Will launch `nodemon`and reload the development server when a file inside of 
`./src` changes.


## Querying the API

Once the database is seeded, two endpoints can be called:

`/api/users/{userId}/investments`

This endpoint will return a list of a given users' investments.


`/api/users/{userId}/investments/{investmentId}`

This endpoint will return the details of the investment a user has made.

## Considerations

-  Setup Express to return JSON / API format (no views or front-end)
-  Setup dependency injection in the HTTP stack
- Passes a configuration of possible error messages
- Injects a list of pointers to service layer functions
- API Error Handling
- API endpoints will return a general error response and a 400 status code

# License

```
Copyright 2021 Nuix

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```
