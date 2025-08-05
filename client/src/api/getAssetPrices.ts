import type { PriceType } from '@/types';
import { API_URL } from '../config';

export default async function getAssetPrices(): Promise<PriceType> {
  const response = await fetch(`${API_URL}/assets/prices`);
  return response.json();
}
