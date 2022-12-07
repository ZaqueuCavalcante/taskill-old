import { Button } from "@mui/material";
import { Navigate, Route, Routes } from "react-router-dom";

export const AppRoutes = () => {
  return (
    <Routes>
      <Route path="tasks" element={<Button>Lalala</Button>} />

      <Route path="*" element={<Navigate to="/" />} />
    </Routes>
  );
};
