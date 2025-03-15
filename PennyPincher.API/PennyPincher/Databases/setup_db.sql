DROP DATABASE IF EXISTS penny_pinchers_db;

CREATE DATABASE penny_pinchers_db;

CREATE TYPE expense_category AS ENUM (
    'undefined', 
    'none', 
    'living',
    'utilities', 
    'entertainment',
    'shopping',
    'takeout'
);

CREATE TYPE cashflow_flow_type AS ENUM (
    'income',
    'expense'
);
       
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    income decimal,
    budget decimal
);

CREATE TABLE expenses (
    id SERIAL FOREIGN KEY,
    name character(500),
    category_type expense_category,
    price decimal
);

CREATE TABLE cashflows {
    id SERIAL FOREIGN KEY,
    name character(500),
    description character(500),
    amount decimal, 
    flow_type cashflow_flow_type,
    expense_category expense_category   
}
