import { PageHeader } from "@/shared/components/page-header";
import { AddPurchaseCard } from "./components/add-purchase-card";

const AddPurchasePage = () => {
  return (
    <>
      <PageHeader
        title="Registrar Compra"
        description="Registra una compra para ingresar stock al inventario."
        actionLabel="Volver a la lista"
        actionPath="/purchases"
        buttonType="back"
      />

      <AddPurchaseCard/>
    </>
  );
}

export default AddPurchasePage;