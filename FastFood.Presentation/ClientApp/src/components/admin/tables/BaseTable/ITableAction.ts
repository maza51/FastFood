export interface ITableAction<T> {
	element: JSX.Element;
	action: (object: T) => void;
}
