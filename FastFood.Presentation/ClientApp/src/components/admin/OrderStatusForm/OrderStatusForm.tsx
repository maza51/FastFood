import styles from "./OrderStatusForm.module.css";
import { Button } from "@/components/ui/Button/Button";
import { FC, useEffect, useState } from "react";
import { IOrder, OrderStatus } from "@/types/IOrder";

interface IProps {
	order: IOrder;
	tooltip: JSX.Element;
	onSubmit: (status: OrderStatus) => void;
}

export const OrderStatusForm: FC<IProps> = ({ order, tooltip, onSubmit }) => {
	const [status, setStatus] = useState<OrderStatus>(0);

	useEffect(() => {
		if (order) {
			setStatus(order.status);
		}
	}, [order]);

	const handleSubmit = (e) => {
		e.preventDefault();

		onSubmit(status);
	};

	const statuses = Object.values(OrderStatus).filter(
		(x) => !isNaN(parseInt(x.toString()))
	);

	return (
		<div className={styles.orderStatusForm}>
			<h3>Изминение статуса заказа:</h3>
			<form>
				<label>
					Status:
					<select
						onChange={(e) => setStatus(parseInt(e.target.value))}
						value={status}
					>
						{statuses.map((status) => (
							<option key={status} value={status}>
								{OrderStatus[status]}
							</option>
						))}
					</select>
				</label>
				<br />
				<div className={styles.footer}>
					<Button type="submit" onClick={handleSubmit}>
						Отправить
					</Button>
					<div className={styles.tooltip}>{tooltip}</div>
				</div>
			</form>
		</div>
	);
};
