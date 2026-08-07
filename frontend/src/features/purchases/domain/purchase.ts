export interface Purchase {
  id: number;
  registrationDate: string;
  subTotal: number;
  taxAmount: number;
  totalAmount: number;
}

export interface PurchaseDetail {
  /** local row id, UI only */
  rowId: string;
  productId: number | null;
  productName: string;
  stock: number;
  quantity: number;
  unitPrice: number;
}

export interface CreatePurchaseDetailRequest {
  productId: number;
  quantity: number;
  unitPrice: number;
}

export interface CreatePurchaseRequest {
  details: CreatePurchaseDetailRequest[];
}