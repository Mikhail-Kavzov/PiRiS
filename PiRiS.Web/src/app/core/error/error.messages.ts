import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class MessagesService
{
    constructor(public _snackBarService: MatSnackBar) {}

    public openError(message, action = 'OK', customOptions?): void
    {
        const options = customOptions || {
            verticalPosition: 'top',
            horizontalPosition: 'right',
            panelClass: ['error-dialog'],
            duration: 7000,
        };

        this._snackBarService.open(message, action, options);
    }
}
