import axiosBase from "@/api/axiosBase";
import { ICategory } from "@/types/ICategory";
import { IPagedList } from "@/types/IPagedList";

export const CategoryService = {
	async getAll(): Promise<IPagedList<ICategory>> {
		const response = await axiosBase.get<IPagedList<ICategory>>(
			"categories"
		);
		return response.data as IPagedList<ICategory>;
	},

	async create(category: ICreateCategoryModel): Promise<ICategory> {
		const response = await axiosBase.post<ICategory>(
			"categories",
			category
		);
		return response.data as ICategory;
	},

	async delete(id: number): Promise<void> {
		await axiosBase.delete(`categories/${id}`);
	},
};

export interface ICreateCategoryModel {
	name: string;
}
