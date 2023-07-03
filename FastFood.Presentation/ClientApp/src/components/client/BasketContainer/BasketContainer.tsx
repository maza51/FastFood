import { FC, useState } from "react";
import { BasketModal } from "@/components/client/BasketContainer/BasketModal/BasketModal";
import { BottomPanel } from "@/components/client/BasketContainer/BottomPanel/BottomPanel";
import { IOrder } from "@/types/IOrder";
import { BuyerCodeModal } from "@/components/client/BuyerCodeModal";

export const BasketContainer: FC = () => {
	const [isOpenModal, setIsOpenModal] = useState<boolean>(false);
	const [createdOrder, setCreatedOrder] = useState<IOrder | null>(null);

	const openModal = () => {
		setIsOpenModal(true);
	};

	const closeModal = () => {
		setIsOpenModal(false);
		setCreatedOrder(null);
	};

	const handleCreateOrder = (order: IOrder) => {
		setCreatedOrder(order);
	};

	return (
		<>
			{createdOrder ? (
				<BuyerCodeModal
					isOpen={isOpenModal}
					onClose={closeModal}
					buyerCode={createdOrder.buyerCode}
				/>
			) : (
				<BasketModal
					isOpen={isOpenModal}
					onClose={closeModal}
					onCreateOrder={handleCreateOrder}
				/>
			)}
			<BottomPanel onClickShow={openModal} />
		</>
	);
};
