import {
  useMutation,
  useQueryClient,
} from "@tanstack/react-query";

import { purchaseService } from "../infrastructure/purchase.service";
import type { CreatePurchaseRequest } from "../domain/purchase";
import { purchaseKeys } from "./purchase-keys";

export function useCreatePurchase() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (request: CreatePurchaseRequest) =>
      purchaseService.createPurchase(request),

    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: purchaseKeys.list(),
        refetchType: "active" 
      });
    },
  });
}