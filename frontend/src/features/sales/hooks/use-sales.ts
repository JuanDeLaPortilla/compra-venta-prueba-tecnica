import { useQuery } from "@tanstack/react-query";
import { saleService } from "../infrastructure/sale.service";
import { saleKeys } from "./sale-keys";

export const useSales = () => {
  return useQuery({
    queryKey: saleKeys.list(),
    queryFn: () => saleService.getSales()
  });
};