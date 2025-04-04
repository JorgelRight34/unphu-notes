import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginBtnComponent } from './login-btn.component';

describe('LoginBtnComponent', () => {
  let component: LoginBtnComponent;
  let fixture: ComponentFixture<LoginBtnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginBtnComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginBtnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
