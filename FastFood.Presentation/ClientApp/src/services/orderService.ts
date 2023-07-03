import { IOrder } from "@/types/IOrder";
import axiosBase from "@/api/axiosBase";
import { IPagedList } from "@/types/IPagedList";

export const OrderService = {
	async getPaged(
		pageIndex: number,
		pageSize: number
	): Promise<IPagedList<IOrder>> {
		const response = await axiosBase.get<IPagedList<IOrder>>("orders", {
			params: {
				pageIndex,
				pageSize,
			},
		});
		return response.data as IPagedList<IOrder>;
	},

	async getDetailed(): Promise<IPagedList<IOrder>> {
		const response = await axiosBase.get<IPagedList<IOrder>>(
			"orders/detailed"
		);
		return response.data as IPagedList<IOrder>;
	},

	async getActive(): Promise<IPagedList<IOrder>> {
		const response = await axiosBase.get<IPagedList<IOrder>>(
			"orders/active"
		);
		return response.data as IPagedList<IOrder>;
	},

	async getActiveDetailed(): Promise<IPagedList<IOrder>> {
		const response = await axiosBase.get<IPagedList<IOrder>>(
			"orders/active-detailed"
		);
		return response.data as IPagedList<IOrder>;
	},

	async getById(id: number): Promise<IOrder> {
		const response = await axiosBase.get<IOrder>(`orders/${id}`);
		return response.data as IOrder;
	},

	async create(order: ICreateOrderModel): Promise<IOrder> {
		const response = await axiosBase.post<IOrder>("orders", order);
		return response.data as IOrder;
	},

	async updateStatus(orderId: number, status: number): Promise<IOrder> {
		const response = await axiosBase.patch<IOrder>(
			`orders/${orderId}/status`,
			{ status }
		);
		return response.data as IOrder;
	},
};

export interface ICreateOrderModel {
	status: number;
	orderItems?: ICreateOrderItemModel[];
}

export interface ICreateOrderItemModel {
	productId: number;
	productName: string;
	quantity: number;
	unitPrice: number;
}
