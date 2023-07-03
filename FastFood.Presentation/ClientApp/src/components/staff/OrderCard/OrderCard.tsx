import styles from "./OrderCard.module.css";
import { FC, useEffect, useState } from "react";
import { IOrder, OrderStatus } from "@/types/IOrder";
import { Button } from "@/components/ui/Button/Button";
import { getOrderStatusText } from "@/helpers/orderHelpers";
import { OrderService } from "@/services/orderService";
import { Timer } from "@/components/staff/OrderCard/Timer/Timer";

interface IProps {
	order: IOrder;
}

export const OrderCard: FC<IProps> = ({ order }) => {
	const [buttonStatus, setButtonStatus] = useState<boolean>(true);

	useEffect(() => {
		setButtonStatus(true);
	}, [order]);

	const updateOrderStatus = async (status: OrderStatus) => {
		try {
			setButtonStatus(false);
			await OrderService.updateStatus(order.id, status);
		} catch (error) {
			setButtonStatus(true);
			console.error(error);
		}
	};

	const button =
		order.status === OrderStatus.Ready ? (
			<Button
				style={{ width: "100%" }}
				onClick={() => updateOrderStatus(OrderStatus.Completed)}
				disabled={!buttonStatus}
			>
				Завершить
			</Button>
		) : (
			<Button
				style={{
					backgroundColor: "#c2c2c2",
					width: "100%",
				}}
				onClick={() => updateOrderStatus(OrderStatus.Ready)}
				disabled={!buttonStatus}
			>
				Готов
			</Button>
		);

	return (
		<div
			className={
				order.status === OrderStatus.Ready
					? styles.orderCardReady
					: styles.orderCard
			}
		>
			<div className={styles.header}>
				<div className={styles.buyerCode}>{order.buyerCode}</div>
				<div>
					<Timer date={new Date(order.createdAt)} />
				</div>
				<div
					className={
						order.status === OrderStatus.Processing
							? styles.statusProcessing
							: styles.statusReady
					}
				>
					{getOrderStatusText(order.status)}
				</div>
			</div>
			<div className={styles.body}>
				{order.orderItems?.map((x) => (
					<div key={x.id} className={styles.item}>
						<div className={styles.count}>
							{x.quantity} x &nbsp;
						</div>
						<div className={styles.name}>{x.productName}</div>
					</div>
				))}
			</div>
			<div className={styles.footer}>{button}</div>
		</div>
	);
};
