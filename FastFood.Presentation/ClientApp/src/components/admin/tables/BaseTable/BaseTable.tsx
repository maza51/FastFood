import styles from "./BaseTable.module.css";
import { ITableAction } from "@/components/admin/tables/BaseTable/ITableAction";
import { Header } from "@/components/admin/tables/BaseTable/Header";
import { Body } from "@/components/admin/tables/BaseTable/Body";
import { FC } from "react";

interface IProps {
	headerItems: string[];
	objects: any[];
	actions?: ITableAction<any>[] | undefined;
}

export const BaseTable: FC<IProps> = ({ headerItems, objects, actions }) => {
	return (
		<table className={styles.baseTable}>
			
			<Header items={headerItems} action={actions !== undefined} />
			<Body items={objects} actions={actions} />
		</table>
	);
};
