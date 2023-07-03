export interface IPagedList<T> {
	data: T[];
	pageIndex: number;
	pageSize: number;
	totalCount: number;
}
