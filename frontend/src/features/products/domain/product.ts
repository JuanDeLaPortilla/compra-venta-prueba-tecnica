export interface Product {
  id: number;
  name: string;
  batchNumber: string | null;
  cost: number;
  salePrice: number;
  stock: number;
}

export interface CreateProductRequest {
  name: string;
  batchNumber: string;
}