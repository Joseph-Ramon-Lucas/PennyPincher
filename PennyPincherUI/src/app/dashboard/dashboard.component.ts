import { Component } from "@angular/core";
import { NavBarComponent } from "../nav-bar/nav-bar.component";

@Component({
	selector: "app-dashboard",
	imports: [NavBarComponent],
	templateUrl: "./dashboard.component.html",
	styleUrl: "./dashboard.component.css",
})
export class DashboardComponent {}
