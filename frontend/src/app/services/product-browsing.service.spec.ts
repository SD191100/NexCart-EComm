import { TestBed } from '@angular/core/testing';

import { ProductBrowsingService } from './product-browsing.service';

describe('ProductBrowsingService', () => {
  let service: ProductBrowsingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductBrowsingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
