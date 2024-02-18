import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { CreditService } from '../credit.service';
import { CreditScheduleDto } from '../../../../../api/api.client';
import { KeyValue } from '@angular/common';

@Component({
    selector: 'credit-schedule',
    templateUrl: './credit-schedule.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class CreditScheduleComponent implements OnInit, OnDestroy {

    private _unsubscribeAll: Subject<any> = new Subject<any>();
    schedule: CreditScheduleDto;

    constructor(private _creditService: CreditService) {
    }

    ngOnInit(): void {

        this._creditService.schedule$.pipe(takeUntil(this._unsubscribeAll))
            .subscribe((schedule) => {
                this.schedule = schedule;
                JSON.stringify(this.schedule.schedule);
            })
    }

    ngOnDestroy(): void {

        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    private onCompare(_left: KeyValue<any, any>, _right: KeyValue<any, any>): number {
        return -1;
    }
}
