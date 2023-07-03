import { useContext } from "react";
import { HubContext } from "@/providers/HubProvider";

export const useHubContext = () => {
	const context = useContext(HubContext);
	if (!context) throw new Error("hub context must be used");

	return context;
};
