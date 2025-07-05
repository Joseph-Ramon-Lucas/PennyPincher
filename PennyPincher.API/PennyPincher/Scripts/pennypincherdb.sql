DROP DATABASE IF EXISTS penny_pincher_db;
CREATE DATABASE penny_pincher_db;
\c penny_pincher_db;

CREATE TABLE IF NOT EXISTS public.user_account (
    user_id SERIAL PRIMARY KEY,
    token_id SERIAL,
    email VARCHAR(500),
    password VARCHAR(500)
);

CREATE TABLE IF NOT EXISTS public.cashflow_group (
    cashflow_group_id SERIAL PRIMARY KEY,
    group_name VARCHAR(500),
    description varchar(500)
);

CREATE TABLE IF NOT EXISTS public.cashflow_entry (
    cashflow_entry_id SERIAL PRIMARY KEY,
    cashflow_entry_name VARCHAR(500),
    description VARCHAR(500),
    amount NUMERIC,
    entry_date DATE,
    cashflow_entry_type VARCHAR(500),
    category_type VARCHAR(500)
);

CREATE TABLE IF NOT EXISTS public.management_profile (
    management_profile_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    cashflow_group_id INTEGER NOT NULL,
    cashflow_entry_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES public.user_account (user_id),
    FOREIGN KEY (cashflow_group_id) REFERENCES public.cashflow_group (cashflow_group_id),
    FOREIGN KEY (cashflow_entry_id) REFERENCES public.cashflow_entry (cashflow_entry_id)
);


-- DUMMY DATA
-- user_account
INSERT INTO public.user_account (user_id, token_id, email, password) VALUES
(1, 1, 'alice@mail.com', 'Passw0rd!'),
(2, 2, 'bob@mail.com', 'Qwerty123'),
(3, 3, 'carol@mail.com', 'Carol2025'),
(4, 4, 'dave@mail.com', 'Dave@2025'),
(5, 5, 'eve@mail.com', 'EveSecure');

-- cashflow_group
INSERT INTO public.cashflow_group (cashflow_group_id, group_name, description) VALUES
(1, 'Housing', 'Expenses related to living space'),
(2, 'Transportation', 'All travel and commuting expenses'),
(3, 'Food', 'Groceries and dining out'),
(4, 'Health', 'Medical and fitness expenses'),
(5, 'Entertainment', 'Leisure and fun activities'),
(6, 'Income', 'All sources of income');

-- cashflow_entry
INSERT INTO public.cashflow_entry (cashflow_entry_id, cashflow_entry_name, description, amount, entry_date, cashflow_entry_type, category_type) VALUES
(1, 'Rent', 'July apartment rent', 1200, '2025-07-01', 'Expense', 'Housing'),
(2, 'Bus Pass', 'Monthly bus subscription', 60, '2025-07-02', 'Expense', 'Transportation'),
(3, 'Groceries', 'Supermarket weekly shop', 110, '2025-07-03', 'Expense', 'Food'),
(4, 'Doctor Visit', 'Annual physical checkup', 80, '2025-07-04', 'Expense', 'Health'),
(5, 'Concert Ticket', 'Rock concert downtown', 75, '2025-07-05', 'Expense', 'Entertainment'),
(6, 'Water Bill', 'Monthly water utility', 25, '2025-07-06', 'Expense', 'Housing'),
(7, 'Gasoline', 'Car refueling', 50, '2025-07-07', 'Expense', 'Transportation'),
(8, 'Restaurant', 'Dinner at Italian place', 45, '2025-07-08', 'Expense', 'Food'),
(9, 'Yoga Class', 'Weekly yoga session', 20, '2025-07-09', 'Expense', 'Health'),
(10, 'Movie Night', 'Cinema with friends', 30, '2025-07-10', 'Expense', 'Entertainment'),
(11, 'Salary', 'Monthly salary payment', 3500, '2025-07-01', 'Income', 'Job'),
(12, 'Freelance Project', 'Payment for freelance web app', 600, '2025-07-05', 'Income', 'Freelance'),
(13, 'Gift', 'Birthday gift from family', 200, '2025-07-08', 'Income', 'Gift'),
(14, 'Investment Return', 'Monthly stock dividends', 120, '2025-07-09', 'Income', 'Investment');

-- management_profile
INSERT INTO public.management_profile (management_profile_id, user_id, cashflow_group_id, cashflow_entry_id) VALUES
(1, 1, 1, 1),
(2, 2, 2, 2),
(3, 3, 3, 3),
(4, 4, 4, 4),
(5, 5, 5, 5),
(6, 1, 1, 6),
(7, 2, 2, 7),
(8, 3, 3, 8),
(9, 4, 4, 9),
(10, 5, 5, 10),
(11, 1, 6, 11),
(12, 2, 6, 12),
(13, 3, 6, 13),
(14, 4, 6, 14);
