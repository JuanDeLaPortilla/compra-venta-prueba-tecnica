import {
  useMutation,
  useQueryClient,
} from "@tanstack/react-query";

import { saleService } from "../infrastructure/sale.service";
import { saleKeys } from "../hooks/sale-keys";
import type { CreateSaleRequest } from "../domain/sale";

export function useCreateSale() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (request: CreateSaleRequest) =>
      saleService.createSale(request),

    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: saleKeys.list(),
        refetchType: "active",
      });
    },
  });
}