import { Button } from "./ui/button";
import type { AccountProps, AssetProps } from "@/types";
import AddAccount from "./AddAccount";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import AddAsset from "./AddAsset";

export default function Stocks({
  accounts,
  setAccounts,
  assets,
  setAssets,
}: AccountProps & AssetProps) {
  const stockAccounts = accounts.filter((acc) => acc.type === "stock");
  const stockAssets = assets.filter((asset) => asset.type === "stock");

  return (
    <div>
      <h1>hii this is the stocks page :3</h1>

      <Button onClick={() => console.log(stockAccounts)}>get stocks</Button>

      <AddAccount accounts={accounts} setAccounts={setAccounts} type="stock" />

      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Description</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {stockAccounts.map((c) => (
            <TableRow key={c.id}>
              <TableCell>{c.description}</TableCell>
              <TableCell>
                <AddAsset
                  assets={stockAssets}
                  setAssets={setAssets}
                  type={"stock"}
                  accountID={c.id!}
                />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
}
