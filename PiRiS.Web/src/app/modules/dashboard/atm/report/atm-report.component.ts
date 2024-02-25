import { Component, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { AtmReportDto } from '../../../../../api/api.client';
import { PrintService } from '../../print/print.service';

@Component({
    selector: 'atm-report',
    templateUrl: './atm-report.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class AtmReportComponent{

    @Input()
    report: AtmReportDto;

    constructor(private _printService: PrintService) {

    }

    print(){
        let date = this.report.operationDate.toDateString();
        let sum = this.report.sum.toFixed(2);
        let content = `Sum: ${sum} ${this.report.currencyName} \nDate: ${date}\n
        Card: ${this.report.creditCardNumber}\n Operation Name: ${this.report.operationName}`;
        this._printService.print(content);
    }
}
