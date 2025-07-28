import { API_URL } from "../config";
import type { AccountType } from "../types";

export default async function getAccounts(): Promise<AccountType[]> {
  const response = await fetch(`${API_URL}/accounts`);
  return response.json();
}
