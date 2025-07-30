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
import type { AssetType } from "@/types";
import addItem from "@/api/addItem";

type AddAssetProps = {
  assets: AssetType[];
  setAssets: React.Dispatch<React.SetStateAction<AssetType[]>>;
  type: AssetType["type"];
  accountId: number;
};

export default function AddAsset({ assets, setAssets, type, accountId }: AddAssetProps) {
  const [symbol, setSymbol] = useState<string>("");
  const [description, setDescription] = useState<string>("");

  async function handleAddAsset() {
    const newAsset: AssetType = await addItem("assets", {
      accountId,
      symbol,
      description,
      type,
    });
    setAssets([...assets, newAsset]);
    console.log(newAsset);
  }

  function resetValues() {
    setSymbol("");
    setDescription("");
  }

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    handleAddAsset();
    document.getElementById("close-dialog")?.click(); // Simulate closing dialog
    resetValues();
  }

  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button>Add Item</Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Add a new {type} asset </DialogTitle>
          <DialogDescription />
        </DialogHeader>
        <form onSubmit={handleSubmit}>
          <div className="grid w-full max-w-sm items-center gap-1.5 mb-4">
            <Label htmlFor="symbol">Symbol</Label>
            <Input
              type="symbol"
              id="symbol"
              placeholder="Symbol..."
              value={symbol}
              onChange={(e) => setSymbol(e.target.value)}
            />
          </div>

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
