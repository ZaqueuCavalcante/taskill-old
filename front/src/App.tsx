import { BrowserRouter } from "react-router-dom";
import { LateralMenu } from "./components";
import { AppThemeProvider } from "./contexts/ThemeContext";
import { AppRoutes } from "./routes";

export const App = () => {
  return (
    <AppThemeProvider>
      <BrowserRouter>
        <LateralMenu>
          <AppRoutes />
        </LateralMenu>
      </BrowserRouter>
    </AppThemeProvider>
  );
};
