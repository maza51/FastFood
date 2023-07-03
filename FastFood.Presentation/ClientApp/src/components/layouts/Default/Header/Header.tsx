import styles from "./Header.module.css";
import { FC } from "react";
import { Link } from "react-router-dom";

export const Header: FC = () => {
	return (
		<header className={styles.header}>
			<div className={styles.splitter}>
				<div className={styles.wrapper}>
					<Link to="admin">Админ</Link>
					<Link to="staff">Персонал</Link>
					<Link to="client">Клиент</Link>
					<Link to="order-board">Доска заказов</Link>
				</div>
				<div className={styles.wrapper}>
					<Link to="about">О приложении</Link>
				</div>
			</div>
		</header>
	);
};
