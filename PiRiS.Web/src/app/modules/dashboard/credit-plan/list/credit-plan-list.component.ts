import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { Subject, switchMap, takeUntil } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { CreditPlanDto, } from '../../../../../api/api.client';
import { Pagination } from '../../../../../types/pagination.types';
import { CreditPlanService } from '../credit-plan.service';

@Component({
    selector: 'credit-plan-list',
    templateUrl: './credit-plan-list.component.html',
    styleUrls: ['./credit-plan-list.component.scss'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations
})
export class CreditPlanListComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    plans: CreditPlanDto[];
    pagination: Pagination;


    private _unsubscribeAll: Subject<any> = new Subject<any>();
    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _creditPlanService: CreditPlanService)
    {
    }

    ngOnInit(): void {

        this._creditPlanService.pagination$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagination: Pagination) => {

                this.pagination = pagination;

                this._changeDetectorRef.markForCheck();
            });

        this._creditPlanService.plans$.pipe(takeUntil(this._unsubscribeAll))
            .subscribe((plans: CreditPlanDto[]) => {
            this.plans = plans;
            this._changeDetectorRef.markForCheck();
        })
    }

    ngAfterViewInit(): void {
        if (this._paginator) {

            this._changeDetectorRef.markForCheck();

            this._paginator.page.pipe(
                switchMap(() => {
                    return this._creditPlanService.getPlans(this._paginator.pageIndex, this._paginator.pageSize);
                })
            ).subscribe();
        }
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }


    trackByFn(index: number, item: CreditPlanDto): any {
        return item.creditPlanId || index;
    }
}
