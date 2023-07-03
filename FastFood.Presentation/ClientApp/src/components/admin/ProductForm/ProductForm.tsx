import styles from "./ProductForm.module.css";
import { FC, useEffect, useState } from "react";
import { InputImageBase64 } from "@/components/ui/InputImageBase64/InputImageBase64";
import { Button } from "@/components/ui/Button/Button";
import { IProduct } from "@/types/IProduct";
import { InputFloat } from "@/components/ui/InputFloat/InputFloat";
import { convertImagePathToBase64 } from "@/helpers/imageHelper";
import { useFetchCategories } from "@/hooks/useFetchCategories";

interface IProps {
	header: string;
	tooltip: JSX.Element;
	initialProduct?: IProduct;
	onSubmit: (productModel: IProductFormModel) => void;
}

export const ProductForm: FC<IProps> = (props: IProps) => {
	const [name, setName] = useState<string>("");
	const [description, setDescription] = useState<string>("");
	const [unitPrice, setUnitPrice] = useState<number>(0);
	const [imageBase64, setImageBase64] = useState<string>("");
	const [categoryId, setCategoryId] = useState<number>(0);

	const { categories } = useFetchCategories();

	if (!categoryId && categories.length > 0) {
		setCategoryId(categories[0].id);
	}

	useEffect(() => {
		setStates().then();
	}, [props.initialProduct]);

	const imageUrl = import.meta.env.VITE_APP_IMAGE_URL;

	const setStates = async () => {
		if (props.initialProduct) {
			const image = await convertImagePathToBase64(
				imageUrl + props.initialProduct.imagePath
			);

			setName(props.initialProduct.name);
			setDescription(props.initialProduct.description);
			setUnitPrice(props.initialProduct.unitPrice);
			setImageBase64(image);
			setCategoryId(props.initialProduct.categoryId);
		}
	};

	const handleSubmit = async (e) => {
		e.preventDefault();

		const productModel: IProductFormModel = {
			name: name,
			description: description,
			unitPrice: parseFloat(unitPrice.toFixed(2)),
			imageBase64: imageBase64,
			categoryId: categoryId,
		};

		props.onSubmit(productModel);
	};

	return (
		<div className={styles.productForm}>
			<h3>{props.header}</h3>
			<form>
				<InputImageBase64
					value={imageBase64}
					onChange={(imageString: string) =>
						setImageBase64(imageString)
					}
				/>
				<br />
				<label>
					Name:
					<input
						type="text"
						value={name}
						onChange={(e) => setName(e.target.value)}
					/>
				</label>
				<br />
				<label>
					Description:
					<input
						type="text"
						value={description}
						onChange={(e) => setDescription(e.target.value)}
					/>
				</label>
				<br />
				<label>
					Unit price:
					<InputFloat
						value={unitPrice}
						onChange={(value) => setUnitPrice(value)}
					/>
				</label>
				<br />
				<label>
					Category:
					<select
						onChange={(e) =>
							setCategoryId(parseInt(e.target.value))
						}
						value={categoryId}
					>
						{categories.map((category) => (
							<option key={category.id} value={category.id}>
								{category.name}
							</option>
						))}
					</select>
				</label>
				<br />
				<div className={styles.footer}>
					<Button type="submit" onClick={handleSubmit}>
						Отправить
					</Button>
					<div className={styles.tooltip}>{props.tooltip}</div>
				</div>
			</form>
		</div>
	);
};

export interface IProductFormModel {
	name: string;
	description: string;
	unitPrice: number;
	imageBase64: string;
	categoryId: number;
}
