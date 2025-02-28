import { Component, inject } from "@angular/core";
import { ExpenseForUpdateDto } from "../../../models/expense";
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
	],
	templateUrl: "./edit-expense-panel.component.html",
	styleUrl: "./edit-expense-panel.component.css",
})
export class EditExpensePanelComponent {
	title = "edit-item";
	submitted = false;
	expenseHistoryService = inject(ExpenseHistoryService);
	newItemId = 0;
	newItem = new ExpenseForUpdateDto("", 0, 0);

	handleSubmit() {
		console.log(this.newItem);

		this.expenseHistoryService
			.updateItem(this.newItemId, this.newItem)
			.subscribe(() => this.onSubmit());
	}

	onSubmit() {
		this.submitted = true;
	}
}
