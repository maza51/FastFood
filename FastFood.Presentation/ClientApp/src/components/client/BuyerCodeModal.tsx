import { FC } from "react";
import { Modal } from "@/components/ui/Modal/Modal";

interface IProps {
	isOpen: boolean;
	onClose: () => void;
	buyerCode: string;
}

export const BuyerCodeModal: FC<IProps> = ({ isOpen, onClose, buyerCode }) => {
	return (
		<Modal isOpen={isOpen} onClose={onClose}>
			<div style={{ textAlign: "center", padding: "50px" }}>
				<h1>Заказ оформлен!</h1>
				<h3>Код получения:</h3>
				<div
					style={{
						fontSize: "60px",
						color: "#6ebd96",
						fontWeight: "bold",
					}}
				>
					{buyerCode}
				</div>
			</div>
		</Modal>
	);
};
