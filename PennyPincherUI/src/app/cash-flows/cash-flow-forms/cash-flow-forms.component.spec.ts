import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CashFlowFormsComponent } from './cash-flow-forms.component';

describe('CashFlowFormsComponent', () => {
  let component: CashFlowFormsComponent;
  let fixture: ComponentFixture<CashFlowFormsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CashFlowFormsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CashFlowFormsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
