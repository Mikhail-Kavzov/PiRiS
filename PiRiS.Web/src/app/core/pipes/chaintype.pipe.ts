import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
 name: 'chaintype'
})

export class ChainPipe implements PipeTransform 
{
    transform(value: string): string 
    {
        return value.replace("x", "×");
    }
}