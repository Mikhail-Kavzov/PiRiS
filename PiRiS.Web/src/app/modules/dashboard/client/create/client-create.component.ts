import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseValidators } from '@fuse/validators';

@Component({
    selector: 'client-create',
    templateUrl: './client-create.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ClientCreateComponent implements OnInit {
    createForm: FormGroup;

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _formBuilder: FormBuilder,
        private _router: Router
    ) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    ngOnInit(): void {

    }


}
