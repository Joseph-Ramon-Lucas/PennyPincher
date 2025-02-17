import { Component } from '@angular/core';
import { Expense } from '../models/expense';

@Component({
  selector: 'expense-form',
  templateUrl: './expense-form.component.html',
  styleUrl: './expense-form.component.css'
})
export class ExpenseForm {
  title = 'ExpenseForm'
  submitted = false;

  model = new Expense(1, "Chipotle", 1, 2.00);

  constructor() {}

  onSubmit() {
    this.submitted = true;
  }

}
