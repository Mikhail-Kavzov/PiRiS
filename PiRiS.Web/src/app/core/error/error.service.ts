import { Injector, Injectable } from '@angular/core';
import { MessagesService } from './error.messages';

@Injectable()
export class ErrorsService
{
    constructor(private _injector: Injector, private _messagesService: MessagesService) { }

    showErrors(error): void
    {
        if (error.isUnconfirmed) {
            return;
        }
        if (error && error.errors)
        {
            this.showValidationErrors(error);
        }
        else
        {
            const defaultMessage = 'Something went wrong!';
            const message = error
                ? error.title || error.message || error.status || defaultMessage
                : defaultMessage;
            this._messagesService.openError(message);
        }
    }

    showValidationErrors(error): void
    {
        const errorFields = Object.keys(error.errors);

        errorFields.forEach((field) => {
            this._messagesService.openError(error.errors[field].join('<br>'));
        });
    }
}
