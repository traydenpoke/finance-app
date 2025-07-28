import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import AddAsset from "./AddAsset";
import type { AccountType, AssetProps, AssetType } from "@/types";
import DisplayAssets from "./DisplayAssets";

type DisplayAccountProps = {
  accounts: AccountType[];
  type: AssetType["type"];
} & AssetProps;

export default function DisplayAccounts({
  accounts,
  assets,
  setAssets,
  type,
}: DisplayAccountProps) {
  const typeAccounts = accounts.filter((acc) => acc.type === type);

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>Description</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {typeAccounts.map((c) => (
          <TableRow key={c.id}>
            <TableCell>{c.description}</TableCell>
            <TableCell>
              <AddAsset assets={assets} setAssets={setAssets} type={type} accountId={c.id!} />
            </TableCell>
            <TableCell>
              <DisplayAssets assets={assets} accountId={c.id!} />
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
