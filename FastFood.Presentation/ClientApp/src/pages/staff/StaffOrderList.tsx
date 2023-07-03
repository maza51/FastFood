import { FC } from "react";
import { useFetchOrders } from "@/hooks/useFetchOrders";
import { OrderGrid } from "@/components/staff/OrderGrid/OrderGrid";
import { useHubContext } from "@/hooks/useHubContext";

export const StaffOrderList: FC = () => {
	const { orders, fetchData } = useFetchOrders({
		activeOnly: true,
		detailed: true,
	});

	const { connection } = useHubContext();

	if (connection) {
		connection.on("OrdersChanged", fetchData);
	}

	return <OrderGrid orders={orders} />;
};
