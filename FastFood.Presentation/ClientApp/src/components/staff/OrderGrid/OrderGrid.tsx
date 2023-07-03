import styles from "./OrderGrid.module.css";
import { FC } from "react";
import { IOrder } from "@/types/IOrder";
import { OrderCard } from "@/components/staff/OrderCard/OrderCard";

interface IProps {
	orders: IOrder[];
}

export const OrderGrid: FC<IProps> = ({ orders }) => {
	return (
		<div className={styles.orderGrid}>
			<div className={styles.gridContainer}>
				{orders.map((x) => (
					<OrderCard key={x.id} order={x} />
				))}
			</div>
		</div>
	);
};
