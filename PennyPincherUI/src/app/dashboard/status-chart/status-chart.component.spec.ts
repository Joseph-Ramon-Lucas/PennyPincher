import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatusChartComponent } from './status-chart.component';

describe('StatusChartComponent', () => {
  let component: StatusChartComponent;
  let fixture: ComponentFixture<StatusChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StatusChartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StatusChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
