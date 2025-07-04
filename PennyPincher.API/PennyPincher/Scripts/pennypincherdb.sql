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
    group_name VARCHAR(500)
);

CREATE TABLE IF NOT EXISTS public.cashflow_entry (
    cashflow_entry_id SERIAL PRIMARY KEY,
    cashflow_entry_name VARCHAR(500),
    description VARCHAR(500),
    amount NUMERIC,
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