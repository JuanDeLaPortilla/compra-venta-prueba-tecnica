import { PageHeader } from "@/shared/components/page-header";
import { KardexTable } from "./components/kardex-table";
import { MovementsDialog } from "./components/movements-dialog";
import { useKardex } from "../hooks/user-kardex";
import { useState } from "react";
import type { KardexProduct } from "../domain/kardex";
import { Helmet } from "react-helmet-async";
import { BASE_TITLE } from "@/shared/constants";

const KardexPage = () => {

  const {
    data: products = [],
    isLoading,
    isError,
    error,
  } = useKardex();

  const [selectedProduct, setSelectedProduct] =
    useState<KardexProduct | null>(null);

  return (
    <>
      <Helmet>
        <title>Kardex | {BASE_TITLE}</title>
      </Helmet>

      <PageHeader
        title="Kardex de Inventario"
        description="Consulta el movimiento y saldo de cada producto."
      />

      {isError && (
        <div className="mb-4 rounded-lg border border-destructive/30 bg-destructive/10 p-4 text-sm text-destructive">
          {error instanceof Error
            ? error.message
            : "No se pudo obtener el Kardex."}
        </div>
      )}

      <KardexTable
        products={products}
        loading={isLoading}
        onViewMovements={setSelectedProduct}
      />

      <MovementsDialog
        product={selectedProduct}
        open={selectedProduct !== null}
        onOpenChange={(open) => {
          if (!open) {
            setSelectedProduct(null);
          }
        }}
      />

    </>
  );
}

export default KardexPage;