import { OrderStatus } from "@/types/IOrder";

export const getOrderStatusText = (status: OrderStatus): string => {
	switch (status) {
		case OrderStatus.Processing:
			return "Сборка";
		case OrderStatus.Ready:
			return "Готов";
		case OrderStatus.Completed:
			return "Завершен";
		case OrderStatus.Cancelled:
			return "Отменен";
		default:
			throw new Error("Неизвестный статус заказа");
	}
};
