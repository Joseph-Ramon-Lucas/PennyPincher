import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCashFlowPanelComponent } from './add-cash-flow-panel.component';

describe('AddCashFlowPanelComponent', () => {
  let component: AddCashFlowPanelComponent;
  let fixture: ComponentFixture<AddCashFlowPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddCashFlowPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddCashFlowPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
