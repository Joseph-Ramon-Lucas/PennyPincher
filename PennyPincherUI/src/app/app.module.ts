import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";

import { AppComponent } from "./app.component";
import { ExpenseForm } from "./expenses/expense-form.component";

@NgModule({
  imports: [BrowserModule, CommonModule, FormsModule],
  declarations: [AppComponent, ExpenseForm],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule {}
