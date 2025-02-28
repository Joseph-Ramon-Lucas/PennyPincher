import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteExpensePanelComponent } from './delete-expense-panel.component';

describe('DeleteExpensePanelComponent', () => {
  let component: DeleteExpensePanelComponent;
  let fixture: ComponentFixture<DeleteExpensePanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteExpensePanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteExpensePanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
