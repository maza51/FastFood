import styles from "./InputImageBase64.module.css";
import { FC, useEffect, useState } from "react";

interface IProps {
	value: string;
	onChange: (imageString: string) => void;
}

export const InputImageBase64: FC<IProps> = (props: IProps) => {
	const [imageString, setImageString] = useState<string>(props.value);

	useEffect(() => {
		setImageString(props.value);
	}, [props.value]);

	const handleChangeImage = async (e) => {
		const file = e.target.files[0];
		const reader = new FileReader();

		if (file) {
			reader.readAsDataURL(file);
		}

		reader.onloadend = () => {
			const base64String = reader.result ? reader.result.toString() : "";
			setImageString(base64String);
			props.onChange(base64String);
		};
	};

	return (
		<label className={styles.inputImageBase64}>
			<input
				type="file"
				accept="image/*"
				className={styles.input}
				onChange={handleChangeImage}
			/>
			{imageString ? (
				<img src={imageString} alt={"wtf"} />
			) : (
				<div>Выбрать изображение</div>
			)}
		</label>
	);
};
