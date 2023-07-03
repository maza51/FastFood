import styles from "./Button.module.css";
import { ButtonHTMLAttributes, FC } from "react";

export const Button: FC<ButtonHTMLAttributes<HTMLButtonElement>> = (
	props: ButtonHTMLAttributes<HTMLButtonElement>
) => {
	return <button className={styles.button} {...props} />;
};
