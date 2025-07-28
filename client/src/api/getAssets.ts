import { API_URL } from "../config";
import type { AssetType } from "../types";

export default async function getAssets(): Promise<AssetType[]> {
  const response = await fetch(`${API_URL}/assets`);
  return response.json();
}
