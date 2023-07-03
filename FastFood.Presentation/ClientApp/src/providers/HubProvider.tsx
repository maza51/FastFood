import { HubConnectionBuilder } from "@microsoft/signalr";
import { createContext, useEffect, useState } from "react";

const HUB_URL: string = import.meta.env.VITE_APP_HUB_URL + "message";

interface IHubContext {
	connection: any;
}

export const HubContext = createContext<IHubContext | undefined>(undefined);

export const HubProvider = ({ children }) => {
	const [connection, setConnection] = useState<any>(null);

	useEffect(() => {
		createConnection().then();

		return () => {
			if (connection) {
				connection.stop();
				console.log("SignalR connection stopped.");
			}
		};
	}, []);

	const createConnection = async () => {
		const newConnection = new HubConnectionBuilder()
			.withUrl(HUB_URL)
			.build();

		try {
			await newConnection.start();
			console.log("SignalR connection established.");
			setConnection(newConnection);
		} catch (error) {
			console.error("Error establishing SignalR connection:", error);
		}
	};

	return (
		<HubContext.Provider value={{ connection }}>
			{children}
		</HubContext.Provider>
	);
};
