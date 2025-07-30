import { API_URL } from "../config";
import type { TableTypes } from "../types";

export default async function addItem<T>(table: TableTypes, item: T): Promise<T> {
  const response = await fetch(`${API_URL}/${table}`, {
    method: "POST",
    body: JSON.stringify({ ...item }),
    headers: { "Content-Type": "application/json" },
  });
  return response.json();
}
