import { useState } from "react";
import { useNavigate } from "react-router";
import { useCreatePurchase } from "../../hooks/use-create-purchase";
import { Card, CardHeader, CardTitle, CardDescription, CardContent, CardFooter } from "@/shared/components/ui/card";
import { PurchaseDetailsTable } from "./purchase-details-table";
import { Button } from "@/shared/components/ui/button";
import { PackagePlus, Plus } from "lucide-react";
import { usePurchaseForm } from "../../hooks/use-purchase-form";
import { PurchaseSummary } from "./purchase-summary";
import type { CreatePurchaseRequest } from "../../domain/purchase";
import { CreateProductDialog } from "../../../products/presentation/components/create-product-dialog";
import { useProducts } from "@/features/products/hooks/use-products";
import { useCreateProduct } from "@/features/products/hooks/use-create-product";
import { toast } from "sonner";
import type { CreateProductRequest } from "@/features/products/domain/product";

export const AddPurchaseCard = () => {

  const navigate = useNavigate();

  const [dialogOpen, setDialogOpen] = useState(false);

  const {
    details,
    addRow,
    removeRow,
    updateRow,
    selectProduct,
    addProduct,
    summary,
    isValid,
  } = usePurchaseForm();

  const {
    data: products = [],
    isLoading: isProductsLoading,
    isError: isProductsError,
  } = useProducts();

  const productMutation = useCreateProduct();

  const handleCreateProduct = async (
    request: CreateProductRequest,
  ) => {
    try {
      const response = await productMutation.mutateAsync(request);

      const product = response.data;

      if (!product) {
        toast.error(
          response.message ?? "No se pudo crear el producto.",
        );

        return;
      }

      addProduct(product);

      toast.success(
        response.message ?? "Producto creado correctamente.",
      );

      setDialogOpen(false);
    } catch (error) {
      toast.error(
        "No se pudo registrar el producto.",
      );
    }
  };

  const purchaseMutation = useCreatePurchase();

  const handleSubmit = async () => {
    if (!isValid) {
      return;
    }

    const request: CreatePurchaseRequest = {
      details: details.map((detail) => ({
        productId: detail.productId!,
        quantity: detail.quantity,
        unitPrice: detail.unitPrice,
      })),
    };

    try {
      const response =
        await purchaseMutation.mutateAsync(request);

      toast.success(
        response.message ??
        "Compra registrada correctamente.",
      );

      navigate("/purchases");
    } catch (error) {
      toast.error(
        "No se pudo registrar la compra.",
      );
    }
  };

  return (
    <>
      <CreateProductDialog
        open={dialogOpen}
        onOpenChange={setDialogOpen}
        onCreate={handleCreateProduct}
      />

      <Card className="shadow-none">

        <CardHeader className="border-b">
          <CardTitle>Productos de la compra</CardTitle>
          <CardDescription>
            Selecciona los productos y especifica la cantidad y el precio de compra.
          </CardDescription>
        </CardHeader>

        <CardContent className="space-y-4">
          <PurchaseDetailsTable
            products={products}
            details={details}
            onSelectProduct={selectProduct}
            onUpdateRow={updateRow}
            onRemoveRow={removeRow}
            onRequestCreateProduct={() => setDialogOpen(true)}
            isProductsLoading={isProductsLoading}
            isProductsError={isProductsError}
          />

          <div className="flex flex-col gap-2 sm:flex-row">
            <Button variant="outline" onClick={addRow}>
              <Plus className="size-4" />
              Agregar producto
            </Button>
            <Button variant="secondary" onClick={() => setDialogOpen(true)}>
              <Plus className="size-4" />
              Registrar producto
            </Button>
          </div>
        </CardContent>

        <CardFooter className="flex-col gap-4">
          <PurchaseSummary
            productsCount={summary.productsCount}
            units={summary.units}
            subtotal={summary.subtotal}
            taxAmount={summary.taxAmount}
            total={summary.total}
          />

          <div className="flex justify-end w-full">
            <Button
              size="lg"
              disabled={!isValid || purchaseMutation.isPending}
              onClick={handleSubmit}
            >
              <PackagePlus className="size-4" />

              {purchaseMutation.isPending
                ? "Registrando..."
                : "Registrar compra"}
            </Button>
          </div>
        </CardFooter>

      </Card >
    </>
  );
}