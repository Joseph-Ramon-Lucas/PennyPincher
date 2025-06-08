DROP DATABASE IF EXISTS penny_pincher_db;

CREATE DATABASE penny_pincher_db;

\c penny_pincher_db;

CREATE TABLE IF NOT EXISTS public.user_account (
    user_id SERIAL PRIMARY KEY,
    token_id SERIAL,
    email VARCHAR(500),
    password VARCHAR(500)
);

CREATE TABLE IF NOT EXISTS public.actual_group (
    actual_group_id SERIAL PRIMARY KEY,
    group_name VARCHAR(500)
);

CREATE TABLE IF NOT EXISTS public.actual_cashflow_entry (
    actual_cashflow_entry_id SERIAL PRIMARY KEY,
    actual_cashflow_entry_name VARCHAR(500),
    description VARCHAR(500),
    amount NUMERIC,
    cashflow_entry_type VARCHAR(500),
    category_type VARCHAR(500)
);

CREATE TABLE IF NOT EXISTS public.budget_group (
    budget_group_id SERIAL PRIMARY KEY,
    group_name VARCHAR(500)
);

CREATE TABLE IF NOT EXISTS public.budget_cashflow_entry (
    budget_cashflow_entry_id SERIAL PRIMARY KEY,
    budget_name VARCHAR(500),
    description VARCHAR(500),
    amount NUMERIC,
    cashflow_entry_type VARCHAR(500),
    category_type VARCHAR(500)
);

CREATE TABLE IF NOT EXISTS public.actual_junction (
    actual_junction_id SERIAL PRIMARY KEY,
    actual_group_id INTEGER NOT NULL,
    actual_cashflow_entry_id INTEGER NOT NULL,
    CONSTRAINT fk_actual_group FOREIGN KEY (actual_group_id)
        REFERENCES public.actual_group (actual_group_id),
    CONSTRAINT fk_actual_entry FOREIGN KEY (actual_cashflow_entry_id)
        REFERENCES public.actual_cashflow_entry (actual_cashflow_entry_id)
);

CREATE TABLE IF NOT EXISTS public.budget_junction (
    budget_junction_id SERIAL PRIMARY KEY,
    budget_group_id INTEGER NOT NULL,
    budget_cashflow_entry_id INTEGER NOT NULL,
    CONSTRAINT fk_budget_group FOREIGN KEY (budget_group_id)
        REFERENCES public.budget_group (budget_group_id),
    CONSTRAINT fk_budget_entry FOREIGN KEY (budget_cashflow_entry_id)
        REFERENCES public.budget_cashflow_entry (budget_cashflow_entry_id)
);

CREATE TABLE IF NOT EXISTS public.financial_management (
    financial_managment_id SERIAL PRIMARY KEY,
    budget_junction_id INTEGER NOT NULL,
    actual_junction_id INTEGER NOT NULL,
    user_account_id INTEGER NOT NULL,
    CONSTRAINT fk_user_account FOREIGN KEY (user_account_id)
        REFERENCES public.user_account (user_id),
    CONSTRAINT fk_budget_junction FOREIGN KEY (budget_junction_id)
        REFERENCES public.budget_junction (budget_junction_id),
    CONSTRAINT fk_actual_junction FOREIGN KEY (actual_junction_id)
        REFERENCES public.actual_junction (actual_junction_id)
);
