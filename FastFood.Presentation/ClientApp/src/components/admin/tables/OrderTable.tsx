import { IOrder, OrderStatus } from "@/types/IOrder";
import { FC } from "react";
import { BaseTable } from "@/components/admin/tables/BaseTable/BaseTable";
import { ITableAction } from "@/components/admin/tables/BaseTable/ITableAction";
import { useNavigate } from "react-router-dom";

interface IOrderTableItem {
	id: number;
	buyerCode: string;
	status: string;
	createdAt: string;
}

interface IProps {
	orders: IOrder[];
}

export const OrderTable: FC<IProps> = ({ orders }) => {
	const navigate = useNavigate();

	const orderTableItems: IOrderTableItem[] = [];
	for (const order of orders) {
		orderTableItems.push({
			id: order.id,
			buyerCode: order.buyerCode,
			status: OrderStatus[order.status],
			createdAt: new Date(order.createdAt).toLocaleString(),
		});
	}

	const actions: ITableAction<IOrderTableItem>[] = [
		{
			element: <div style={{ color: "green" }}>edit</div>,
			action: (order) => {
				navigate(`../update-order-status/${order.id}`);
			},
		},
	];

	return (
		<BaseTable
			headerItems={["Id", "Код покупателя", "Статус", "Дата оформления"]}
			objects={orderTableItems}
			actions={actions}
		/>
	);
};
