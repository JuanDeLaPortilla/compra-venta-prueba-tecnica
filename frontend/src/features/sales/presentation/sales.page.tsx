import { PageHeader } from "@/shared/components/page-header";
import { SalesTable } from "./components/sales-table";
import { useSales } from "../hooks/use-sales";
import { BASE_TITLE } from "@/shared/constants";
import { Helmet } from "react-helmet-async";

const SalesPage = () => {

  const {
    data: sales = [],
    isLoading
  } = useSales();

  return (
    <>
      <Helmet>
        <title>Ventas | {BASE_TITLE}</title>
      </Helmet>

      <PageHeader
        title="Ventas"
        description="Consulta las ventas realizadas."
        actionLabel="Registrar Venta"
        actionPath="add"
      />

      <SalesTable
        sales={sales}
        loading={isLoading}
      />
    </>
  );
}

export default SalesPage;