import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectAssignFormComponent } from './project-assign-form.component';

describe('ProjectAssignFormComponent', () => {
  let component: ProjectAssignFormComponent;
  let fixture: ComponentFixture<ProjectAssignFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectAssignFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectAssignFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
