import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditExpensePanelComponent } from './edit-expense-panel.component';

describe('EditExpensePanelComponent', () => {
  let component: EditExpensePanelComponent;
  let fixture: ComponentFixture<EditExpensePanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditExpensePanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditExpensePanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
