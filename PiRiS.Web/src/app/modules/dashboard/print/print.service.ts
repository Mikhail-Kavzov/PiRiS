import { Injectable } from '@angular/core';
import * as pdfMake from 'pdfmake/build/pdfMake';
import * as  pdfFonts from 'pdfmake/build/vfs_fonts';

(pdfMake as any).vfs = pdfFonts.pdfMake.vfs;

@Injectable({
    providedIn: 'root'
})
export class PrintService {

 print (content: string){
    let docDefinition = {
        content: content,
    };
    pdfMake.createPdf(docDefinition).open();
 }

}
