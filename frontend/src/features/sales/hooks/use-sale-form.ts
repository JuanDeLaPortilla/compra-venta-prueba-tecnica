import { useMemo, useState } from "react";

import type { Product } from "@/features/products/domain/product";
import { TAX_RATE } from "@/shared/constants";

import type { SaleDetail } from "../domain/sale";

const newRow = (): SaleDetail => ({
  rowId: crypto.randomUUID(),

  productId: null,
  productName: "",

  stock: 0,

  quantity: 1,
  unitPrice: 0,
});

export const calculateSaleDetailAmounts = (
  quantity: number,
  unitPrice: number,
) => {
  const subtotal = quantity * unitPrice;
  const taxAmount = subtotal * TAX_RATE;
  const total = subtotal + taxAmount;

  return {
    subtotal,
    taxAmount,
    total,
  };
};

export function useSaleForm() {
  const [details, setDetails] = useState<SaleDetail[]>([
    newRow(),
  ]);

  const addRow = () => {
    setDetails((prev) => [...prev, newRow()]);
  };

  const removeRow = (rowId: string) => {
    setDetails((prev) =>
      prev.length > 1
        ? prev.filter((detail) => detail.rowId !== rowId)
        : prev,
    );
  };

  const updateRow = (
    rowId: string,
    patch: Partial<SaleDetail>,
  ) => {
    setDetails((prev) =>
      prev.map((detail) =>
        detail.rowId === rowId
          ? { ...detail, ...patch }
          : detail,
      ),
    );
  };

  const selectProduct = (
    rowId: string,
    product: Product,
  ) => {
    updateRow(rowId, {
      productId: product.id,
      productName: product.name,
      stock: product.stock,
      unitPrice: product.salePrice,
      quantity: 1,
    });
  };

  const summary = useMemo(() => {
    const productsCount = details.filter(
      (detail) => detail.productId !== null,
    ).length;

    const units = details.reduce(
      (acc, detail) => acc + (detail.quantity || 0),
      0,
    );

    const subtotal = details.reduce(
      (acc, detail) =>
        acc +
        (detail.quantity || 0) *
          (detail.unitPrice || 0),
      0,
    );

    const taxAmount = subtotal * TAX_RATE;

    const total = subtotal + taxAmount;

    return {
      productsCount,
      units,
      subtotal,
      taxAmount,
      total,
    };
  }, [details]);

  const hasStockError = details.some(
    (detail) =>
      detail.productId !== null &&
      detail.quantity > detail.stock,
  );

  const isValid =
    details.length > 0 &&
    details.every(
      (detail) =>
        detail.productId !== null &&
        detail.quantity > 0 &&
        detail.unitPrice > 0 &&
        detail.quantity <= detail.stock,
    );

  return {
    details,

    addRow,
    removeRow,
    updateRow,
    selectProduct,

    summary,

    hasStockError,
    isValid,
  };
}