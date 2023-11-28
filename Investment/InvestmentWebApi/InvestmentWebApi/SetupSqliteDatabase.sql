PRAGMA foreign_keys = ON;

DROP TABLE IF EXISTS investments;
DROP TABLE IF EXISTS stock_prices;
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS stocks;

CREATE TABLE IF NOT EXISTS users(
                                    user_id INTEGER NOT NULL PRIMARY KEY,
                                    name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS stocks(
                                     stock_id INTEGER NOT NULL PRIMARY KEY ,
                                     name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS stock_prices (
                                            stock_id INTEGER NOT NULL,
                                            start_time TEXT NOT NULL,
                                            end_time TEXT NULL,
                                            price REAL NOT NULL,
                                            PRIMARY KEY(stock_id, start_time, end_time),
                                            FOREIGN KEY (stock_id)
                                                REFERENCES stocks (stock_id)
);

CREATE TABLE IF NOT EXISTS investments(
                                          investment_id INTEGER NOT NULL PRIMARY KEY,
                                          user_id INTEGER NOT NULL,
                                          stock_id INTEGER NOT NULL,
                                          share_count REAL NOT NULL,
                                          acquire_date TEXT NOT NULL,
                                          FOREIGN KEY (user_id)
                                              REFERENCES users (user_id),
                                          FOREIGN KEY (stock_id)
                                              REFERENCES stocks (stock_id)
);

INSERT INTO users(user_id, name)
VALUES
    (1, 'Mr. Mon O. Polly'),
    (2, 'Miss Taye Ken'),
    (3, 'Ms. Dow Tefire'),
    (4, 'Mr. Dow Jones'),
    (5, 'Mrs. S.P. Fivehundred');

INSERT INTO stocks(stock_id, name)
VALUES
    (1, 'Pear'),
    (2, 'Coal'),
    (3, 'Lumber'),
    (4, 'Fish'),
    (5, 'Cheese');

INSERT INTO stock_prices(
    stock_id, start_time, end_time, price)
VALUES
    (1, '2000-01-01T00:00:00', '2010-01-01T00:00:00', 50),
    (1, '2010-01-01T00:00:00', '2020-01-01T00:00:00', 60),
    (1, '2020-01-01T00:00:00', '2021-01-01T00:00:00', 70),
    (1, '2021-01-01T00:00:00', '2022-01-01T00:00:00', 80),
    (1, '2022-01-01T00:00:00', NULL, 90),
    (2, '2000-01-01T00:00:00', '2010-01-01T00:00:00', 150),
    (2, '2010-01-01T00:00:00', '2020-01-01T00:00:00', 160),
    (2, '2020-01-01T00:00:00', '2021-01-01T00:00:00', 170),
    (2, '2021-01-01T00:00:00', '2022-01-01T00:00:00', 180),
    (2, '2022-01-01T00:00:00', NULL, 190),
    (3, '2000-01-01T00:00:00', '2010-01-01T00:00:00', 250),
    (3, '2010-01-01T00:00:00', '2020-01-01T00:00:00', 260),
    (3, '2020-01-01T00:00:00', '2021-01-01T00:00:00', 270),
    (3, '2021-01-01T00:00:00', '2022-01-01T00:00:00', 280),
    (3, '2022-01-01T00:00:00', NULL, 290),
    (4, '2000-01-01T00:00:00', '2010-01-01T00:00:00', 350),
    (4, '2010-01-01T00:00:00', '2020-01-01T00:00:00', 360),
    (4, '2020-01-01T00:00:00', '2021-01-01T00:00:00', 370),
    (4, '2021-01-01T00:00:00', '2022-01-01T00:00:00', 380),
    (4, '2022-01-01T00:00:00', NULL, 390),
    (5, '2000-01-01T00:00:00', '2010-01-01T00:00:00', 450),
    (5, '2010-01-01T00:00:00', '2020-01-01T00:00:00', 460),
    (5, '2020-01-01T00:00:00', '2021-01-01T00:00:00', 470),
    (5, '2021-01-01T00:00:00', '2022-01-01T00:00:00', 480),
    (5, '2022-01-01T00:00:00', NULL, 490);

INSERT INTO investments(
    investment_id, user_id, stock_id, share_count, acquire_date)
VALUES
    (1, 1, 1, 20, '2011-05-06T00:00:00'),
    (2, 1, 3, 20, '2015-01-05T00:00:00'),
    (3, 1, 5, 20, '2023-05-06T00:00:00'),

    (4, 2, 2, 20, '2019-08-24T00:00:00'),
    (5, 2, 4, 20, '2023-10-08T00:00:00'),

    (6, 3, 1, 20, '2023-05-06T00:00:00'),

    (7, 4, 1, 20, '2023-01-06T00:00:00'),
    (8, 4, 2, 20, '2023-02-06T00:00:00'),
    (9, 4, 3, 20, '2023-03-06T00:00:00'),
    (10, 4, 4, 20, '2023-04-06T00:00:00'),
    (11, 4, 5, 20, '2023-05-06T00:00:00');