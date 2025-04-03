import { Component } from "@angular/core";
import { AnalysisComponent } from "./analysis/analysis.component";

@Component({
	selector: "app-dashboard",
	imports: [AnalysisComponent],
	templateUrl: "./dashboard.component.html",
	styleUrl: "./dashboard.component.css",
})
export class DashboardComponent {}
