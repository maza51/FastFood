import styles from "./SideBar.module.css";
import { FC, ReactNode } from "react";
import { Link } from "react-router-dom";

interface IProps {
	link: string;
	selected: boolean;
	children: ReactNode;
}

export const SideBarItem: FC<IProps> = (props: IProps) => {
	return (
		<div className={props.selected ? styles.selectedItem : styles.item}>
			<Link
				to={{
					pathname: props.link,
				}}
			>
				{props.children}
			</Link>
		</div>
	);
};
