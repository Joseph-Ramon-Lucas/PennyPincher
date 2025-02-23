import { Component, inject } from "@angular/core";
import { ExpenseTable } from "./expenses/expense-table.component";
import { ExpenseForm } from "./expenses/expense-form.component";
import { ExpenseComponent } from "./expenses/expense.component";

@Component({
	selector: "app-root",
	imports: [ExpenseComponent],
	templateUrl: "./app.component.html",
	styleUrl: "./app.component.css",
})
export class AppComponent {}
