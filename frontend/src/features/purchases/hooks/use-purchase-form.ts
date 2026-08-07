import { useMemo, useState } from "react";
import type { PurchaseDetail } from "../domain/purchase";
import type { Product } from "@/features/products/domain/product";
import { TAX_RATE } from "@/shared/constants";

const newRow = (): PurchaseDetail => ({
  rowId: crypto.randomUUID(),
  productId: null,
  productName: "",
  stock: 0,
  quantity: 1,
  unitPrice: 0,
});

export const calculateDetailAmounts = (
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

export function usePurchaseForm() {
  const [details, setDetails] = useState<PurchaseDetail[]>([newRow()]);

  const addRow = () => setDetails((prev) => [...prev, newRow()]);

  const removeRow = (rowId: string) =>
    setDetails((prev) => (prev.length > 1 ? prev.filter((d) => d.rowId !== rowId) : prev));

  const updateRow = (rowId: string, patch: Partial<PurchaseDetail>) =>
    setDetails((prev) => prev.map((d) => (d.rowId === rowId ? { ...d, ...patch } : d)));

  const selectProduct = (rowId: string, product: Product) =>
    updateRow(rowId, {
      productId: product.id,
      productName: product.name,
      stock: product.stock,
      unitPrice: product.cost,
    });

  const addProduct = (product: Product) => {
    setDetails((prev) => {
      const emptyRowIndex = prev.findIndex(
        (detail) => detail.productId === null,
      );

      const detail: PurchaseDetail = {
        rowId: crypto.randomUUID(),
        productId: product.id,
        productName: product.name,
        stock: 0,
        quantity: 1,
        unitPrice: product.cost,
      };

      if (emptyRowIndex === -1) {
        return [...prev, detail];
      }

      const next = [...prev];

      next[emptyRowIndex] = {
        ...detail, rowId: next[emptyRowIndex].rowId,
      };

      return next;
    });
  };

  const summary = useMemo(() => {
    const productsCount = details.filter(
      (d) => d.productId !== null
    ).length;

    const units = details.reduce(
      (acc, d) => acc + (d.quantity || 0),
      0
    );

    const subtotal = details.reduce(
      (acc, d) =>
        acc + (d.quantity || 0) * (d.unitPrice || 0),
      0
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

  const isValid =
    details.length > 0 &&
    details.every((d) => d.productId !== null && d.quantity > 0 && d.unitPrice > 0);

  return {
    details,
    addRow,
    removeRow,
    updateRow,
    selectProduct,
    addProduct,
    summary,
    isValid,
  };
}
