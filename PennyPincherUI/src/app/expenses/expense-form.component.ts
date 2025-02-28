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
import { MatCardModule } from "@angular/material/card";
import { FormsModule } from "@angular/forms";

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
		MatCardModule,
		FormsModule,
	],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ExpenseForm {
	title = "ExpenseForm";
	submitted = false;
	expenseHistoryService = inject(ExpenseHistoryService);
	newItem = new Expense(1, "", 0, 0); //item id default to 1 for now

	constructor() {}
	handleSubmit() {
		console.log(this.newItem);

		this.expenseHistoryService
			.addItem(this.newItem)
			.subscribe(() => this.onSubmit());
	}

	onSubmit() {
		this.submitted = true;
	}
}
