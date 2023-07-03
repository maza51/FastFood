import styles from "./ProductGrid.module.css";
import { FC, useEffect, useState } from "react";
import { ProductService } from "@/services/productService";
import { IProduct } from "@/types/IProduct";
import { IPagedList } from "@/types/IPagedList";
import { useInView } from "react-intersection-observer";
import { ProductCard } from "@/components/client/ProductCard/ProductCard";

interface IProps {
	categoryId: number;
}

export const ProductGrid: FC<IProps> = ({ categoryId }) => {
	const { ref, inView } = useInView({
		threshold: 0,
	});

	const [products, setProducts] = useState<IProduct[]>([]);
	const [pageIndex, setPageIndex] = useState<number>(1);
	const [isInit, setIsInit] = useState<boolean>(false);

	useEffect(() => {
		setPageIndex(1);
		fetchData(1, 12).then();
	}, [categoryId]);

	useEffect(() => {
		fetchData(pageIndex, 12).then();
	}, [pageIndex]);

	useEffect(() => {
		if (inView && isInit) {
			setPageIndex(pageIndex + 1);
		}
	}, [inView, isInit]);

	const fetchData = async (pageIndex: number, pageSize: number) => {
		try {
			let result: IPagedList<IProduct>;
			if (categoryId > 0) {
				result = await ProductService.getPagedByCategory(
					categoryId,
					pageIndex,
					pageSize
				);
			} else {
				result = await ProductService.getNew(pageIndex, pageSize);
			}
			if (pageIndex === 1) {
				setProducts(result.data);
			} else {
				setProducts([...products, ...result.data]);
			}

			setTimeout(() => {
				setIsInit(true);
			});
		} catch (error) {
			console.error(error);
		}
	};

	return (
		<div className={styles.productGrid}>
			<div className={styles.gridContainer}>
				{products.map((product) => (
					<ProductCard
						key={product.id}
						product={product}
					></ProductCard>
				))}
			</div>
			<div className={styles.paginateTarget} ref={ref as any}></div>
		</div>
	);
};
