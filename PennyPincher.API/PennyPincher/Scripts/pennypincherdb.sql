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
    -- side note that category types modeled in back end use Enums 
    -- and strings don't carry over nicely when querying
);

CREATE TABLE IF NOT EXISTS public.management_profile (
    management_profile_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    cashflow_group_id INTEGER NOT NULL,
    cashflow_entry_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES public.user_account (user_id) ON DELETE CASCADE,
    FOREIGN KEY (cashflow_group_id) REFERENCES public.cashflow_group (cashflow_group_id) ON DELETE CASCADE,
    FOREIGN KEY (cashflow_entry_id) REFERENCES public.cashflow_entry (cashflow_entry_id) ON DELETE CASCADE
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
-- More dummy cashflow_entry data (entries 15–26), mixing groups
INSERT INTO public.cashflow_entry (cashflow_entry_id, cashflow_entry_name, description, amount, entry_date, cashflow_entry_type, category_type) VALUES
(15, 'Grocery Run', 'Weekly groceries', 95, '2025-07-11', 'Expense', 'Food'),         -- For user 1
(16, 'Tram Ticket', 'Day pass for city tram', 6, '2025-07-12', 'Expense', 'Transportation'),  -- For user 1
(17, 'Gym Membership', 'Monthly fitness', 55, '2025-07-13', 'Expense', 'Health'),     -- For user 1
(18, 'Art Expo', 'Exhibition ticket', 20, '2025-07-14', 'Expense', 'Entertainment'),  -- For user 1
(19, 'Netflix', 'Streaming subscription', 15, '2025-07-15', 'Expense', 'Entertainment'), -- For user 2
(20, 'Lunch', 'Quick lunch at cafe', 13, '2025-07-16', 'Expense', 'Food'),           -- For user 2
(21, 'Prescription', 'Monthly medication', 32, '2025-07-17', 'Expense', 'Health'),   -- For user 2
(22, 'Repair', 'Bike tire repair', 20, '2025-07-18', 'Expense', 'Transportation'),   -- For user 3
(23, 'Bread', 'Bakery purchase', 7, '2025-07-19', 'Expense', 'Food'),               -- For user 3
(24, 'Therapy', 'Counseling session', 70, '2025-07-20', 'Expense', 'Health'),       -- For user 3
(25, 'Ski Trip', 'Winter weekend trip', 300, '2025-07-21', 'Expense', 'Entertainment'), -- For user 3
(26, 'Fitness App', 'Mobile workout app', 9, '2025-07-22', 'Expense', 'Health');    -- For user 4

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

-- Alice: Housing, Food, Transportation, Health, Entertainment, Income
INSERT INTO public.management_profile (management_profile_id, user_id, cashflow_group_id, cashflow_entry_id) VALUES
(15, 1, 3, 15),   -- Alice, Food
(16, 1, 2, 16),   -- Alice, Transportation
(17, 1, 4, 17),   -- Alice, Health
(18, 1, 5, 18),   -- Alice, Entertainment
(19, 2, 5, 19),   -- Bob, Entertainment
(20, 2, 3, 20),   -- Bob, Food
(21, 2, 4, 21),   -- Bob, Health
(22, 3, 2, 22),   -- Carol, Transportation
(23, 3, 3, 23),   -- Carol, Food
(24, 3, 4, 24),   -- Carol, Health
(25, 3, 5, 25),   -- Carol, Entertainment
(26, 4, 4, 26),   -- Dave, Health
(27, 4, 2, 7),    -- Dave, Transportation (gasoline, re-use existing entry)
(28, 5, 3, 8),    -- Eve, Food (restaurant)
(29, 5, 2, 2),    -- Eve, Transportation (bus pass; re-use entry)
(30, 5, 4, 4);    -- Eve, Health (doctor visit; re-use entry)