import { useNavigate } from "react-router";
import { PackagePlus, Plus } from "lucide-react";
import { toast } from "sonner";

import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
  CardFooter,
} from "@/shared/components/ui/card";

import { Button } from "@/shared/components/ui/button";

import { useProducts } from "@/features/products/hooks/use-products";

import { useCreateSale } from "../../hooks/use-create-sale";
import { useSaleForm } from "../../hooks/use-sale-form";

import type { CreateSaleRequest } from "../../domain/sale";

import { SaleDetailsTable } from "./sale-details-table";
import { SaleSummary } from "./sale-summary";

export const AddSaleCard = () => {
  const navigate = useNavigate();

  const {
    details,
    addRow,
    removeRow,
    updateRow,
    selectProduct,
    summary,
    hasStockError,
    isValid,
  } = useSaleForm();

  const {
    data: products = [],
    isLoading: isProductsLoading,
    isError: isProductsError,
  } = useProducts();

  const saleMutation = useCreateSale();

  const handleSubmit = async () => {
    if (!isValid) {
      if (hasStockError) {
        toast.error(
          "La cantidad no puede ser mayor al stock disponible.",
        );
      }

      return;
    }

    const request: CreateSaleRequest = {
      details: details.map((detail) => ({
        productId: detail.productId!,
        quantity: detail.quantity,
        unitPrice: detail.unitPrice,
      })),
    };

    try {
      const response =
        await saleMutation.mutateAsync(request);

      toast.success(
        response.message ??
          "Venta registrada correctamente.",
      );

      navigate("/sales");
    } catch {
      toast.error(
        "No se pudo registrar la venta.",
      );
    }
  };

  return (
    <Card className="shadow-none">
      <CardHeader className="border-b">
        <CardTitle>
          Productos de la venta
        </CardTitle>

        <CardDescription>
          Selecciona los productos y especifica la
          cantidad a vender.
        </CardDescription>
      </CardHeader>

      <CardContent className="space-y-4">
        <SaleDetailsTable
          products={products}
          details={details}
          onSelectProduct={selectProduct}
          onUpdateRow={updateRow}
          onRemoveRow={removeRow}
          isProductsLoading={isProductsLoading}
          isProductsError={isProductsError}
        />

        <div className="flex flex-col gap-2 sm:flex-row">
          <Button
            variant="outline"
            onClick={addRow}
          >
            <Plus className="size-4" />
            Agregar producto
          </Button>
        </div>
      </CardContent>

      <CardFooter className="flex-col gap-4">
        <SaleSummary
          productsCount={summary.productsCount}
          units={summary.units}
          subtotal={summary.subtotal}
          taxAmount={summary.taxAmount}
          total={summary.total}
        />

        <div className="flex w-full justify-end">
          <Button
            size="lg"
            disabled={
              !isValid ||
              saleMutation.isPending
            }
            onClick={handleSubmit}
          >
            <PackagePlus className="size-4" />

            {saleMutation.isPending
              ? "Registrando..."
              : "Registrar venta"}
          </Button>
        </div>
      </CardFooter>
    </Card>
  );
};