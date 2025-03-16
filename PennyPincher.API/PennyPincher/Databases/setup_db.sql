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
    id SERIAL PRIMARY KEY,
	id users_id NOT NULL REFERENCES users(id),
    name character(500),
    category_type expense_category,
    price decimal
);

CREATE TABLE cashflows (
    id SERIAL PRIMARY KEY,
	id users_id NOT NULL REFERENCES users(id),
    name character(500),
    description character(500),
    amount decimal, 
    flow_type cashflow_flow_type,
    expense_category expense_category   
);
