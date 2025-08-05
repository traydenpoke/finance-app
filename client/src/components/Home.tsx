import { Button } from './ui/button';
import type { AccountType, AssetType, PriceType } from '@/types';
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table';

export default function Home({
  accounts,
  assets,
  assetPrices,
}: {
  accounts: AccountType[];
  assets: AssetType[];
  assetPrices: PriceType;
}) {
  return (
    <div>
      <h1>hii this is the home page :3</h1>

      <Button onClick={() => console.log(accounts)}>get all accounts</Button>
      <Button onClick={() => console.log(assets)}>get all assets</Button>
      <Button onClick={() => console.log(assetPrices)}>
        get all asset prices
      </Button>

      {accounts.length ? (
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Account Type</TableHead>
              <TableHead>Description</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {accounts.map((c) => (
              <TableRow key={c.id}>
                <TableCell>{c.type}</TableCell>
                <TableCell>{c.description}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      ) : (
        <h2>No accounts found.</h2>
      )}
    </div>
  );
}
