import { useState } from "react";
import { Trash2 } from "lucide-react";
import { Badge } from "@/shared/components/ui/badge";
import { Button } from "@/shared/components/ui/button";
import { Input } from "@/shared/components/ui/input";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/shared/components/ui/table";
import { money } from "@/shared/lib/utils";
import type { PurchaseDetail } from "../../domain/purchase";
import { ProductSelector } from "@/features/products/presentation/components/product-selector";
import type { Product } from "@/features/products/domain/product";
import { calculateDetailAmounts } from "../../hooks/use-purchase-form";

interface PurchaseDetailsTableProps {
  products: Product[];
  details: PurchaseDetail[];
  onSelectProduct: (rowId: string, product: Product) => void;
  onUpdateRow: (rowId: string, patch: Partial<PurchaseDetail>) => void;
  onRemoveRow: (rowId: string) => void;
  onRequestCreateProduct: () => void;
  isProductsLoading: boolean;
  isProductsError: boolean;
}

export function PurchaseDetailsTable({
  products,
  details,
  onSelectProduct,
  onUpdateRow,
  onRemoveRow,
  onRequestCreateProduct,
  isProductsLoading,
  isProductsError
}: PurchaseDetailsTableProps) {

  const [quantityInputs, setQuantityInputs] = useState<Record<string, string>>({});
  const [priceInputs, setPriceInputs] = useState<Record<string, string>>({});

  return (
    <div className="w-full overflow-x-auto rounded-lg border">
      <Table className="min-w-[860px]">

        <TableHeader>
          <TableRow className="bg-muted/50">
            <TableHead className="w-[24%]">Producto</TableHead>
            <TableHead className="w-[10%]">Stock actual</TableHead>
            <TableHead className="w-[10%]">Cantidad</TableHead>
            <TableHead className="w-[15%]">Precio de compra</TableHead>
            <TableHead className="w-[13%] text-right">Subtotal</TableHead>
            <TableHead className="w-[10%] text-right">IGV</TableHead>
            <TableHead className="w-[10%] text-right">Total</TableHead>
            <TableHead className="w-[8%] text-right">Acción</TableHead>
          </TableRow>
        </TableHeader>

        <TableBody>
          {details.map((detail) => {

            const { subtotal, taxAmount, total } =
              calculateDetailAmounts(
                detail.quantity,
                detail.unitPrice,
              );

            return (
              <TableRow key={detail.rowId} className="align-middle">
                <TableCell>
                  <ProductSelector
                    products={products}
                    selectedId={detail.productId}
                    onSelect={(product) => onSelectProduct(detail.rowId, product)}
                    onRequestCreate={onRequestCreateProduct}
                    isLoading={isProductsLoading}
                    isError={isProductsError}
                  />
                </TableCell>
                <TableCell>
                  {detail.productId ? (
                    <Badge variant="secondary">{detail.stock}</Badge>
                  ) : (
                    <span className="text-sm text-muted-foreground">—</span>
                  )}
                </TableCell>
                <TableCell>
                  <Input
                    type="number"
                    min={1}
                    step={1}
                    value={
                      quantityInputs[detail.rowId] ??
                      String(detail.quantity)
                    }
                    aria-label="Cantidad"
                    onChange={(e) => {
                      const rawValue = e.target.value;

                      setQuantityInputs((prev) => ({
                        ...prev,
                        [detail.rowId]: rawValue,
                      }));

                      if (rawValue === "") {
                        return;
                      }

                      const value = Number.parseInt(rawValue, 10);

                      if (!Number.isNaN(value)) {
                        onUpdateRow(detail.rowId, {
                          quantity: value,
                        });
                      }
                    }}
                    onBlur={() => {
                      setQuantityInputs((prev) => {
                        const next = { ...prev };
                        delete next[detail.rowId];
                        return next;
                      });
                    }}
                  />
                </TableCell>

                <TableCell>
                  <Input
                    type="number"
                    min={0}
                    step="0.01"
                    value={
                      priceInputs[detail.rowId] ??
                      String(detail.unitPrice)
                    }
                    aria-label="Precio de compra"
                    onChange={(e) => {
                      const rawValue = e.target.value;

                      setPriceInputs((prev) => ({
                        ...prev,
                        [detail.rowId]: rawValue,
                      }));

                      if (rawValue === "") {
                        return;
                      }

                      const value = Number.parseFloat(rawValue);

                      if (!Number.isNaN(value)) {
                        onUpdateRow(detail.rowId, {
                          unitPrice: value,
                        });
                      }
                    }}
                    onBlur={() => {
                      setPriceInputs((prev) => {
                        const next = { ...prev };
                        delete next[detail.rowId];
                        return next;
                      });
                    }}
                  />
                </TableCell>

                <TableCell className="text-right font-medium tabular-nums">
                  {money(subtotal)}
                </TableCell>
                <TableCell className="text-right font-medium tabular-nums">
                  {money(taxAmount)}
                </TableCell>
                <TableCell className="text-right font-medium tabular-nums">
                  {money(total)}
                </TableCell>
                <TableCell className="text-right">
                  <Button
                    variant="ghost"
                    size="icon"
                    aria-label="Eliminar fila"
                    disabled={details.length <= 1}
                    onClick={() => onRemoveRow(detail.rowId)}
                    className="text-muted-foreground hover:text-destructive"
                  >
                    <Trash2 className="size-4" />
                  </Button>
                </TableCell>
              </TableRow>
            )
          })}
        </TableBody>

      </Table>
    </div>
  );
}