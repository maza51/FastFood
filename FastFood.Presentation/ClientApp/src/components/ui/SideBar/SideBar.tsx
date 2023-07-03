import styles from "./SideBar.module.css";
import { FC, ReactNode } from "react";

interface IProps {
	children: ReactNode;
}

export const SideBar: FC<IProps> = (props: IProps) => {
	return <div className={styles.sideBar}>{props.children}</div>;
};
