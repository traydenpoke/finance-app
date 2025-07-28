import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import type { AssetType } from "@/types";

type DisplayAssetsProps = {
  assets: AssetType[];
  accountId: number;
};

export default function DisplayAssets({ assets, accountId }: DisplayAssetsProps) {
  const accountAssets = assets.filter((asset) => asset.accountId === accountId);

  return <p>{accountAssets.map((asset) => asset.symbol).join(", ")}</p>;
}
