import type { Routes } from "@angular/router";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { CashFlowsComponent } from "./cash-flows/cash-flows.component";
import { ExpenseComponent } from "./expenses/expense.component";
import { ErrorPageNotFoundComponent } from "./error-page-not-found/error-page-not-found.component";

export const routes: Routes = [
	{ path: "", redirectTo: "/dashboard", pathMatch: "full" },
	{ path: "expenses", component: ExpenseComponent },
	{ path: "dashboard", component: DashboardComponent },
	{ path: "budget", component: CashFlowsComponent },
	{ path: "**", component: ErrorPageNotFoundComponent },
];
