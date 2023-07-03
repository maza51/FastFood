import { FC, useMemo } from "react";
import { useFetchOrders } from "@/hooks/useFetchOrders";
import { OrderStatus } from "@/types/IOrder";
import styles from "./OrderBoard.module.css";
import { useHubContext } from "@/hooks/useHubContext";

export const OrderBoard: FC = () => {
	const { orders, fetchData } = useFetchOrders({ activeOnly: true });

	const ordersProcessing = useMemo(() => {
		return orders.filter((x) => x.status === OrderStatus.Processing);
	}, [orders]);

	const ordersReady = useMemo(() => {
		return orders.filter((x) => x.status === OrderStatus.Ready);
	}, [orders]);

	const { connection } = useHubContext();

	if (connection) {
		connection.on("OrdersChanged", fetchData);
	}

	return (
		<div className={styles.orderBoard}>
			<div className={styles.columnPrepare}>
				<div className={styles.header}>Готовятся</div>
				<div className={styles.body}>
					{ordersProcessing
						.map((x) => (
							<div className={styles.item} key={x.id}>
								{x.buyerCode}
							</div>
						))
						.slice(0, 14)}
				</div>
			</div>
			<div className={styles.columnReady}>
				<div className={styles.header}>Готовы</div>
				<div className={styles.body}>
					{ordersReady
						.map((x) => (
							<div className={styles.item} key={x.id}>
								{x.buyerCode}
							</div>
						))
						.slice(0, 14)}
				</div>
			</div>
		</div>
	);
};
