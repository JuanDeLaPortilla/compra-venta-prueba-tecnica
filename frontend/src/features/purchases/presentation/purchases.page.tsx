import { PageHeader } from "@/shared/components/page-header";
import { PurchasesTable } from "./components/purchases-table";
import { usePurchases } from "../hooks/use-purchases";
import { Helmet } from "react-helmet-async";
import { BASE_TITLE } from "@/shared/constants";

const PurchasesPage = () => {

  const {
    data: purchases = [],
    isLoading
  } = usePurchases();

  return (
    <>
      <Helmet>
        <title>Compras | {BASE_TITLE}</title>
      </Helmet>

      <PageHeader
        title="Compras"
        description="Consulta y revisa todas tus compras."
        actionLabel="Registrar Compra"
        actionPath="add"
      />

      <PurchasesTable
        purchases={purchases}
        loading={isLoading}
      />

    </>
  );
}

export default PurchasesPage;