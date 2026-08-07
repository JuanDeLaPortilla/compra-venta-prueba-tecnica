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

import type { Product } from "@/features/products/domain/product";
import { ProductSelector } from "@/features/products/presentation/components/product-selector";

import type { SaleDetail } from "../../domain/sale";
import { calculateSaleDetailAmounts } from "../../hooks/use-sale-form";

interface SaleDetailsTableProps {
  products: Product[];
  details: SaleDetail[];

  onSelectProduct: (
    rowId: string,
    product: Product,
  ) => void;

  onUpdateRow: (
    rowId: string,
    patch: Partial<SaleDetail>,
  ) => void;

  onRemoveRow: (rowId: string) => void;

  isProductsLoading: boolean;
  isProductsError: boolean;
}

export function SaleDetailsTable({
  products,
  details,
  onSelectProduct,
  onUpdateRow,
  onRemoveRow,
  isProductsLoading,
  isProductsError,
}: SaleDetailsTableProps) {

  const [quantityInputs, setQuantityInputs] = useState<Record<string, string>>({});

  return (
    <div className="w-full overflow-x-auto rounded-lg border">
      <Table className="min-w-[860px]">
        <TableHeader>
          <TableRow className="bg-muted/50">
            <TableHead className="w-[24%]">Producto</TableHead>
            <TableHead className="w-[10%]">Stock actual</TableHead>
            <TableHead className="w-[10%]">Cantidad</TableHead>
            <TableHead className="w-[15%]">Precio de venta</TableHead>
            <TableHead className="w-[13%] text-right">Subtotal</TableHead>
            <TableHead className="w-[10%] text-right">IGV</TableHead>
            <TableHead className="w-[10%] text-right">Total</TableHead>
            <TableHead className="w-[8%] text-right">Acción</TableHead>
          </TableRow>
        </TableHeader>

        <TableBody>
          {details.map((detail) => {
            const {
              subtotal,
              taxAmount,
              total,
            } = calculateSaleDetailAmounts(
              detail.quantity,
              detail.unitPrice,
            );

            const stockError =
              detail.productId !== null &&
              detail.quantity > detail.stock;

            return (
              <TableRow
                key={detail.rowId}
                className="align-middle"
              >
                <TableCell>
                  <ProductSelector
                    products={products}
                    selectedId={detail.productId}
                    onSelect={(product) => onSelectProduct(
                      detail.rowId,
                      product
                    )}
                    isLoading={isProductsLoading}
                    isError={isProductsError}
                  />
                </TableCell>

                <TableCell>
                  {detail.productId ? (
                    <Badge
                      variant={
                        stockError
                          ? "destructive"
                          : "secondary"
                      }
                    >
                      {detail.stock}
                    </Badge>
                  ) : (
                    <span className="text-sm text-muted-foreground">
                      —
                    </span>
                  )}
                </TableCell>

                <TableCell>
                  <Input
                    type="number"
                    min={1}
                    max={detail.stock || undefined}
                    step={1}
                    value={
                      quantityInputs[detail.rowId] ??
                      String(detail.quantity)
                    }
                    aria-label="Cantidad"
                    aria-invalid={stockError}
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

                  {stockError && (
                    <p className="mt-1 text-xs text-destructive">
                      La cantidad no puede ser mayor al
                      stock disponible ({detail.stock}).
                    </p>
                  )}
                </TableCell>

                <TableCell>
                  <span className="font-medium tabular-nums">
                    {money(detail.unitPrice)}
                  </span>
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
                    onClick={() =>
                      onRemoveRow(detail.rowId)
                    }
                    className="text-muted-foreground hover:text-destructive"
                  >
                    <Trash2 className="size-4" />
                  </Button>
                </TableCell>
              </TableRow>
            );
          })}
        </TableBody>
      </Table>
    </div>
  );
}