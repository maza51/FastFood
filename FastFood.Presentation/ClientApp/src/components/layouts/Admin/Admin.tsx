import styles from "./Admin.module.css";
import { Outlet } from "react-router-dom";
import { AdminSidebar } from "@/components/admin/AdminSidebar";

export const Admin = () => {
	return (
		<div className={styles.admin}>
			<AdminSidebar />
			<div className={styles.main}>
				<Outlet />
			</div>
		</div>
	);
};
