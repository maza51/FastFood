import { useEffect, useMemo, useState } from "react";
import { IOrder } from "@/types/IOrder";
import { OrderService } from "@/services/orderService";

interface IFetchOrdersOptions {
	activeOnly?: boolean;
	detailed?: boolean;
	pageIndex?: number;
	pageSize?: number;
}

export const useFetchOrders = (options: IFetchOrdersOptions = {}) => {
	const {
		activeOnly = false,
		detailed = false,
		pageIndex = 1,
		pageSize = 12,
	} = options;
	const [orders, setOrders] = useState<IOrder[]>([]);
	const [totalCount, setTotalCount] = useState<number>(0);
	const [loading, setLoading] = useState<boolean>(false);

	useEffect(() => {
		fetchData().then();
	}, [activeOnly, detailed, pageIndex, pageSize]);

	const fetchData = async () => {
		setLoading(true);
		try {
			let result;
			if (activeOnly && detailed) {
				result = await OrderService.getActiveDetailed();
			} else if (activeOnly) {
				result = await OrderService.getActive();
			} else if (detailed) {
				result = await OrderService.getDetailed();
			} else {
				result = await OrderService.getPaged(pageIndex, pageSize);
			}

			setOrders(result.data);
			setTotalCount(result.totalCount);
		} catch (error) {
			console.error(error);
		} finally {
			setLoading(false);
		}
	};

	const totalPages = useMemo(() => {
		return Math.ceil(totalCount / pageSize);
	}, [totalCount]);

	return { orders, totalCount, totalPages, fetchData, loading };
};
