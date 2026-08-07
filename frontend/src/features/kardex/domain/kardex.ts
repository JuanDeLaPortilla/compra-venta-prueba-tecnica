export interface KardexProduct {
  productId: number;
  productName: string;
  stock: number;
  cost: number;
  salePrice: number;
}

export interface ProductMovement {
  registrationDate: string;
  movementType: string;
  quantity: number;
}