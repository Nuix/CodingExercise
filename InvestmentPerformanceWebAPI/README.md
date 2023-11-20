# Investment Performance Web API
The structure of this API using the in memory database to store and read data from two tables.

Table 1 is the investmentinfo table which is used to store a unique investments with the primary key being investmentId.

Table 2 is the investorinfo table which is used to store a unique userid as well as the users name, the primary key is userId.

To use this API, we need to fill the databases with some test data and then we can use either the swagger page or the curl commands to test.

## Executing API
The API found in this repository can be run with the following command:

```console
dotnet run --launch-profile https
```

## Steps for testing api

swagger url: https://localhost:7047/swagger/index.html


Run the following curls (or use swagger) to add data to the investmentinfo table

```console
curl -X 'POST' \
  'https://localhost:7047/api/InvestmentInfo' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "investmentId": 1,
  "userId": 1,
  "shareNumber": 3,
  "boughtPrice": 4.5,
  "currentPrice": 5.5,
  "shortTerm": true
}'
```

```console
curl -X 'POST' \
  'https://localhost:7047/api/InvestmentInfo' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "investmentId": 2,
  "userId": 1,
  "shareNumber": 5,
  "boughtPrice": 3.75,
  "currentPrice": 1,
  "shortTerm": true
}'
```
Run the following curl (or use swagger) to add data to the investorinfo table

```console
curl -X 'POST' \
  'https://localhost:7047/api/InvestorInfo' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "userId": 1,
  "name": "Nuix"
}'
```

Run the following curl (or use swagger) to get the investor info for invester id 1

```console
curl -X 'GET' \
  'https://localhost:7047/api/InvestorInfo/1' \
  -H 'accept: text/plain'
```

Run the following curls (or use swagger) to get the investment info for each of the unique investment ids returned from the previous curl

```console
curl -X 'GET' \
  'https://localhost:7047/api/InvestmentInfo/2' \
  -H 'accept: text/plain'
```

```console
curl -X 'GET' \
  'https://localhost:7047/api/InvestmentInfo/1' \
  -H 'accept: text/plain'
```