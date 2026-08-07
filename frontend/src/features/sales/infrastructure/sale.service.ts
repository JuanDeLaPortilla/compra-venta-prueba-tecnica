import { apiClient } from "@/core/http/api-client";
import type { ApiResponse } from "@/core/http/api-response";
import type { CreateSaleRequest, Sale } from "../domain/sale";

export const saleService = {
  async getSales(): Promise<Sale[]> {
    const response =
      await apiClient.get<ApiResponse<Sale[]>>("/sales")

    return response.data.data!;
  },

  async createSale(
    request: CreateSaleRequest,
  ): Promise<ApiResponse<Sale>> {
    const response =
      await apiClient.post<ApiResponse<Sale>>(
        "/sales",
        request,
      );

    return response.data;
  },
}