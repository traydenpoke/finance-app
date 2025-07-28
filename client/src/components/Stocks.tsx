import { Button } from "./ui/button";
import type { AccountProps, AssetProps } from "@/types";
import AddAccount from "./AddAccount";
import DisplayAccounts from "./DisplayAccounts";

export default function Stocks({
  accounts,
  setAccounts,
  assets,
  setAssets,
}: AccountProps & AssetProps) {
  const stockAccounts = accounts.filter((acc) => acc.type === "stock");

  return (
    <div>
      <h1>hii this is the stocks page :3</h1>

      <Button onClick={() => console.log(stockAccounts)}>get stocks</Button>

      <AddAccount accounts={accounts} setAccounts={setAccounts} type="stock" />

      <DisplayAccounts
        accounts={stockAccounts}
        assets={assets}
        setAssets={setAssets}
        type="stock"
      />
    </div>
  );
}
