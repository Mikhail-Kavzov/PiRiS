import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormControl, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { debounceTime, map, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { TokenPagination } from './token-correlation.types';
import { TokenCorrelationService } from './token-correlation.service';
import { TokenCorrelationDto, UpdateTokenCorrelationDto } from '../../../../api/api.client';
import { UpperCasePipe } from '@angular/common';

@Component({
    selector: 'token-correlation',
    templateUrl: './token-correlation.component.html',
    styleUrls: ['./token-correlation.component.scss'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations
})
export class TokenCorrelationComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    tokens: TokenCorrelationDto[];
    flashMessage: 'success' | 'error' | null = null;
    isLoading: boolean = false;
    pagination: TokenPagination;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    nullControl: FormControl = new FormControl(true);
    selectedToken: TokenCorrelationDto | null = null;
    selectedTokenForm: UntypedFormGroup;

    private _unsubscribeAll: Subject<any> = new Subject<any>();
    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseConfirmationService: FuseConfirmationService,
        private _formBuilder: UntypedFormBuilder,
        private _tokenService: TokenCorrelationService
    ) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {

        // Create the selected token form

        this.selectedTokenForm = this._formBuilder.group({
            coinGeckoApiId: ['',[Validators.required]]
        });

        this._tokenService.pagination$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagination: TokenPagination) => {

                // Update the pagination
                this.pagination = pagination;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Get the tokens

        this._tokenService.tokens$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((tokens: TokenCorrelationDto[]) => {

                this.tokens = tokens;

                this._changeDetectorRef.markForCheck();
            });

        // Subscribe to search input field value changes
        this.searchInputControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                debounceTime(300),
                switchMap((query) => {
                    this.closeDetails();
                    this.isLoading = true;
                    
                    return this._tokenService.getTokens(0, 10, query, this.nullControl.value);
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();

        this.nullControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                debounceTime(300),
                switchMap((isNull) => {
                    this.closeDetails();
                    this.isLoading = true;
                    let search = this.searchInputControl.value ?? '';
                    return this._tokenService.getTokens(0, 10, search, isNull);
                }),
                map(() => {
                    this.isLoading = false;
                })
            )
            .subscribe();   
    }

    /**
     * After view init
     */
    ngAfterViewInit(): void {
        if (this._paginator) {

            // Mark for check
            this._changeDetectorRef.markForCheck();

            // Get tokens if sort or page changes
            this._paginator.page.pipe(
                switchMap(() => {
                    this.closeDetails();
                    this.isLoading = true;
                    let search = this.searchInputControl.value ?? '';
                    return this._tokenService.getTokens(this._paginator.pageIndex, this._paginator.pageSize,
                        search, this.nullControl.value);
                }),
                map(() => {
                    this.isLoading = false;
                })
            ).subscribe();
        }
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------


    /**
     * Toggle token details
     *
     * @param id
     */
    toggleDetails(id: string): void {
        // If the token is already selected...
        if (this.selectedToken && this.selectedToken.id === id) {
            // Close the details
            this.closeDetails();
            return;
        }

        // Get the token by id
        this._tokenService.getTokenById(id)
            .subscribe((token) => {
                // Set the selected token
                this.selectedToken = token;
                // Fill the form
                this.selectedTokenForm.patchValue(token)

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * Close the details
     */
    closeDetails(): void {
        this.selectedToken = null;
    }

    /**
     * Update the selected token using the form data
     */
    updateSelectedToken(): void {
        // Get the token object
        const token = this.selectedToken;
        const updatedToken:UpdateTokenCorrelationDto = this.selectedTokenForm.getRawValue();
        token.coinGeckoApiId = updatedToken.coinGeckoApiId;

        // Update the token on the server
        this._tokenService.updateToken(token)
            .subscribe(() => {

                // Show a success message
                this.showFlashMessage('success');
            },
                () => {
                    this.showFlashMessage('error');
                });
    }

    /**
     * Show flash message
     */
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

    /**
     * Track by function for ngFor loops
     *
     * @param index
     * @param item
     */
    trackByFn(index: number, item: any): any {
        return item.id || index;
    }
}
