import { apiClient } from "@/core/http/api-client";
import type { ApiResponse } from "@/core/http/api-response";
import type { KardexProduct, ProductMovement } from "../domain/kardex";

export const kardexService = {
  async getProducts(): Promise<KardexProduct[]> {
    const response =
      await apiClient.get<ApiResponse<KardexProduct[]>>(
        "/kardex"
      );

    return response.data.data!;
  },

  async getMovements(
    productId: number
  ): Promise<ProductMovement[]> {

    const response =
      await apiClient.get<
        ApiResponse<ProductMovement[]>
      >(`/kardex/${productId}/movements`);

    return response.data.data!;
  }
};