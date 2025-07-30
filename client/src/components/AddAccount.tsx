import { Button } from "./ui/button";
import {
  DialogFooter,
  DialogHeader,
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogTitle,
  DialogTrigger,
} from "./ui/dialog";
import { Label } from "@radix-ui/react-label";
import { Input } from "./ui/input";
import { useState } from "react";
import type { AccountType } from "@/types";
import addItem from "@/api/addItem";

type AddAccountProps = {
  accounts: AccountType[];
  setAccounts: React.Dispatch<React.SetStateAction<AccountType[]>>;
  type: AccountType["type"];
};

export default function AddAccount({ accounts, setAccounts, type }: AddAccountProps) {
  const [description, setDescription] = useState<string>("");
  const [balance, setBalance] = useState<string>("");

  async function handleAddAccount() {
    if (isNaN(Number(balance))) {
      console.error("not a number!");
      return;
    }

    const newAccount: AccountType = await addItem("accounts", {
      description,
      type,
      balance: Number(balance),
    });
    setAccounts([...accounts, newAccount]);
    console.log(newAccount);
  }

  function resetValues() {
    setDescription("");
    setBalance("");
  }

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    handleAddAccount();
    document.getElementById("close-dialog")?.click(); // Simulate closing dialog
    resetValues();
  }

  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button>Add Account</Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Create a new {type} account </DialogTitle>
          <DialogDescription />
        </DialogHeader>
        <form onSubmit={handleSubmit}>
          <div className="grid w-full max-w-sm items-center gap-1.5 mb-4">
            <Label htmlFor="description">Description</Label>
            <Input
              type="description"
              id="description"
              placeholder="Description..."
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />
          </div>

          <div className="grid w-full max-w-sm items-center gap-1.5 mb-4">
            <Label htmlFor="balance">Balance</Label>
            <Input
              type="balance"
              id="balance"
              placeholder="Balance..."
              value={balance}
              onChange={(e) => setBalance(e.target.value)}
            />
          </div>

          <DialogFooter>
            <DialogClose asChild>
              <Button type="submit" id="close-dialog">
                Add
              </Button>
            </DialogClose>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
