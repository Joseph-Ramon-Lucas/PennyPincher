import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatusTextComponent } from './status-text.component';

describe('StatusTextComponent', () => {
  let component: StatusTextComponent;
  let fixture: ComponentFixture<StatusTextComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StatusTextComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StatusTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
