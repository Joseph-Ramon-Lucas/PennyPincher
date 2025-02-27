import { ChangeDetectionStrategy, Component, inject } from "@angular/core";
import { Expense } from "../models/expense";
import { ExpenseHistoryService } from "../services/expense-history.service";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatButtonModule } from "@angular/material/button";
import { MatDividerModule } from "@angular/material/divider";
import { MatIconModule } from "@angular/material/icon";
import { MatTooltipModule } from "@angular/material/tooltip";

@Component({
	selector: "expense-form",
	templateUrl: "./expense-form.component.html",
	styleUrl: "./expense-form.component.css",
	imports: [
		MatFormFieldModule,
		MatInputModule,
		MatSelectModule,
		MatButtonModule,
		MatDividerModule,
		MatIconModule,
		MatTooltipModule,
	],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ExpenseForm {
	title = "ExpenseForm";
	submitted = false;

	model = new Expense(1, "Chipotle", 1, 12.99);
	expenseHistoryService = inject(ExpenseHistoryService);

	constructor() {
		() => this.submit();
	}
	submit() {
		this.expenseHistoryService
			.addItem(this.model)
			.subscribe(() => this.onSubmit());
	}

	onSubmit() {
		this.submitted = true;
	}
}
