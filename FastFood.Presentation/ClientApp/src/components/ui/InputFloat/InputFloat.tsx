import { ChangeEvent, FC, useEffect, useState } from "react";

interface IProps {
	value: number;
	onChange?: (value: number) => void;
}

export const InputFloat: FC<IProps> = (props: IProps) => {
	const [floatStringValue, setFloatStringValue] = useState<string>("");

	useEffect(() => {
		setFloatStringValue(props.value.toString());
	}, [props.value]);

	const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
		let floatString = getFloatString(e.target.value);

		floatString = floatString.length > 0 ? floatString : "0";

		setFloatStringValue(floatString);

		if (props.onChange) {
			props.onChange(parseFloat(floatString));
		}
	};

	const getFloatString = (value: string): string => {
		if (!value) {
			return "";
		}

		if (isFloat(value)) {
			return value;
		}

		return floatStringValue;
	};

	const isFloat = (value: string): boolean => {
		return /^\d+([.]\d*?)?$/.test(value);
	};

	return (
		<input type="text" value={floatStringValue} onChange={handleChange} />
	);
};
