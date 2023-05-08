import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { NavComponent } from './nav/nav.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { HomeComponent } from './home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './_modules/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { RegisterComponent } from './register/register.component';
import { TextInputComponent } from './text-input/text-input.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { TestsComponent } from './tests/tests.component';
import { TestCardComponent } from './test-card/test-card.component';
import { TestViewComponent } from './test-view/test-view.component';
import { QuestionViewComponent } from './question-view/question-view.component';

@NgModule({
  declarations: [
    AppComponent,
    HasRoleDirective,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    TextInputComponent,
    AdminPanelComponent,
    RolesModalComponent,
    UserManagementComponent,
    TestsComponent,
    TestCardComponent,
    TestViewComponent,
    QuestionViewComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
