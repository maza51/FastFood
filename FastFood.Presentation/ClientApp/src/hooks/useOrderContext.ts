import { useContext } from "react";
import { OrderContext } from "@/providers/OrderProvider";

export const useOrderContext = () => {
	const context = useContext(OrderContext);
	if (!context) throw new Error("order context must be used");

	return context;
};
