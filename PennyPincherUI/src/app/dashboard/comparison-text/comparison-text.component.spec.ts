import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComparisonTextComponent } from './comparison-text.component';

describe('ComparisonTextComponent', () => {
  let component: ComparisonTextComponent;
  let fixture: ComponentFixture<ComparisonTextComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComparisonTextComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComparisonTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
