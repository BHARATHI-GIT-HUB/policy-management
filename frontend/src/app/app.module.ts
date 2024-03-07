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
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  NzBreadCrumbComponent,
  NzBreadCrumbItemComponent,
} from 'ng-zorro-antd/breadcrumb';
import { NzButtonComponent, NzButtonModule } from 'ng-zorro-antd/button';
import { HttpClientModule } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import fr from '@angular/common/locales/fr';
import { provideNzI18n, en_US } from 'ng-zorro-antd/i18n';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from '../environments/environment';
import { AuthenticationRoutingModule } from './authentication/authentication-routing.module';
import { AuthModule } from './authentication/authentication.module';
import {
  AuthInterceptor,
  AuthInterceptorProvider,
} from './helpers/auth.interceptor';

registerLocaleData(en);
registerLocaleData(fr);

export function tokenGetter() {
  return localStorage.getItem('jwt');
}
@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    PagesModule,
    NgZorroModule,
    AuthModule,
    IconsProviderModule,
    ComponentModule,
    ReactiveFormsModule,
    CommonModule,
    BrowserAnimationsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: [environment.apiurl],
        disallowedRoutes: [],
      },
    }),
  ],
  providers: [provideNzI18n(en_US)],
  bootstrap: [AppComponent],
})
export class AppModule {}
