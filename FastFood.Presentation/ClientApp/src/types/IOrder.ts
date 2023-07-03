import { IOrderItem } from "@/types/IOrderItem";

export interface IOrder {
	id: number;
	buyerCode: string;
	status: OrderStatus;
	createdAt: Date;

	orderItems?: IOrderItem[];
}

export enum OrderStatus {
	Processing,
	Ready,
	Completed,
	Cancelled,
}
