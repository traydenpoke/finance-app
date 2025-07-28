import { Button } from "./ui/button";
import type { AccountProps } from "@/types";
import AddAccount from "./AddAccount";

export default function Crypto({ accounts, setAccounts }: AccountProps) {
  const cryptoAccounts = accounts.filter((acc) => acc.type === "crypto");

  return (
    <div>
      <h1>hii this is the crypto page :3</h1>

      <Button onClick={() => console.log(cryptoAccounts)}>get crypto</Button>

      <AddAccount accounts={accounts} setAccounts={setAccounts} type="crypto" />
    </div>
  );
}
