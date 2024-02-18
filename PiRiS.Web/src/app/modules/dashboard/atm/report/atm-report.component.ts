import { Component, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { AtmReportDto } from '../../../../../api/api.client';

@Component({
    selector: 'atm-report',
    templateUrl: './atm-report.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class AtmReportComponent{

    @Input()
    report: AtmReportDto;

}
