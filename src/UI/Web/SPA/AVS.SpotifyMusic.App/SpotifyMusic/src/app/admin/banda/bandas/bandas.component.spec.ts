/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BandasComponent } from './bandas.component';

describe('BandasComponent', () => {
  let component: BandasComponent;
  let fixture: ComponentFixture<BandasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BandasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BandasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
