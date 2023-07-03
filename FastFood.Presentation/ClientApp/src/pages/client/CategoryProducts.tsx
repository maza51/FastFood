import { useParams } from "react-router-dom";
import { FC } from "react";
import { ProductGrid } from "@/components/client/ProductGrid/ProductGrid";
import { CategorySidebar } from "@/components/client/CategorySidebar/CategorySidebar";
import { BasketContainer } from "@/components/client/BasketContainer/BasketContainer";

export const CategoryProducts: FC = () => {
	const { id } = useParams();

	return (
		<div style={{ display: "flex" }}>
			<CategorySidebar selectedCategory={Number(id)} />
			<ProductGrid categoryId={Number(id)} />
			<BasketContainer />
		</div>
	);
};
