import { useQuery } from "@tanstack/react-query";

import { kardexKeys } from "./kardex-keys";
import { kardexService } from "../infrastructure/kardex.service";

export const useProductMovements = (
  productId: number | null
) => {
  return useQuery({
    queryKey: productId
      ? kardexKeys.movements(productId)
      : ["kardex", "movements", "empty"],

    queryFn: () =>
      kardexService.getMovements(productId!),

    enabled: productId !== null,
  });
};