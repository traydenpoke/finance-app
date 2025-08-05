export type AccountType = {
  id?: number;
  description: string;
  type: 'cash' | 'crypto' | 'stock';
  balance: number;
};

export type AssetType = {
  id?: number;
  accountId?: number;
  symbol: string;
  description: string;
  type: 'stock' | 'crypto';
};

export type PriceType = Record<string, number> | null;

export type AccountProps = {
  accounts: AccountType[];
  setAccounts: React.Dispatch<React.SetStateAction<AccountType[]>>;
};

export type AssetProps = {
  assets: AssetType[];
  setAssets: React.Dispatch<React.SetStateAction<AssetType[]>>;
};

export type TableTypes = 'accounts' | 'assets' | 'transactions';
