import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { debounceTime, Subject, switchMap, takeUntil } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { ClientViewDto, } from '../../../../../api/api.client';
import { Pagination } from '../../../../../types/pagination.types';
import { ClientService } from '../client.service';

@Component({
    selector: 'client-list',
    templateUrl: './client-list.component.html',
    styleUrls: ['./client-list.component.scss'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations
})
export class ClientListComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    clients: ClientViewDto[];
    flashMessage: 'success' | 'error' | null = null;
    pagination: Pagination;

    searchInputControl: UntypedFormControl = new UntypedFormControl();
    sortByControl: UntypedFormControl = new UntypedFormControl();
    sortDirectionControl: UntypedFormControl = new UntypedFormControl();

    selectedClient: ClientViewDto | null = null;

    private _unsubscribeAll: Subject<any> = new Subject<any>();
    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _clientService: ClientService
    ) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {

        this._clientService.pagination$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagination: Pagination) => {

                this.pagination = pagination;

                this._changeDetectorRef.markForCheck();
            });

        this._clientService.clients$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((clients: ClientViewDto[]) => {

                this.clients = clients;

                this._changeDetectorRef.markForCheck();
            });

        this.searchInputControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                debounceTime(300),
                switchMap((query) => {
                    this.closeDetails();
                    let sortDirection = this.sortDirectionControl.value;
                    let sortField = this.sortByControl.value;

                    return this._clientService.getClients(0, this._paginator.pageSize, query, sortDirection, sortField);
                })
            )
            .subscribe();

        this.sortByControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                switchMap((sortField) => {
                    this.closeDetails();

                    let search = this.searchInputControl.value;
                    let sortDirection = this.sortDirectionControl.value;

                    return this._clientService.getClients(0, this._paginator.pageSize, search, sortDirection, sortField);
                })
            )
            .subscribe();

        this.sortDirectionControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                switchMap((sortDirection) => {
                    this.closeDetails();

                    let search = this.searchInputControl.value;
                    let sortField = this.sortByControl.value;

                    return this._clientService.getClients(0, this._paginator.pageSize, search, sortDirection, sortField);
                })
            )
            .subscribe();
    }

    ngAfterViewInit(): void {
        if (this._paginator) {

            this._changeDetectorRef.markForCheck();

            this._paginator.page.pipe(
                switchMap(() => {
                    this.closeDetails();

                    let sortDirection = this.sortDirectionControl.value;
                    let sortField = this.sortByControl.value;
                    let search = this.searchInputControl.value ?? '';

                    return this._clientService.getClients(this._paginator.pageIndex, this._paginator.pageSize, search, sortDirection, sortField);
                })
            ).subscribe();
        }
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    toggleDetails(client: ClientViewDto): void {

        if (this.selectedClient && this.selectedClient.clientId === client.clientId) {
            // Close the details
            this.closeDetails();
            return;
        }
        this.selectedClient = client;
        this._changeDetectorRef.markForCheck();
    }

    deleteClient() {
        if (this.selectedClient) {
            let clientId = this.selectedClient.clientId;

            this._clientService.deleteClient(clientId).subscribe(
                () => {
                    let index = this.clients.indexOf(this.selectedClient);
                    this.clients.splice(index, 1);

                    this.closeDetails();
                    this.showFlashMessage('success');
                },
                () => {
                    this.showFlashMessage('error');
                }
            )
        }
    }

    closeDetails(): void {
        this.selectedClient = null;
    }

    showFlashMessage(type: 'success' | 'error'): void {
        // Show the message
        this.flashMessage = type;

        // Mark for check
        this._changeDetectorRef.markForCheck();

        // Hide it after 3 seconds
        setTimeout(() => {

            this.flashMessage = null;

            // Mark for check
            this._changeDetectorRef.markForCheck();
        }, 3000);
    }

    trackByFn(index: number, item: ClientViewDto): any {
        return item.clientId || index;
    }
}
