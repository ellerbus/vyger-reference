import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { FlashMessage, FlashMessageSeverity } from 'src/models/flash-message';
import { FlashMessageService } from './flash-message.service';

@Injectable({
    providedIn: 'root'
})
export class HttpErrorInterceptorService implements HttpInterceptor
{
    constructor(private flashMessageService: FlashMessageService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
    {
        return next.handle(request).pipe(catchError(this.handleError) as any);
    }

    private handleError = (err: any, caught: Observable<HttpEvent<any>>) =>
    {
        if (err instanceof HttpErrorResponse)
        {
            let options = {
                severity: FlashMessageSeverity.Danger,
                label: err.statusText,
                message: err.message
            };

            let msg = new FlashMessage(options);

            this.flashMessageService.messages.push(msg);
        }
        return of(err);
    }
}
