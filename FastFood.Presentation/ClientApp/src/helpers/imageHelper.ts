export const convertImageFileToBase64 = async (
	file: File | Blob
): Promise<string> => {
	return new Promise<string>((resolve, reject) => {
		const reader = new FileReader();
		reader.onload = () => {
			if (typeof reader.result === "string") {
				resolve(reader.result);
			} else {
				reject(new Error("Failed to convert image to Base64."));
			}
		};
		reader.onerror = (error) => {
			reject(error);
		};
		reader.readAsDataURL(file);
	});
};

export const convertImagePathToBase64 = async (
	url: string
): Promise<string> => {
	const response = await fetch(url);
	const blob = await response.blob();

	return await convertImageFileToBase64(blob);
};
