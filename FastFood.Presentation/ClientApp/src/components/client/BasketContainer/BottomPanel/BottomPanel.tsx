import styles from "./BottomPanel.module.css";
import { FC } from "react";
import { useBasketContext } from "@/hooks/useBasketContext";
import { Button } from "@/components/ui/Button/Button";

interface IProps {
	onClickShow: () => void;
}

export const BottomPanel: FC<IProps> = ({ onClickShow }) => {
	const context = useBasketContext();

	return context.itemsCount ? (
		<div className={styles.bottomPanel}>
			<div className={styles.text}>
				Выбрано {context.itemsCount} товара на сумму{" "}
				{context.itemsTotalPrice}р.
			</div>
			<Button onClick={onClickShow}>Посмотреть</Button>
		</div>
	) : null;
};
