DROP DATABASE IF EXISTS penny_pincher_db;

CREATE DATABASE penny_pincher_db;

\c penny_pincher_db;

-- USERS TABLE
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    income DECIMAL,
    budget DECIMAL
);

-- EXPENSES TABLE
CREATE TABLE expenses (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES users(id), 
    name VARCHAR(500),
    category_type VARCHAR(200),
    price DECIMAL
);

-- CASHFLOWS TABLE
CREATE TABLE cashflows (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES users(id), 
    name VARCHAR(500),
    description VARCHAR(500),
    amount DECIMAL, 
    flow_type VARCHAR(200),
    category_type VARCHAR(200)
);
