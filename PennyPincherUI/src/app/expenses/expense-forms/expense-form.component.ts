import { ChangeDetectionStrategy, Component } from "@angular/core";
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
import { AddExpensePanelComponent } from "./add-expense-panel/add-expense-panel.component";
import { EditExpensePanelComponent } from "./edit-expense-panel/edit-expense-panel.component";
import { DeleteExpensePanelComponent } from "./delete-expense-panel/delete-expense-panel.component";

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
		MatTabsModule,
		AddExpensePanelComponent,
		EditExpensePanelComponent,
		DeleteExpensePanelComponent,
	],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ExpenseForm {
	title = "ExpenseForm";
	constructor() {}
}
