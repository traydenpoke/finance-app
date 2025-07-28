import { Button } from "./ui/button";
import type { AccountProps, AssetProps } from "@/types";
import AddAccount from "./AddAccount";
import DisplayAccounts from "./DisplayAccounts";

export default function Crypto({
  accounts,
  setAccounts,
  assets,
  setAssets,
}: AccountProps & AssetProps) {
  const cryptoAccounts = accounts.filter((acc) => acc.type === "crypto");

  return (
    <div>
      <h1>hii this is the crypto page :3</h1>

      <Button onClick={() => console.log(cryptoAccounts)}>get crypto</Button>

      <AddAccount accounts={accounts} setAccounts={setAccounts} type="crypto" />

      <DisplayAccounts
        accounts={cryptoAccounts}
        assets={assets}
        setAssets={setAssets}
        type="crypto"
      />
    </div>
  );
}
