import { useEffect, useState } from "react";
import { ICategory } from "@/types/ICategory";
import { CategoryService } from "@/services/categoryService";

export const useFetchCategories = () => {
	const [categories, setCategories] = useState<ICategory[]>([]);
	const [loading, setLoading] = useState<boolean>(false);

	useEffect(() => {
		fetchData().then();
	}, []);

	const fetchData = async () => {
		setLoading(true);
		try {
			const result = await CategoryService.getAll();
			setCategories(result.data);
		} catch (error) {
			console.error(error);
		} finally {
			setLoading(false);
		}
	};

	return { categories, loading };
};
