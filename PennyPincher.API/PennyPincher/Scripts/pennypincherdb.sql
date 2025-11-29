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
    group_name VARCHAR(500) NOT NULL,
    description varchar(500),
    user_id INTEGER NOT NULL,
    
    FOREIGN KEY (user_id) REFERENCES public.user_account (user_id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS public.cashflow_entry (
    cashflow_entry_id SERIAL PRIMARY KEY,
    cashflow_entry_name VARCHAR(500) NOT NULL,
    description VARCHAR(500),
    amount NUMERIC NOT NULL,
    entry_date DATE,
    cashflow_entry_type VARCHAR(500),
    category_type VARCHAR(500),
    -- side note that category types modeled in back end use Enums 
    -- and strings don't carry over nicely when querying
    user_id INTEGER NOT NULL,
    
    FOREIGN KEY (user_id) REFERENCES public.user_account (user_id) ON DELETE CASCADE
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
INSERT INTO public.user_account (email, password) VALUES
    ('alice@mail.com',    'Passw0rd!'),
    ('bob@mail.com',      'Qwerty123'),
    ('carol@mail.com',    'Carol2025'),
    ('dave@mail.com',     'Dave@2025'),
    ('eve@mail.com',      'EveSecure');

INSERT INTO public.cashflow_group (group_name, description, user_id) VALUES
    ('Housing',       'Expenses related to living space',     1),
    ('Utilities',     'Power, water, heating bills',          1),
    ('Income',        'Salary and side hustles',               1),
    ('Transportation','All travel and commuting expenses',    2),
    ('Entertainment', 'Leisure, fun, streaming',               2),
    ('Food',          'Groceries, dining out, cafes',          3),
    ('Health',        'Medical, sports, wellness',             3),
    ('Fitness',       'Gym memberships and classes',           4),
    ('Medical',       'Checkups, medication',                   4),
    ('Hobbies',       'Crafts, gaming, reading',                4),
    ('Travel',        'Vacations, getaways, hotels',            5),
    ('Insurance',     'Car, health, property insurance',        5),
    ('Pets',          'Food, vet, toys for pets',                5);

INSERT INTO public.cashflow_entry (
    cashflow_entry_name, description, amount, entry_date,
    cashflow_entry_type, category_type, user_id
) VALUES
    ( 'Rent',             'Monthly apartment rent',          1200, '2025-07-01', 'Expense', 'Housing',       1),
    ( 'Power Bill',       'Electricity bill',                  90, '2025-07-03', 'Expense', 'Utilities',     1),
    ( 'Internet',         'Monthly internet bill',             60, '2025-07-05', 'Expense', 'Utilities',     1),
    ( 'Salary',           'Primary job income',              3400, '2025-07-10', 'Income',  'Income',        1),
    ( 'Side Project',     'Freelancing payout',               600, '2025-07-15', 'Income',  'Income',        1),
    ( 'Bus Pass',         'Local transit monthly pass',        60, '2025-07-02', 'Expense', 'Transportation', 2),
    ( 'Bike Maintenance', 'Annual checkup for bicycle',        50, '2025-07-09', 'Expense', 'Transportation', 2),
    ( 'Netflix',          'Streaming subscription',            15, '2025-07-13', 'Expense', 'Entertainment',  2),
    ( 'Concert Ticket',   'Attending live concert',            75, '2025-07-16', 'Expense', 'Entertainment',  2),
    ('Groceries',        'Weekly grocery trip',              120, '2025-07-03', 'Expense', 'Food',          3),
    ('Dinner Out',       'Meal at Italian bistro',            45, '2025-07-10', 'Expense', 'Food',          3),
    ('Doctor Visit',     'General health checkup',            80, '2025-07-11', 'Expense', 'Health',        3),
    ('Gym Membership',   'Monthly gym fee',                   55, '2025-07-20', 'Expense', 'Health',        3),
    ('Yoga Class',       'Drop-in yoga session',              20, '2025-07-09', 'Expense', 'Fitness',       4),
    ('Therapy Session',  'Mental health counseling',          90, '2025-07-12', 'Expense', 'Medical',       4),
    ('Video Game',       'Purchased new release',             60, '2025-07-15', 'Expense', 'Hobbies',       4),
    ('Book Purchase',    'Bought novel at bookstore',         25, '2025-07-18', 'Expense', 'Hobbies',       4),
    ('Flight Ticket',    'Round trip to lake cabin',         400, '2025-07-06', 'Expense', 'Travel',        5),
    ('Hotel Booking',    'Weekend stay at resort',           350, '2025-07-14', 'Expense', 'Travel',        5),
    ('Pet Food',         'Bulk dog food purchase',            60, '2025-07-07', 'Expense', 'Pets',          5),
    ('Vet Appointment',  'Dog annual checkup',                80, '2025-07-17', 'Expense', 'Pets',          5),
    ('Car Insurance',    'Monthly premium',                  120, '2025-07-11', 'Expense', 'Insurance',     5),
    ('Health Insurance', 'Monthly health premium',           350, '2025-07-01', 'Expense', 'Insurance',     5);

INSERT INTO public.management_profile (user_id, cashflow_group_id, cashflow_entry_id) VALUES
    (1,  1,  1),
    (1,  2,  2),
    (1,  2,  3),
    (1,  3,  4),
    (1,  3,  5),
    (2,  4,  6),
    (2,  4,  7),
    (2,  5,  8),
    (2,  5,  9),
    (3,  6, 10),
    (3,  6, 11),
    (3,  7, 12),
    (3,  7, 13),
    (4,  8, 14),
    (4,  9, 15),
    (4, 10, 16),
    (4, 10, 17),
    (5, 11, 18),
    (5, 11, 19),
    (5, 13, 20),
    (5, 13, 21),
    (5, 12, 22),
    (5, 12, 23);
