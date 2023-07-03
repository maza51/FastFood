import { FC } from "react";
import { SideBarItem } from "@/components/ui/SideBar/SideBarItem";
import { SideBar } from "@/components/ui/SideBar/SideBar";
import { useLocation } from "react-router";

export const AdminSidebar: FC = () => {
	const location = useLocation();

	const { pathname } = location;

	const splitLocation = pathname.split("/");

	return (
		<SideBar>
			<SideBarItem
				link={"products"}
				selected={splitLocation[2] === "products"}
			>
				Продукты
			</SideBarItem>
			<SideBarItem
				link={"create-product"}
				selected={splitLocation[2] === "create-product"}
			>
				Новый продукт
			</SideBarItem>
			<SideBarItem
				link={"orders"}
				selected={splitLocation[2] === "orders"}
			>
				Заказы
			</SideBarItem>
		</SideBar>
	);
};
