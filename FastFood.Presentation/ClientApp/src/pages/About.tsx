import { FC } from "react";

export const About: FC = () => {
	return (
		<div style={{ width: "400px", margin: "40px auto" }}>
			<div style={{ marginBottom: "10px" }}>
				Это демонстрационное приложение.
			</div>
			<div style={{ marginBottom: "30px" }}>
				Подробнее можно узнать на&nbsp;
				<a href="https://github.com/maza51/FastFood/" target="_blank">
					GinHub
				</a>
				.
			</div>
			<div style={{ fontSize: "14px" }}>
				Приложение сбрасывается каждый час.
			</div>
		</div>
	);
};
