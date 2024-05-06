import { Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";
import AppContext from "./contexts/AppContext";

export default function App() {
  return (
    <div className="container min-h-dvh min-w-full bg-neutral-800">
      <div className="wrapper mx-auto sm:w-3/5">
        <main>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route
              path="/admin/:token"
              element={
                <AppContext.Provider value={{ isAdmin: true }}>
                  <HomePage />
                </AppContext.Provider>
              }
            />
          </Routes>
        </main>
      </div>
    </div>
  );
}
