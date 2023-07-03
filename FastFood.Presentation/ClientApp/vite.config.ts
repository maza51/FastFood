import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import mkcert from "vite-plugin-mkcert";
import https from "https";
import path from "path";

// https://vitejs.dev/config/
export default defineConfig({
	plugins: [react(), mkcert()],
	server: {
		port: 44494,
		https: true,
		strictPort: true,
		proxy: {
			"/api": {
				target: "https://localhost:7194",
				changeOrigin: true,
				secure: false,
				agent: new https.Agent(),
			},
			"/swagger": {
				target: "https://localhost:7194",
				changeOrigin: true,
				secure: false,
				agent: new https.Agent(),
			},
			"/Image": {
				target: "https://localhost:7194",
				changeOrigin: true,
				secure: false,
				agent: new https.Agent(),
			},
			"/hub": {
				target: "https://localhost:7194",
				changeOrigin: true,
				secure: false,
				ws: true,
				agent: new https.Agent(),
			},
		},
	},
	resolve: {
		alias: {
			"@/": path.resolve(__dirname, "./src"),
			"@/components": path.resolve(__dirname, "./src/components"),
			"@/api": path.resolve(__dirname, "./src/api"),
			"@/assets": path.resolve(__dirname, "./src/assets"),
			"@/types": path.resolve(__dirname, "./src/types"),
			"@/providers": path.resolve(__dirname, "./src/providers"),
			"@/pages": path.resolve(__dirname, "./src/pages"),
			"@/hooks": path.resolve(__dirname, "./src/hooks"),
			"@/services": path.resolve(__dirname, "./src/services"),
			"@/helpers": path.resolve(__dirname, "./src/helpers"),
		},
	},
	optimizeDeps: {
		exclude: ["js-big-decimal"],
	},
});
