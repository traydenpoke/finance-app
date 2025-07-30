import { API_URL } from "../config";
import type { TableTypes } from "../types";

export default async function getTable<T>(table: TableTypes): Promise<T[]> {
  const response = await fetch(`${API_URL}/${table}`);
  return response.json();
}
