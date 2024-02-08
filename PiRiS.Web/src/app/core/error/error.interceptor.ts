/* eslint-disable @typescript-eslint/consistent-type-assertions */
/* eslint-disable @typescript-eslint/explicit-function-return-type */
/* eslint consistent-return: "error" */

import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ErrorsService } from './error.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor
{
    constructor(private _errorService: ErrorsService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
    {
        return next
            .handle(request)
            .pipe(catchError(this.handleError.bind(this)));
    }

    handleError(requestError: HttpErrorResponse)
    {
        if (requestError instanceof HttpErrorResponse && requestError.error instanceof Blob)
        {
            return this.parseErrorBlob(requestError);
        }

        this._errorService.showErrors(requestError);
        return throwError(requestError).toPromise();
    }

    parseErrorBlob(error: HttpErrorResponse): Promise<any>
    {
        return new Promise<any>((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = (e: Event) =>
            {
                try
                {
                    const errmsg = JSON.parse((<any>e.target).result);
                    this._errorService.showErrors(errmsg);

                    reject(
                        new HttpErrorResponse({
                            error: errmsg,
                            headers: error.headers,
                            status: error.status,
                            statusText: error.statusText,
                            url: error.url,
                        })
                    );
                }
                catch (er)
                {
                    reject(error);
                }
            };

            reader.onerror = (er) => {
                reject(er);
            };

            reader.readAsText(error.error);
        });
    }
}
