import styles from "./BaseTable.module.css";
import { FC } from "react";
import { ITableAction } from "@/components/admin/tables/BaseTable/ITableAction";

interface IProps {
	items: object[];
	actions: ITableAction<any>[] | undefined;
}

export const Body: FC<IProps> = ({ items, actions }) => {
	return (
		<tbody>
			{items.map((item, index) => (
				<tr key={index}>
					{Object.values(item).map((property, index) => (
						<td key={index}>{property}</td>
					))}
					{actions && (
						<td>
							<div className={styles.buttonActionsWrapper}>
								{actions.map((action, index) => (
									<div
										className={styles.action}
										key={index}
										onClick={() => action.action(item)}
									>
										{action.element}
									</div>
								))}
							</div>
						</td>
					)}
				</tr>
			))}
		</tbody>
	);
};
