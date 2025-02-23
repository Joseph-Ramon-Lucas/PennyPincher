import { Component, inject } from "@angular/core";
import { ExpenseHistoryService } from "../services/expense-history.service";

@Component({
	selector: "expense-table",
	templateUrl: "./expense-table.component.html",
	styleUrl: "./expense-table.component.css",
})
export class ExpenseTable {
	title = "ExpenseTable";
	// biome-ignore lint/suspicious/noExplicitAny: <explanation>
	expenses: any[] = [];
	expenseHistoryService = inject(ExpenseHistoryService);

	constructor() {
		this.expenseHistoryService.getAllItems().subscribe((expenses) => {
			this.expenses = expenses;
		});
	}
}
