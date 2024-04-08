import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { provideEnvironmentNgxCurrency, NgxCurrencyInputMode } from 'ngx-currency';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideClientHydration(),
    provideAnimationsAsync(),
    provideHttpClient(),
    provideAnimations(),
    provideToastr(),
    provideHttpClient(withInterceptorsFromDi()),
    {
        provide:HTTP_INTERCEPTORS,
        useClass:JwtInterceptor,
        multi:true
    },
    provideEnvironmentNgxCurrency({
      align: "left",
      allowNegative: true,
      allowZero: true,
      decimal: ",",
      precision: 2,
      prefix: "",
      suffix: "",
      thousands: ".",
      nullable: true,
      min: null,
      max: null,
      inputMode: NgxCurrencyInputMode.Financial,
    })
  ]
};
