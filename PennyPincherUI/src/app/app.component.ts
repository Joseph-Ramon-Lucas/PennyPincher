import { Component, inject } from "@angular/core";
import {
	RouterModule,
	RouterOutlet,
	RouterLink,
	RouterLinkActive,
} from "@angular/router";

@Component({
	selector: "app-root",
	imports: [RouterModule, RouterOutlet, RouterLink, RouterLinkActive],
	templateUrl: "./app.component.html",
	template: `

  `,
	styleUrl: "./app.component.css",
})
export class AppComponent {
	title = "Penny Pincher";
}
