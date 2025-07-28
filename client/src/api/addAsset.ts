import { API_URL } from "../config";
import type { AssetType } from "../types";

export default async function addAsset({
  id: accountID,
  symbol,
  description,
  type,
}: AssetType): Promise<AssetType> {
  const response = await fetch(`${API_URL}/assets`, {
    method: "POST",
    body: JSON.stringify({ accountID, symbol, description, type }),
    headers: { "Content-Type": "application/json" },
  });
  return response.json();
}
