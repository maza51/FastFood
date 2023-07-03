import styles from "./Default.module.css";
import { FC } from "react";
import { Outlet } from "react-router-dom";
import { Header } from "./Header/Header";

export const Default: FC = () => {
	return (
		<div className={styles.layout}>
			<Header></Header>
			<div className={styles.main}>
				<Outlet />
			</div>
		</div>
	);
};
