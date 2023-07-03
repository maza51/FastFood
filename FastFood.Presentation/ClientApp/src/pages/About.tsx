import { FC } from "react";

export const About: FC = () => {
	return (
		<div
			style={{ width: "500px", margin: "80px auto", textAlign: "center" }}
		>
			<div style={{ marginBottom: "80px" }}>
				Описание приложения можно посмотреть на&nbsp;
				<a href="https://github.com/maza51/FastFood/" target="_blank">
					GitHub
				</a>
				.
			</div>
			<div style={{ fontSize: "14px" }}>
				Приложение сбрасывается каждый час.
			</div>
		</div>
	);
};
