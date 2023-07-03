import styles from "./Modal.module.css";
import { FC, ReactNode } from "react";

interface IProps {
	isOpen: boolean;
	onClose: () => void;
	children: ReactNode;
}

export const Modal: FC<IProps> = (props: IProps) => {
	if (!props.isOpen) {
		return null;
	}

	return (
		<div className={styles.modal} onClick={props.onClose}>
			<div className={styles.window} onClick={(e) => e.stopPropagation()}>
				{props.children}
			</div>
		</div>
	);
};
