import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { TransactionDtoPaginationList} from '../../../../../api/api.client';
import { TransactionService } from '../transaction.service';

@Injectable()
export class TransactionListResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _transactionService: TransactionService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<TransactionDtoPaginationList> {
        return this._transactionService.getTransactions(0, 10);
    }
}
