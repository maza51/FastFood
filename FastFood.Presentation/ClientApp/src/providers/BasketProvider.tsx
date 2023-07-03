import { createContext, useEffect, useMemo, useState } from "react";
import { IBasketItem } from "@/types/IBasketItem";
import { IProduct } from "@/types/IProduct";

const STORAGE_KEY = "basket";

interface IBasketContext {
	items: IBasketItem[];
	itemsCount: number;
	itemsTotalPrice: number;
	createItem: (product: IProduct) => void;
	deleteItem: (ProductId: number) => void;
	changeItemCount: (productId: number, count: number) => void;
	clearItems: () => void;
}

export const BasketContext = createContext<IBasketContext | undefined>(
	undefined
);

export const BasketProvider = ({ children }) => {
	const [items, setItems] = useState<IBasketItem[]>([]);

	useEffect(() => {
		const itemsInLocalStorage = JSON.parse(
			localStorage.getItem(STORAGE_KEY) ?? "[]"
		);
		if (itemsInLocalStorage) setItems(itemsInLocalStorage);
	}, []);

	const itemsCount = useMemo<number>(() => {
		return items.reduce((accumulator, x) => {
			return accumulator + x.quantity;
		}, 0);
	}, [items]);

	const itemsTotalPrice = useMemo<number>(() => {
		return items.reduce((accumulator, x) => {
			return accumulator + x.product.unitPrice * x.quantity;
		}, 0);
	}, [items]);

	const setAndSaveItems = (items: IBasketItem[]) => {
		setItems(items);
		localStorage.setItem(STORAGE_KEY, JSON.stringify(items));
	};

	const createItem = (product: IProduct) => {
		const existItem = items.find((x) => x.product.id === product.id);

		if (existItem) {
			console.error(`item id ${product.id} exist in basket`);
			return;
		}

		const basketItem: IBasketItem = {
			product: product,
			quantity: 1,
		};

		setAndSaveItems([...items, basketItem]);
	};

	const deleteItem = (productId: number) => {
		setAndSaveItems(items.filter((x) => x.product.id !== productId));
	};

	const changeItemCount = (productId: number, count: number) => {
		if (count <= 0) {
			deleteItem(productId);
			return;
		}

		const item = items.find((x) => x.product.id === productId);

		if (!item) return;

		item.quantity = count;

		const changedItems = items.map((x) => {
			if (x.product.id === item.product.id) {
				x.quantity = item.quantity;
			}
			return x;
		});

		setAndSaveItems(changedItems);
	};

	const clearItems = () => {
		setAndSaveItems([]);
	};

	return (
		<BasketContext.Provider
			value={{
				items,
				itemsCount,
				itemsTotalPrice,
				createItem,
				deleteItem,
				changeItemCount,
				clearItems,
			}}
		>
			{children}
		</BasketContext.Provider>
	);
};
