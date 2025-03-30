import {
	ChangeDetectionStrategy,
	Component,
	computed,
	inject,
	input,
	output,
	signal,
} from "@angular/core";
import { CATEGORY_TYPES, ExpenseDto } from "../../../models/expense";
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
	selector: "add-expense-panel",
	templateUrl: "./add-expense-panel.component.html",
	styleUrl: "./add-expense-panel.component.css",
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
})
export class AddExpensePanelComponent {
	title = "add-expense";

	private expenseHistoryService = inject(ExpenseHistoryService);
	protected expenseForm = this.expenseHistoryService.expenseForm;

	handleSubmit() {
		const newItem: ExpenseDto = new ExpenseDto(
			this.expenseForm.value.id ?? 0,
			this.expenseForm.value.name ?? "",
			this.expenseForm.value.category ?? CATEGORY_TYPES.None,
			this.expenseForm.value.price ?? 0,
		);

		this.expenseHistoryService.addItem(newItem).subscribe({
			complete: () => {
				// push update upon successful submission to refresh table
				this.expenseHistoryService.submissionComplete$.next();
			},
		});
	}
}
