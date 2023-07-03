import { useEffect, useMemo, useState } from "react";
import { IProduct } from "@/types/IProduct";
import { ProductService } from "@/services/productService";
import { IPagedList } from "@/types/IPagedList";

interface IFetchProductsOptions {
	pageIndex?: number;
	pageSize?: number;
	categoryId?: number;
}

export const useFetchProducts = (options: IFetchProductsOptions = {}) => {
	const { pageIndex = 1, pageSize = 12, categoryId = 0 } = options;
	const [products, setProducts] = useState<IProduct[]>([]);
	const [totalCount, setTotalCount] = useState<number>(0);
	const [loading, setLoading] = useState<boolean>(false);

	useEffect(() => {
		fetchData().then();
	}, [pageIndex, pageSize, categoryId]);

	const fetchData = async () => {
		setLoading(true);
		try {
			let result: IPagedList<IProduct>;
			if (categoryId > 0) {
				result = await ProductService.getPagedByCategory(
					categoryId,
					pageIndex,
					pageSize
				);
			} else {
				result = await ProductService.getPaged(pageIndex, pageSize);
			}

			setProducts(result.data);
			setTotalCount(result.totalCount);
		} catch (error) {
			console.error(error);
		} finally {
			setLoading(false);
		}
	};

	const totalPages = useMemo(() => {
		return Math.ceil(totalCount / pageSize);
	}, [totalCount]);

	return { products, totalCount, totalPages, fetchData, loading };
};
