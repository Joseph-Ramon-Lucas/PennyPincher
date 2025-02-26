import { Component } from "@angular/core";
import { RouterLink, RouterLinkActive } from "@angular/router";
import { NavBarComponent } from "../nav-bar/nav-bar.component";

@Component({
	selector: "app-header",
	imports: [RouterLink, RouterLinkActive, RouterLinkActive, NavBarComponent],
	templateUrl: "./header.component.html",
	styleUrl: "./header.component.css",
})
export class HeaderComponent {}
