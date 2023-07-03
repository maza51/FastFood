import { useSearchParams, useNavigate } from "react-router-dom";
import { Pagination } from "@/components/ui/Pagination/Pagination";
import { FC } from "react";
import { useFetchOrders } from "@/hooks/useFetchOrders";
import { OrderTable } from "@/components/admin/tables/OrderTable";

export const OrderList: FC = () => {
	const [searchParams] = useSearchParams();
	const pageIndex = Number(searchParams.get("pageIndex")) || 1;

	const { orders, totalPages } = useFetchOrders({ pageIndex: pageIndex });

	const navigate = useNavigate();

	return (
		<>
			<OrderTable orders={orders} />
			<Pagination
				currentPage={pageIndex}
				totalPages={totalPages}
				onPageChange={(page: number) =>
					navigate(`../orders?pageIndex=${page}`)
				}
			/>
		</>
	);
};
