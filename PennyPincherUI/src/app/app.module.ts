import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";

@NgModule({
	imports: [BrowserModule, RouterModule, CommonModule, FormsModule],
	declarations: [AppComponent],
	providers: [],
	bootstrap: [AppComponent],
})
export class AppModule {}
