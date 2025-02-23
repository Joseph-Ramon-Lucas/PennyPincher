import { Component, inject } from "@angular/core";
import { ExpenseTable } from "./expense-table.component";
import { ExpenseForm } from "./expense-form.component";

@Component({
	selector: "app-expense",
	imports: [ExpenseTable, ExpenseForm],
	templateUrl: "./expense.component.html",
	styleUrl: "./expense.component.css",
})
export class ExpenseComponent {}
