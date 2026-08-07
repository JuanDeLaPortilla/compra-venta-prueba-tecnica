import { PageHeader } from "@/shared/components/page-header";
import { AddSaleCard } from "./components/add-sale-card";

const AddSalePage = () => {
  return (
    <>
      <PageHeader
        title="Registrar Venta"
        description="Crea una nueva venta y actualiza tu inventario."
        actionLabel="Volver a la lista"
        actionPath="/sales"
        buttonType="back"
      />

      <AddSaleCard />
    </>
  );
}

export default AddSalePage;