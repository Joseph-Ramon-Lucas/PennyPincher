import { Component } from "@angular/core";
import { RouterLink } from "@angular/router";
import { MatIconModule } from "@angular/material/icon";
import { MatDividerModule } from "@angular/material/divider";
import { MatButtonModule } from "@angular/material/button";
import { MatTooltipModule } from "@angular/material/tooltip";

@Component({
	selector: "app-nav-bar",
	imports: [
		RouterLink,
		MatButtonModule,
		MatDividerModule,
		MatIconModule,
		MatTooltipModule,
	],
	templateUrl: "./nav-bar.component.html",
	styleUrl: "./nav-bar.component.css",
})
export class NavBarComponent {}
