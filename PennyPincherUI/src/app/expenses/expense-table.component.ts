import {
	Component,
	inject,
	type OnChanges,
	type OnInit,
	type SimpleChanges,
} from "@angular/core";
import type { Expense } from "../models/expense";
import { ExpenseHistoryService } from "../services/expense-history.service";
import { MatIconModule } from "@angular/material/icon";
import { MatDividerModule } from "@angular/material/divider";
import { MatButtonModule } from "@angular/material/button";
import { MatTooltipModule } from "@angular/material/tooltip";

@Component({
	selector: "expense-table",
	templateUrl: "./expense-table.component.html",
	styleUrl: "./expense-table.component.css",
	imports: [MatButtonModule, MatDividerModule, MatIconModule, MatTooltipModule],
})
export class ExpenseTable implements OnInit {
	title = "ExpenseTable";
	expenses: Expense[] = [];
	expenseHistoryService = inject(ExpenseHistoryService);

	constructor() {}
	ngOnInit(): void {
		this.updateTable();
	}
	// ngOnChanges(changes: SimpleChanges): void {
	// 	console.log("changes!", changes);
	// 	this.updateTable();
	// }

	updateTable(): void {
		this.expenseHistoryService.getAllItems().subscribe((expenses) => {
			this.expenses = expenses;
		});
	}
}
