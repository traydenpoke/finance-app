import { API_URL } from "../config";
import type { AccountType } from "../types";

export default async function addAccount({
  description,
  type,
  balance,
}: AccountType): Promise<AccountType> {
  const response = await fetch(`${API_URL}/accounts`, {
    method: "POST",
    body: JSON.stringify({ description, type, balance }),
    headers: { "Content-Type": "application/json" },
  });
  return response.json();
}
