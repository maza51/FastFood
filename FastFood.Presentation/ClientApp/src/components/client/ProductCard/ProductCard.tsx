import styles from "./ProductCard.module.css";
import { FC } from "react";
import { ButtonCounter } from "@/components/ui/ButtonCounter/ButtonCounter";
import { IProduct } from "@/types/IProduct";
import { useBasketContext } from "@/hooks/useBasketContext";

interface IProps {
	product: IProduct;
}

export const ProductCard: FC<IProps> = ({ product }) => {
	const context = useBasketContext();

	const handleAdd = () => {
		context.createItem(product);
	};

	const handleChangeCount = (count: number) => {
		context.changeItemCount(product.id, count);
	};

	const getItemQuantity = () => {
		const basketItem = context.items.find(
			(x) => x.product.id === product.id
		);
		return basketItem?.quantity ?? 0;
	};

	const imageUrl = import.meta.env.VITE_APP_IMAGE_URL;

	return (
		<div className={styles.productCard}>
			<div className={styles.body}>
				<img
					className={styles.image}
					src={imageUrl + product.imagePath}
					alt={"img"}
				/>
				<div className={styles.name}>{product.name}</div>
				<div className={styles.description}>{product.description}</div>
				<div className={styles.price}>{product.unitPrice} ₽</div>
			</div>
			<div className={styles.footer}>
				<ButtonCounter
					count={getItemQuantity()}
					onAdd={handleAdd}
					onChangeCount={handleChangeCount}
				/>
			</div>
		</div>
	);
};
