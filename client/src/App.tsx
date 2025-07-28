import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./index.css";
import Header from "./components/Header";
import { ThemeProvider } from "./components/ThemeProvider";
import Home from "./components/Home";
import Cash from "./components/Cash";
import Stocks from "./components/Stocks";
import Crypto from "./components/Crypto";
import { useEffect, useState } from "react";
import type { AccountType, AssetType } from "./types";
import getAccounts from "./api/getAccounts";
import getAssets from "./api/getAssets";

function App() {
  const [accounts, setAccounts] = useState<AccountType[]>([]);
  const [assets, setAssets] = useState<AssetType[]>([]);

  const initAccounts = async () => {
    setAccounts(await getAccounts());
  };

  const initAssets = async () => {
    setAssets(await getAssets());
  };

  useEffect(() => {
    initAccounts();
    initAssets();
  }, []);

  return (
    <main className="">
      <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
        <Router>
          <Header />
          <div className="pt-16 m-auto w-fit">
            <Routes>
              <Route path="/" element={<Home accounts={accounts} assets={assets} />} />
              <Route
                path="/cash"
                element={<Cash accounts={accounts} setAccounts={setAccounts} />}
              />
              <Route
                path="/stocks"
                element={
                  <Stocks
                    accounts={accounts}
                    setAccounts={setAccounts}
                    assets={assets}
                    setAssets={setAssets}
                  />
                }
              />
              <Route
                path="/crypto"
                element={
                  <Crypto
                    accounts={accounts}
                    setAccounts={setAccounts}
                    assets={assets}
                    setAssets={setAssets}
                  />
                }
              />
            </Routes>
          </div>
        </Router>
      </ThemeProvider>
    </main>
  );
}

export default App;
