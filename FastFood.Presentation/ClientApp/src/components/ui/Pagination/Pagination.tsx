import styles from "./Pagination.module.css";
import { FC } from "react";

interface IProps {
	currentPage: number;
	totalPages: number;
	maxVisiblePages?: number;
	onPageChange: (page: number) => void;
}

export const Pagination: FC<IProps> = (props: IProps) => {
	const previousPage =
		props.currentPage > 1 ? props.currentPage - 1 : props.currentPage;
	const nextPage =
		props.currentPage < props.totalPages
			? props.currentPage + 1
			: props.currentPage;

	const handleClick = (n: number) => {
		props.onPageChange(n);
	};

	const renderPageNumbers = () => {
		const maxVisiblePages =
			props.maxVisiblePages ?? Math.min(props.totalPages, 5);
		const halfVisiblePages = Math.floor(maxVisiblePages / 2);

		let startPage = Math.max(props.currentPage - halfVisiblePages, 1);
		let endPage = Math.min(
			props.currentPage + halfVisiblePages,
			props.totalPages
		);

		if (endPage - startPage < maxVisiblePages - 1) {
			startPage = Math.max(endPage - maxVisiblePages + 1, 1);
			endPage = Math.max(maxVisiblePages - startPage + 1, endPage);
		}

		const pageNumbers: JSX.Element[] = [];

		for (let i = startPage; i <= endPage; i++) {
			pageNumbers.push(
				<div
					className={
						props.currentPage === i
							? styles.selectedItem
							: styles.item
					}
					key={i}
					onClick={() => handleClick(i)}
				>
					{i}
				</div>
			);
		}

		return pageNumbers;
	};

	return (
		<div className={styles.pagination}>
			<small className={styles.item} onClick={() => handleClick(1)}>
				{"<<"}
			</small>
			<small
				className={styles.item}
				onClick={() => handleClick(previousPage)}
			>
				{"<"}
			</small>
			{renderPageNumbers()}
			<small
				className={styles.item}
				onClick={() => handleClick(nextPage)}
			>
				{">"}
			</small>
			<small
				className={styles.item}
				onClick={() => handleClick(props.totalPages)}
			>
				{">>"}
			</small>
		</div>
	);
};
