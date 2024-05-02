import { Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";

export default function App() {
  return (
    <div className="container flex min-h-dvh min-w-full bg-neutral-800">
      <div className="wrapper flex-1 flex flex-col px-2">
        <Routes>
          <Route path="/" element={<HomePage />} />
        </Routes>
      </div>
    </div>
  );
}
