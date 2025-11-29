import { Component, inject } from "@angular/core";
import {
	CATEGORY_TYPES,
	ExpenseDto,
	ExpenseForUpdateDto,
} from "../../../models/expense";
import { ExpenseHistoryService } from "../../../services/expense-history.service";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatButtonModule } from "@angular/material/button";
import { MatDividerModule } from "@angular/material/divider";
import { MatIconModule } from "@angular/material/icon";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatCardModule } from "@angular/material/card";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatTabsModule } from "@angular/material/tabs";

@Component({
	selector: "edit-expense-panel",
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
		MatTabsModule,
		ReactiveFormsModule,
	],
	templateUrl: "./edit-expense-panel.component.html",
	styleUrl: "./edit-expense-panel.component.css",
})
export class EditExpensePanelComponent {
	title = "edit-item";
	private expenseHistoryService = inject(ExpenseHistoryService);
	protected expenseForm = this.expenseHistoryService.expenseForm;

	handleSubmit() {
		const newItemId = this.expenseForm.value.id ?? 0;
		const newItem: ExpenseForUpdateDto = new ExpenseForUpdateDto(
			this.expenseForm.value.name ?? "",
			this.expenseForm.value.price ?? 0,
			this.expenseForm.value.category ?? CATEGORY_TYPES.None,
		);

		this.expenseHistoryService.updateItem(newItemId, newItem).subscribe({
			complete: () => {
				this.expenseHistoryService.submissionComplete$.next();
			},
		});
	}
}
