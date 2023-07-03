import { useContext } from "react";
import { BasketContext } from "@/providers/BasketProvider";

export const useBasketContext = () => {
	const context = useContext(BasketContext);
	if (!context) throw new Error("basket context must be used");

	return context;
};
