import { Component, inject } from "@angular/core";
import { RouterModule, RouterOutlet } from "@angular/router";
import { NavBarComponent } from "./shared/nav-bar/nav-bar.component";
import { HeaderComponent } from "./shared/header/header.component";

@Component({
	selector: "app-root",
	imports: [RouterModule, RouterOutlet, HeaderComponent, NavBarComponent],
	templateUrl: "./app.component.html",
	styleUrls: ["./app.component.css"],
})
export class AppComponent {
	title = "Penny Pincher";
}
