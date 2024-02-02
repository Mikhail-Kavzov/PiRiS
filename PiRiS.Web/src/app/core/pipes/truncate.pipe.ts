import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
 name: 'truncate'
})

export class TruncatePipe implements PipeTransform 
{
    transform(value: string, args: any[]): string 
    {
        const limit = args.length > 0 
            ? parseInt(args[0], 10) 
            : 20;

        const trail = args.length > 1 
            ? args[1] : 
            '...';

        const center = args.length > 2
            ? args[2] : 
            false;

        if (center) 
        {
            if (value.length > limit) 
            {
                let firstSymbolsN = limit / 3 * 2;
                let lastSymbolsN = limit - firstSymbolsN;

                let firstSymbols = value.slice(0, firstSymbolsN);
                let lastSymbols = value.slice(-lastSymbolsN);

                return firstSymbols + trail + lastSymbols;
            } 
            else 
            {
                return value;
            }
        } 
        else 
        {
            if (value.length > limit) 
            {
                return  value.substring(0, limit) + trail;
            } 
            else 
            {
                return value;
            }
        }
   }
}