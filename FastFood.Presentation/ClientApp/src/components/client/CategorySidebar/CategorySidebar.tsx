import { FC } from "react";
import { SideBar } from "@/components/ui/SideBar/SideBar";
import { SideBarItem } from "@/components/ui/SideBar/SideBarItem";
import { useFetchCategories } from "@/hooks/useFetchCategories";

interface IProps {
	selectedCategory: number;
}

export const CategorySidebar: FC<IProps> = ({ selectedCategory }) => {
	const { categories, loading } = useFetchCategories();

	return !loading ? (
		<SideBar>
			<SideBarItem
				link={"/client/category/new"}
				selected={0 == selectedCategory}
			>
				Новинки
			</SideBarItem>
			{categories.map((category) => (
				<SideBarItem
					link={`/client/category/${category.id}`}
					key={category.id}
					selected={category.id == selectedCategory}
				>
					{category.name}
				</SideBarItem>
			))}
		</SideBar>
	) : null;
};
