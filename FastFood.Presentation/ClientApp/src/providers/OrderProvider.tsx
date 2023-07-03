import { IOrder, OrderStatus } from "@/types/IOrder";
import { createContext, useEffect, useState } from "react";
import { OrderService } from "@/services/orderService";

interface IOrderContext {
	orders: IOrder[];
	updateStatus: (orderId: number, status: OrderStatus) => Promise<void>;
}

export const OrderContext = createContext<IOrderContext | undefined>(undefined);

export const OrderProvider = ({ children }) => {
	const [orders, setOrders] = useState<IOrder[]>([]);

	useEffect(() => {
		fetchData().then();
	}, []);

	const fetchData = async () => {
		try {
			const result = await OrderService.getActiveDetailed();
			setOrders(result.data);
		} catch (error) {
			console.error(error);
		}
	};

	const updateStatus = async (orderId: number, status: OrderStatus) => {
		const order = await OrderService.updateStatus(orderId, status);
		console.log(order);
		await fetchData();
	};

	return (
		<OrderContext.Provider
			value={{
				orders,
				updateStatus,
			}}
		>
			{children}
		</OrderContext.Provider>
	);
};
