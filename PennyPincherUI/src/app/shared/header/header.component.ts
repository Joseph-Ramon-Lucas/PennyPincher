import { Component, CUSTOM_ELEMENTS_SCHEMA } from "@angular/core";
import { RouterLink, RouterLinkActive } from "@angular/router";

@Component({
	selector: "app-header",
	imports: [RouterLink, RouterLinkActive, RouterLinkActive],
	templateUrl: "./header.component.html",
	styleUrl: "./header.component.css",
	schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class HeaderComponent {}
