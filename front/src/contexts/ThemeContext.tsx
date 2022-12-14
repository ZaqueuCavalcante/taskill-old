import { createContext } from "react";

interface IThemeContextData {
  name: "light" | "dark";
  toggle: () => void;
}

const ThemeContext = createContext({} as IThemeContextData);
