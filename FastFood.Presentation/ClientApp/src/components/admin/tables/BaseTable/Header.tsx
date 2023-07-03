import { FC } from "react";

interface IProps {
	items: string[];
	action: boolean;
}

export const Header: FC<IProps> = ({ items, action }) => {
	return (
		<thead>
			<tr>
				{items.map((item, index) => (
					<th key={index}>{item}</th>
				))}
				{action && <th style={{width: "10%"}}>Действия</th>}
			</tr>
		</thead>
	);
};
