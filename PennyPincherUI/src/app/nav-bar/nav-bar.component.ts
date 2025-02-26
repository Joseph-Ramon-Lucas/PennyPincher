import { Component } from "@angular/core";
import { RouterLink } from "@angular/router";
import { MatIconModule } from "@angular/material/icon";
import { MatDividerModule } from "@angular/material/divider";
import { MatButtonModule } from "@angular/material/button";
@Component({
	selector: "app-nav-bar",
	imports: [RouterLink, MatButtonModule, MatDividerModule, MatIconModule],
	templateUrl: "./nav-bar.component.html",
	styleUrl: "./nav-bar.component.css",
})
export class NavBarComponent {}
