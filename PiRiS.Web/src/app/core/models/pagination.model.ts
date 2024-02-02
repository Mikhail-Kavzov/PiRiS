export class Pagination {
    pageIndex: number;
    pageSize: number;
  
    constructor(data: any = {}) {
        this.pageIndex = data.pageIndex;
        this.pageSize = data.pageSize;
    }
}