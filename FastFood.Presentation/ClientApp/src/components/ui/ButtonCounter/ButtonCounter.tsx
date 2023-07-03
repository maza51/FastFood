import styles from "./ButtonCounter.module.css";
import { FC } from "react";

interface IProps {
	count: number;
	onAdd: () => void;
	onChangeCount: (count: number) => void;
}

export const ButtonCounter: FC<IProps> = (props: IProps) => {
	const increment = () => {
		props.onChangeCount(props.count + 1);
	};

	const decrement = () => {
		props.onChangeCount(props.count - 1);
	};

	const counter = (
		<div className={styles.counter}>
			<div className={styles.action} onClick={decrement}>
				-
			</div>
			<div>{props.count}</div>
			<div className={styles.action} onClick={increment}>
				+
			</div>
		</div>
	);

	const add = (
		<div className={styles.add} onClick={props.onAdd}>
			<div className={styles.action}>
				<div className={styles.textWrapper}>
					<b>+</b> <div>Добавить</div>
				</div>
			</div>
		</div>
	);

	return (
		<div className={styles.buttonCounter}>
			{props.count > 0 ? counter : add}
		</div>
	);
};
