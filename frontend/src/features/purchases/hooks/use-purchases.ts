import { useQuery } from "@tanstack/react-query";
import { purchaseService } from "../infrastructure/purchase.service";
import { purchaseKeys } from "./purchase-keys";

export const usePurchases = () => {
  return useQuery({
    queryKey: purchaseKeys.list(),
    queryFn: () => purchaseService.getPurchases()
  });
};