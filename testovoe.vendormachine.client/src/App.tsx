import { Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";

export default function App() {
  return (
    <div className="container min-h-dvh min-w-full bg-neutral-800">
      <div className="wrapper mx-auto sm:w-3/5">
        <main>
          <Routes>
            <Route path="/" element={<HomePage />} />
          </Routes>
        </main>
      </div>
    </div>
  );
}
