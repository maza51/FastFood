import { FC, useState } from "react";
import { ICreateProductModel, ProductService } from "@/services/productService";
import {
	IProductFormModel,
	ProductForm,
} from "@/components/admin/ProductForm/ProductForm";

export const CreateProduct: FC = () => {
	const [tooltip, setTooltip] = useState<JSX.Element>(<></>);

	const handleSubmit = async (productModel: IProductFormModel) => {
		const createModel: ICreateProductModel = productModel;

		try {
			await ProductService.create(createModel);
			setTooltip(
				<div style={{ color: "#6ebd96" }}>Продукт добавлен!</div>
			);
		} catch (error) {
			console.error(error);
			setTooltip(
				<div style={{ color: "#fd826e" }}>Ошибка на сервере!</div>
			);
		}
	};

	return (
		<ProductForm
			header={"Добавление продукта:"}
			tooltip={tooltip}
			onSubmit={handleSubmit}
		/>
	);
};
