import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCashFlowPanelComponent } from './edit-cash-flow-panel.component';

describe('EditCashFlowPanelComponent', () => {
  let component: EditCashFlowPanelComponent;
  let fixture: ComponentFixture<EditCashFlowPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditCashFlowPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditCashFlowPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
