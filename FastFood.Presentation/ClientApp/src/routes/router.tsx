import { createBrowserRouter, Navigate } from "react-router-dom";
import { Default } from "@/components/layouts/Default/Default";
import { Admin } from "@/components/layouts/Admin/Admin";
import { CreateProduct } from "@/pages/admin/CreateProduct";
import { EditProduct } from "@/pages/admin/EditProduct";
import { ProductList } from "@/pages/admin/ProductList";
import { EditOrder } from "@/pages/admin/EditOrder";
import { OrderList } from "@/pages/admin/OrderList";
import { StaffOrderList } from "@/pages/staff/StaffOrderList";
import { CategoryProducts } from "@/pages/client/CategoryProducts";
import { OrderBoard } from "@/pages/order-board/OrderBoard/OrderBoard";
import {About} from "@/pages/About.tsx";

export const router = createBrowserRouter([
	{
		element: <Default />,
		children: [
			{
				path: "/",
				element: <Navigate to="client/category" replace={true} />,
			},
			{
				path: "admin",
				element: <Admin />,
				children: [
					{
						index: true,
						path: "",
						element: <Navigate to="products" replace={true} />,
					},
					{
						path: "create-product",
						element: <CreateProduct />,
					},
					{
						path: "update-product/:id",
						element: <EditProduct />,
					},
					{
						path: "products",
						element: <ProductList />,
					},
					{
						path: "update-order-status/:id",
						element: <EditOrder />,
					},
					{
						path: "orders",
						element: <OrderList />,
					},
				],
			},
			{
				path: "staff",
				children: [
					{
						index: true,
						path: "",
						element: <Navigate to="orders" replace={true} />,
					},
					{
						path: "orders",
						element: <StaffOrderList />,
					},
				],
			},
			{
				path: "client",
				children: [
					{
						index: true,
						path: "",
						element: <Navigate to="category" replace={true} />,
					},
					{
						path: "category/:id?",
						element: <CategoryProducts />,
					},
				],
			},
			{
				path: "order-board",
				children: [
					{
						index: true,
						path: "",
						element: <OrderBoard />,
					},
				],
			},
			{
				path: "about",
				children: [
					{
						index: true,
						path: "",
						element: <About />,
					},
				],
			},
		],
	},
]);
