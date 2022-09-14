
export class Pagination {

  public currentPage: number;
  public itemsPerPage: number;
  public totalItems: number;
  public totalpages: number;

}


export class PaginatedResult<T> {
  result: T;
  pagination: Pagination;
}


