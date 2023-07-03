import { IProduct } from "@/types/IProduct";
import { FC } from "react";
import { ProductService } from "@/services/productService";
import { BaseTable } from "@/components/admin/tables/BaseTable/BaseTable";
import { useNavigate } from "react-router-dom";
import { ITableAction } from "@/components/admin/tables/BaseTable/ITableAction";
import { useFetchCategories } from "@/hooks/useFetchCategories";

interface IProductTableItem {
	id: number;
	name: string;
	unitPrice: number;
	category: string;
}

interface IProps {
	products: IProduct[];
	onChanged: () => void;
}

export const ProductTable: FC<IProps> = ({ products, onChanged }) => {
	const navigate = useNavigate();

	const { categories } = useFetchCategories();

	const productTableItems: IProductTableItem[] = [];
	for (const product of products) {
		productTableItems.push({
			id: product.id,
			name: product.name,
			unitPrice: product.unitPrice,
			category:
				categories.find((x) => x.id === product.categoryId)?.name ?? "",
		});
	}

	const actions: ITableAction<IProductTableItem>[] = [
		{
			element: <div style={{ color: "green" }}>edit</div>,
			action: (product: IProductTableItem) => {
				navigate(`../update-product/${product.id}`);
			},
		},
		{
			element: <div style={{ color: "red" }}>delete</div>,
			action: async (product: IProductTableItem) => {
				if (confirm("Вы хотите удалить продукт?")) {
					try {
						await ProductService.delete(product.id);
						onChanged();
					} catch (error) {
						console.log(error);
					}
				}
			},
		},
	];

	return (
		<BaseTable
			headerItems={["Id", "Имя", "Цена", "Категория"]}
			objects={productTableItems}
			actions={actions}
		/>
	);
};
