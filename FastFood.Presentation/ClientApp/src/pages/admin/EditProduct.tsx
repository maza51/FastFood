import { useParams } from "react-router";
import { FC, useEffect, useState } from "react";
import { IProduct } from "@/types/IProduct";
import { IUpdateProductModel, ProductService } from "@/services/productService";
import {
	IProductFormModel,
	ProductForm,
} from "@/components/admin/ProductForm/ProductForm";

export const EditProduct: FC = () => {
	const { id } = useParams();

	const [product, setProduct] = useState<IProduct | null>(null);
	const [tooltip, setTooltip] = useState<JSX.Element>(<></>);

	useEffect(() => {
		fetchData().then();
	}, [id]);

	const fetchData = async () => {
		try {
			const result = await ProductService.getById(Number(id));
			setProduct(result);
		} catch (error) {
			console.log(error);
		}
	};

	const handleSubmit = async (productModel: IProductFormModel) => {
		const updateModel: IUpdateProductModel = {
			id: Number(id),
			name: productModel.name,
			description: productModel.description,
			unitPrice: productModel.unitPrice,
			imageBase64: productModel.imageBase64,
			categoryId: productModel.categoryId,
		};

		try {
			await ProductService.update(updateModel);
			setTooltip(
				<div style={{ color: "#6ebd96" }}>Продукт изменен!</div>
			);
		} catch (error) {
			console.error(error);
			setTooltip(
				<div style={{ color: "#fd826e" }}>Ошибка на сервере!</div>
			);
		}
	};

	return product && (
		<ProductForm
			initialProduct={product}
			header={"Изминение продукта:"}
			tooltip={tooltip}
			onSubmit={handleSubmit}
		/>
	);
};
