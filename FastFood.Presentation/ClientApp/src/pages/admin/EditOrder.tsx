import { useParams } from "react-router";
import { useEffect, useState } from "react";
import { OrderStatusForm } from "@/components/admin/OrderStatusForm/OrderStatusForm";
import { IOrder, OrderStatus } from "@/types/IOrder";
import { OrderService } from "@/services/orderService";

export const EditOrder = () => {
	const { id } = useParams();

	const [order, setOrder] = useState<IOrder | null>(null);
	const [tooltip, setTooltip] = useState<JSX.Element>(<></>);

	useEffect(() => {
		fetchData().then();
	}, [id]);

	const fetchData = async () => {
		try {
			const result = await OrderService.getById(Number(id));
			setOrder(result);
		} catch (error) {
			console.log(error);
		}
	};

	const handleSubmit = async (status: OrderStatus) => {
		try {
			await OrderService.updateStatus(Number(id), status);
			setTooltip(<div style={{ color: "#6ebd96" }}>Статус изменен!</div>);
		} catch (error) {
			console.error(error);
			setTooltip(
				<div style={{ color: "#fd826e" }}>Ошибка на сервере!</div>
			);
		}
	};

	return order && (
		<OrderStatusForm
			tooltip={tooltip}
			onSubmit={handleSubmit}
			order={order}
		/>
	);
};
