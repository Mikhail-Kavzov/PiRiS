export class TokenPagination
{
    length: number;
    size: number;
    page: number;

    constructor(page: number, size: number, length: number) {
        this.page = page;
        this.size = size;
        this.length = length;
    }
}
