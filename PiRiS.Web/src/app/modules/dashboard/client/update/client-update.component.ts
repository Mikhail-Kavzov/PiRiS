import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Subject, takeUntil } from 'rxjs';
import { CitizenshipDto, CityDto, ClientAdditionalsDto, ClientDto, DisabilityDto, FamilyStatusDto } from '../../../../../api/api.client';
import { Patterns } from '../../../../core/enums/patterns.enum';
import { ClientService } from '../client.service';

@Component({
    selector: 'client-update',
    templateUrl: './client-update.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations,
})
export class ClientUpdateComponent implements OnInit, OnDestroy {
    clientForm: FormGroup;

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    updatedClient: ClientDto;

    disabilities: DisabilityDto[];
    cities: CityDto[];
    citizenships: CitizenshipDto[];
    familyStatuses: FamilyStatusDto[];

    constructor(
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _clientService: ClientService,
    ) {
    }

    ngOnInit(): void {

        this.clientForm = this._formBuilder.group({
            surname: ['', [Validators.required, Validators.pattern(Patterns.ClientNames)]],
            firstName: ['', [Validators.required, Validators.pattern(Patterns.ClientNames)]],
            lastName: ['', [Validators.required, Validators.pattern(Patterns.ClientNames)]],
            passportSeries: ['', [Validators.required, Validators.pattern(Patterns.PassportSeries)]],
            passportNumber: ['', [Validators.required, Validators.pattern(Patterns.PassportNumber)]],
            issuedBy: ['', Validators.required],
            identificationNumber: ['', [Validators.required, Validators.pattern(Patterns.IdentificationNumber)]],
            placeOfBirth: ['', Validators.required],
            locationAddress: ['', Validators.required],
            cityId: ['', Validators.required],
            registrationAddress: ['', Validators.required],
            citizenshipId: ['', Validators.required],
            disabilityId: ['', Validators.required],
            familyStatusId: ['', Validators.required],
            email: ['', Validators.email],
            homePhone: ['', Validators.pattern(Patterns.Phone)],
            mobilePhone: ['', Validators.pattern(Patterns.Phone)],
            company: [''],
            jobTitle: [''],
            isPensioner: [false],
            monthIncome: [null, Validators.min(0)],
            dateOfBirth: [new Date(), Validators.required],
            dateOfIssue: ['', Validators.required],

        })

        this._clientService.client$.pipe(
            takeUntil(this._unsubscribeAll)
        ).subscribe(
            (client: ClientDto) => {
                this.updatedClient = client;
            }
        );


        this._clientService.additionals$.pipe(takeUntil(this._unsubscribeAll))
            .subscribe(
                (result: ClientAdditionalsDto) => {
                    this.disabilities = result.disabilities;
                    this.citizenships = result.citizenships;
                    this.cities = result.cities;
                    this.familyStatuses = result.familyStatuses;
                }

            )

    }


    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    update() {
        if (this.clientForm.invalid) {
            return;
        }

        let client = new ClientDto();

        client.clientId = this.updatedClient.clientId;
        client.passportNumber = this.clientForm.get('passportNumber').value;
        client.citizenshipId = this.clientForm.get('citizenshipId').value;
        client.passportSeries = this.clientForm.get('passportSeries').value;
        client.cityId = this.clientForm.get('cityId').value;
        client.disabilityId = this.clientForm.get('disabilityId').value;
        client.identificationNumber = this.clientForm.get('identificationNumber').value;
        client.familyStatusId = this.clientForm.get('familyStatusId').value;
        client.issuedBy = this.clientForm.get('issuedBy').value;
        client.company = this.clientForm.get('company').value;
        client.dateOfBirth = this.clientForm.get('dateOfBirth').value;
        client.dateOfIssue = this.clientForm.get('dateOfIssue').value;
        let email = this.clientForm.get('email').value;
        client.email = email ? email : null;
        client.isPensioner = this.clientForm.get('isPensioner').value;
        client.homePhone = this.clientForm.get('homePhone').value;
        let mobilePhone = this.clientForm.get('mobilePhone').value;
        client.mobilePhone = mobilePhone ? mobilePhone : null;
        client.locationAddress = this.clientForm.get('locationAddress').value;
        client.registrationAddress = this.clientForm.get('registrationAddress').value;
        client.monthIncome = this.clientForm.get('monthIncome').value;
        client.placeOfBirth = this.clientForm.get('placeOfBirth').value;
        client.surname = this.clientForm.get('surname').value;
        client.firstName = this.clientForm.get('firstName').value;
        client.lastName = this.clientForm.get('lastName').value;
        client.jobTitle = this.clientForm.get('jobTitle').value;

        this._clientService.updateClient(client)
            .subscribe(
                () => {
                    this._router.navigateByUrl('/client/list');
                })
    }


}
