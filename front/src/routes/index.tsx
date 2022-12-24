import { Button } from "@mui/material";
import { Navigate, Route, Routes } from "react-router-dom";
import { useAppThemeContext } from "../contexts/ThemeContext";

export const AppRoutes = () => {
  const { toggleTheme } = useAppThemeContext();

  return (
    <Routes>
      <Route
        path="tasks"
        element={
          <Button variant="contained" color="primary" onClick={toggleTheme}>
            Change Theme
          </Button>
        }
      />

      <Route path="*" element={<Navigate to="/" />} />
    </Routes>
  );
};
