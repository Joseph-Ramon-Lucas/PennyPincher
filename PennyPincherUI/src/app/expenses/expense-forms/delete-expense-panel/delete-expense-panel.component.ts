import { Component, inject } from "@angular/core";
import { ExpenseDto, ExpenseForUpdateDto } from "../../../models/expense";
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
	selector: "delete-expense-panel",
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
	templateUrl: "./delete-expense-panel.component.html",
	styleUrl: "./delete-expense-panel.component.css",
})
export class DeleteExpensePanelComponent {
	title = "delete-expense";
	submitted = false;
	expenseHistoryService = inject(ExpenseHistoryService);
	newItem = new ExpenseDto(1, "a", 1, 1); //dummy data to avoid nulls - all we care about is the id

	handleSubmit() {
		console.log(this.newItem);

		this.expenseHistoryService.deleteItem(this.newItem).subscribe((result) => {
			console.log(result);
			this.onSubmit();
		});
	}

	onSubmit() {
		this.submitted = true;
	}
}
