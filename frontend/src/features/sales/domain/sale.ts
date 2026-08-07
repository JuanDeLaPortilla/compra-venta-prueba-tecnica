export interface Sale {
  id: number;
  registrationDate: string;
  subTotal: number;
  taxAmount: number;
  totalAmount: number;
}

export interface SaleDetail {
  rowId: string;

  productId: number | null;
  productName: string;

  stock: number;

  quantity: number;
  unitPrice: number;
}

export interface CreateSaleDetailRequest {
  productId: number;
  quantity: number;
  unitPrice: number;
}

export interface CreateSaleRequest {
  details: CreateSaleDetailRequest[];
}