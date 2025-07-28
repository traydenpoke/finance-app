import { Button } from "./ui/button";
import type { AccountProps } from "@/types";
import AddAccount from "./AddAccount";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";

export default function Cash({ accounts, setAccounts }: AccountProps) {
  const cashAccounts = accounts.filter((acc) => acc.type === "cash");

  return (
    <div>
      <h1>Cash Accounts</h1>

      <Button onClick={() => console.log(cashAccounts)}>Log Cash Accounts</Button>

      <AddAccount accounts={accounts} setAccounts={setAccounts} type="cash" />

      {cashAccounts.length ? (
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Description</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {cashAccounts.map((c) => (
              <TableRow key={c.id}>
                <TableCell>{c.description}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      ) : (
        <h2>No cash accounts found.</h2>
      )}
    </div>
  );
}
