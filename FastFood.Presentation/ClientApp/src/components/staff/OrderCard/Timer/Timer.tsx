import styles from "./Timer.module.css";
import { FC, useEffect, useState } from "react";

interface IProps {
	date: Date;
}

export const Timer: FC<IProps> = (props: IProps) => {
	const [time, setTime] = useState<string>("");

	useEffect(() => {
		setDiffTime();

		const interval = setInterval(() => {
			setDiffTime();
		}, 1000);

		return () => clearInterval(interval);
	}, []);

	const setDiffTime = () => {
		const currentTime = new Date();
		const diffTime = Number(currentTime) - Number(props.date);

		const seconds = Math.floor((diffTime / 1000) % 60);
		const minutes = Math.floor((diffTime / (1000 * 60)) % 60);

		const hours = Math.floor(diffTime / (1000 * 60 * 60));

		if (diffTime / (1000 * 60 * 60) > 1) {
			setTime(`${hours.toString()} hours`);
			return;
		}

		const minutesStr = minutes.toString().padStart(2, "0");
		const secondsStr = seconds.toString().padStart(2, "0");

		setTime(`${minutesStr}:${secondsStr}`);
	};

	return <div className={styles.timer}>{time}</div>;
};
