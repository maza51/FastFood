import { useSearchParams, useNavigate } from "react-router-dom";
import { useFetchProducts } from "@/hooks/useFetchProducts";
import { Pagination } from "@/components/ui/Pagination/Pagination";
import { ProductTable } from "@/components/admin/tables/ProductTable";
import { FC } from "react";

export const ProductList: FC = () => {
	const [searchParams] = useSearchParams();
	const pageIndex = Number(searchParams.get("pageIndex")) || 1;

	const { products, totalPages, fetchData } = useFetchProducts({
		pageIndex: pageIndex,
	});

	const navigate = useNavigate();

	return (
		<>
			<ProductTable products={products} onChanged={fetchData} />
			<Pagination
				currentPage={pageIndex}
				totalPages={totalPages}
				onPageChange={(page: number) =>
					navigate(`../products?pageIndex=${page}`)
				}
			/>
		</>
	);
};
