import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BugDesaFormComponent } from './bug-desa-form.component';

describe('BugDesaFormComponent', () => {
  let component: BugDesaFormComponent;
  let fixture: ComponentFixture<BugDesaFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BugDesaFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BugDesaFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
