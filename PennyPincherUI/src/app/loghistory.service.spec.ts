import { TestBed } from '@angular/core/testing';

import { LoghistoryService } from './loghistory.service';

describe('LoghistoryService', () => {
  let service: LoghistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoghistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
