import ReactDOM from "react-dom/client";
import "./index.css";
import { RouterProvider } from "react-router-dom";
import { BasketProvider } from "@/providers/BasketProvider";
import { HubProvider } from "@/providers/HubProvider";
import { router } from "./routes/router";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
	<HubProvider>
		<BasketProvider>
			<RouterProvider router={router} />
		</BasketProvider>
	</HubProvider>
);
