/// <reference types="vite/client" />

interface ImportMetaEnv {
	readonly VITE_APP_API_URL: string;
	readonly VITE_APP_HUB_URL: string;
	readonly VITE_APP_IMAGE_URL: string;
	// more env variables...
}

interface ImportMeta {
	readonly env: ImportMetaEnv;
}
