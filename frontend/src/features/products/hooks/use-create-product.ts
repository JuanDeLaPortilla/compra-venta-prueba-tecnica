import { useMutation, useQueryClient } from "@tanstack/react-query";

import { productKeys } from "./product-keys";
import { productService } from "../infrastructure/product-service";
import type { CreateProductRequest } from "../domain/product";

export function useCreateProduct() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (request: CreateProductRequest) =>
      productService.create(request),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: productKeys.lists(),
      });
    },
  });
}