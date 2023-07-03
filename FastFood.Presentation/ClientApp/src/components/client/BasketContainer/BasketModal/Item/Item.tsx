import styles from "./Item.module.css";
import { ButtonCounter } from "@/components/ui/ButtonCounter/ButtonCounter";
import { IBasketItem } from "@/types/IBasketItem";
import { FC } from "react";
import { useBasketContext } from "@/hooks/useBasketContext";

interface IProps {
	basketItem: IBasketItem;
}

export const Item: FC<IProps> = ({ basketItem }) => {
	const context = useBasketContext();

	// eslint-disable-next-line @typescript-eslint/no-empty-function
	const handleAdd = () => {};

	const handleChangeCount = (count: number) => {
		context.changeItemCount(basketItem.product.id, count);
	};

	const imageUrl = import.meta.env.VITE_APP_IMAGE_URL;

	return (
		<div className={styles.item}>
			<div className={styles.content}>
				<img
					className={styles.image}
					src={imageUrl + basketItem.product.imagePath}
					alt={"wtf"}
				/>
				<div>{basketItem.product.name}</div>
			</div>
			<ButtonCounter
				count={basketItem.quantity}
				onAdd={handleAdd}
				onChangeCount={handleChangeCount}
			/>
		</div>
	);
};
