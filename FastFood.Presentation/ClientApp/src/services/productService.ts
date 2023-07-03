import axiosBase from "@/api/axiosBase";
import { IProduct } from "@/types/IProduct";
import { IPagedList } from "@/types/IPagedList";

export const ProductService = {
	async getPaged(
		pageIndex: number,
		pageSize: number
	): Promise<IPagedList<IProduct>> {
		const response = await axiosBase.get<IPagedList<IProduct>>(
			"products",
			{
				params: {
					pageIndex,
					pageSize,
				},
			}
		);
		return response.data as IPagedList<IProduct>;
	},

	async getNew(
		pageIndex: number,
		pageSize: number
	): Promise<IPagedList<IProduct>> {
		const response = await axiosBase.get<IPagedList<IProduct>>("products/new", {
			params: {
				pageIndex,
				pageSize,
			},
		});
		return response.data as IPagedList<IProduct>;
	},

	async getPagedByCategory(
		categoryId: number,
		pageIndex: number,
		pageSize: number
	): Promise<IPagedList<IProduct>> {
		const response = await axiosBase.get<IPagedList<IProduct>>(
			`categories/${categoryId}/products`,
			{
				params: {
					pageIndex,
					pageSize,
				},
			}
		);
		return response.data as IPagedList<IProduct>;
	},

	async getById(id: number): Promise<IProduct> {
		const response = await axiosBase.get<IProduct>(`products/${id}`);
		return response.data as IProduct;
	},

	async create(product: ICreateProductModel): Promise<IProduct> {
		const response = await axiosBase.post<IProduct>(
			`categories/${product.categoryId}/products`,
			product
		);
		return response.data as IProduct;
	},

	async update(product: IUpdateProductModel): Promise<IProduct> {
		const response = await axiosBase.put<IProduct>(
			`categories/${product.categoryId}/products`,
			product
		);
		return response.data as IProduct;
	},

	async delete(id: number): Promise<void> {
		await axiosBase.delete(`products/${id}`);
	},
};

export interface ICreateProductModel {
	name: string;
	description: string;
	unitPrice: number;
	imageBase64: string;
	categoryId: number;
}

export interface IUpdateProductModel {
	id: number;
	name: string;
	description: string;
	unitPrice: number;
	imageBase64: string;
	categoryId: number;
}
