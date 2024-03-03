import { NgModule, Component } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { NzMenuModule } from 'ng-zorro-antd/menu';
import { LoginComponent } from './login/login.component';
import { NgZorroModule } from '../NgZorro.module';
import { ReactiveFormsModule } from '@angular/forms';
import { TableComponent } from './table/table.component';
import { FormsModule } from '@angular/forms';
import { ChartComponent } from './chart/chart.component';
import { CardComponent } from './card/card.component';
import { CommonModule } from '@angular/common';
import { UploadComponent } from './upload/upload.component';
import { HeaderComponent } from './header/header.component';

@NgModule({
  declarations: [
    LoginComponent,
    TableComponent,
    ChartComponent,
    CardComponent,
    CardComponent,
    UploadComponent,
    HeaderComponent,
  ],
  imports: [
    NgZorroModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserModule,
  ],
  exports: [
    LoginComponent,
    TableComponent,
    ChartComponent,
    CardComponent,
    UploadComponent,
    HeaderComponent,
  ],
})
export class ComponentModule {}
