import {
	ChangeDetectionStrategy,
	Component,
	computed,
	inject,
	input,
	output,
	signal,
} from "@angular/core";
import { ExpenseDto } from "../../../models/expense";
import { ExpenseHistoryService } from "../../../services/expense-history.service";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatButtonModule } from "@angular/material/button";
import { MatDividerModule } from "@angular/material/divider";
import { MatIconModule } from "@angular/material/icon";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatCardModule } from "@angular/material/card";
import { FormsModule } from "@angular/forms";
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
	],
})
export class AddExpensePanelComponent {
	title = "add-expense";

	expenseHistoryService = inject(ExpenseHistoryService);
	newItem = new ExpenseDto(1, "", 0, 0);

	handleSubmit() {
		console.log(this.newItem);

		this.expenseHistoryService.addItem(this.newItem).subscribe({
			complete: () => {
				// push update upon successful submission to refresh table
				this.expenseHistoryService.isSubmitted$.next(true);
			},
		});
	}
}
