import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseLayoutComponent } from './base-layout/base-layout.component';
import { LoginComponent } from './components/login/login.component';
import { NgZorroModule } from '../NgZorro.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [BaseLayoutComponent, LoginComponent],
  imports: [
    NgZorroModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserModule,
  ],
  exports: [BaseLayoutComponent],
})
export class AuthModule {}
