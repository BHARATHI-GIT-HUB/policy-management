import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { NgZorroModule } from './NgZorro.module';
import { PagesModule } from './pages/pages.module';
import { IconsProviderModule } from './icons-provider.module';
import { NzLayoutModule, NzSiderComponent } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { ComponentModule } from './components/components.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  NzBreadCrumbComponent,
  NzBreadCrumbItemComponent,
} from 'ng-zorro-antd/breadcrumb';
import { NzButtonComponent, NzButtonModule } from 'ng-zorro-antd/button';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    PagesModule,
    NgZorroModule,
    IconsProviderModule,
    ComponentModule,
    ReactiveFormsModule,
    CommonModule,
    BrowserAnimationsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
