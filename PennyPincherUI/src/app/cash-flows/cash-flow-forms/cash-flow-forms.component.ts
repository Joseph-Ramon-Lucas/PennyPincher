import { Component } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatDividerModule } from "@angular/material/divider";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatTabsModule } from "@angular/material/tabs";
import { MatTooltipModule } from "@angular/material/tooltip";
import { AddCashFlowPanelComponent } from "./add-cash-flow-panel/add-cash-flow-panel.component";

@Component({
	selector: "app-cash-flow-forms",
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
		AddCashFlowPanelComponent,
	],
	templateUrl: "./cash-flow-forms.component.html",
	styleUrl: "./cash-flow-forms.component.css",
})
export class CashFlowFormsComponent {}
