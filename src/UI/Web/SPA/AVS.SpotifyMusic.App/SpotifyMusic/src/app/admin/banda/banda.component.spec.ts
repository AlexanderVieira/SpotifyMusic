/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BandaComponent } from './banda.component';

describe('BandaComponent', () => {
  let component: BandaComponent;
  let fixture: ComponentFixture<BandaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BandaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BandaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
