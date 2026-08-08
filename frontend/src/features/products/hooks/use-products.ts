import { useQuery } from "@tanstack/react-query";

import { productKeys } from "./product-keys";
import { productService } from "../infrastructure/product-service";

export function useProducts() {
  return useQuery({
    queryKey: productKeys.list(),
    queryFn: productService.getAll,
    refetchOnMount: "always",
  });
}