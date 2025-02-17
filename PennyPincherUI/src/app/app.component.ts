import { Component, inject } from '@angular/core';
import { ExpenseTable } from './expenses/expense-table.component';
import { ExpenseForm } from './expenses/expense-form.component';

@Component({
  selector: 'app-root',
  imports: [ExpenseTable, ExpenseForm],
  template: ``,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
}
