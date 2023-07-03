import styles from "./BasketModal.module.css";
import { FC } from "react";
import { Button } from "@/components/ui/Button/Button";
import { useBasketContext } from "@/hooks/useBasketContext";
import { ICreateOrderModel, OrderService } from "@/services/orderService";
import { IOrder, OrderStatus } from "@/types/IOrder";
import { Modal } from "@/components/ui/Modal/Modal";
import { Item } from "@/components/client/BasketContainer/BasketModal/Item/Item";

interface IProps {
	isOpen: boolean;
	onClose: () => void;
	onCreateOrder: (order: IOrder) => void;
}

export const BasketModal: FC<IProps> = ({ isOpen, onClose, onCreateOrder }) => {
	const context = useBasketContext();

	const createOrder = async () => {
		const createOrderModel: ICreateOrderModel = {
			status: OrderStatus.Processing,
			orderItems: context.items.map((x) => ({
				productId: x.product.id,
				productName: x.product.name,
				quantity: x.quantity,
				unitPrice: x.product.unitPrice,
			})),
		};

		try {
			const order = await OrderService.create(createOrderModel);
			context.clearItems();
			onCreateOrder(order);
		} catch (error) {
			console.log(error);
		}
	};

	return (
		<Modal isOpen={isOpen} onClose={onClose}>
			<div className={styles.header}>
				<h2>Корзина</h2>
			</div>
			<div className={styles.body}>
				{context.items.map((x) => (
					<Item key={x.product.id} basketItem={x} />
				))}
			</div>
			<div className={styles.footer}>
				<div>Всего к оплате {context.itemsTotalPrice}р.</div>
				<Button
					onClick={createOrder}
					disabled={context.itemsCount <= 0}
				>
					Оформить заказ
				</Button>
			</div>
		</Modal>
	);
};
